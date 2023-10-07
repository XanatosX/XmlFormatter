using Microsoft.Extensions.Logging;
using PluginFramework.EventMessages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace XmlFormatterModel.HotfolderFeature
{
    /// <summary>
    /// A default hotfolder manager
    /// </summary>
    public class HotfolderManager : IHotfolderManager
    {
        /// <summary>
        /// All the hotfolder configurations and there file watcher
        /// </summary>
        private readonly Dictionary<Hotfolder, FileSystemWatcher> hotfolders;

        /// <summary>
        /// How often should we try to read the file
        /// </summary>
        private readonly int readAttempts;

        /// <summary>
        /// The time to sleet between the attempts
        /// </summary>
        private readonly int sleepTime;

        /// <summary>
        /// All the tasks to work on
        /// </summary>
        private readonly List<HotfolderTask> tasks;

        /// <summary>
        /// The logger to use
        /// </summary>
        private readonly ILogger<HotfolderManager> logger;

        /// <summary>
        /// Is the conversion currently locked
        /// </summary>
        private bool locked;

        /// <summary>
        /// The last file which was converted
        /// </summary>
        private string lastInput;

        /// <summary>
        /// Create a new instance of this manager class
        /// </summary>
        public HotfolderManager(ILogger<HotfolderManager> logger)
        {
            hotfolders = new Dictionary<Hotfolder, FileSystemWatcher>();
            tasks = new List<HotfolderTask>();
            readAttempts = 25;
            sleepTime = 200;
            this.logger = logger;

        }


        /// <inheritdoc/>
        public bool AddHotfolder(Hotfolder newHotfolder)
        {
            if (IsSameWatcherRegistered(newHotfolder))
            {
                return false;
            }

            FileSystemWatcher watcher = new FileSystemWatcher(newHotfolder.WatchedFolder, newHotfolder.Filter)
            {
                EnableRaisingEvents = true
            };
            watcher.Changed += Watcher_Changed;

            if (!Directory.Exists(newHotfolder.OutputFolder))
            {
                Directory.CreateDirectory(newHotfolder.OutputFolder);
            }

            if (newHotfolder.OnRename)
            {
                watcher.Renamed += Watcher_Changed;
            }
            logger.LogInformation("Adding new hotfolder");
            logger.LogInformation($"Watched folder: {newHotfolder.WatchedFolder}");
            logger.LogInformation($"Output folder: {newHotfolder.OutputFolder}");
            logger.LogInformation($"Mode: {newHotfolder.Mode}");
            if (newHotfolder.FormatterToUse != null)
            {
                logger.LogInformation($"Formatter: {newHotfolder.FormatterToUse}");
                newHotfolder.FormatterToUse.StatusChanged += FormatterToUse_StatusChanged;
                hotfolders.Add(newHotfolder, watcher);
                return true;
            }
            logger.LogInformation("Formatter: Could not be found! Maybe the plugin was deleted?");
            return false;
        }

        /// <summary>
        /// Status of the formatter did change
        /// </summary>
        /// <param name="sender">Sender of the message</param>
        /// <param name="e">The data of the event</param>
        private void FormatterToUse_StatusChanged(object sender, BaseEventArgs e)
        {
            logger.LogInformation($"[{sender}]Convert status: {e.Message}");
        }

        /// <summary>
        /// Get the hotfolder by the watched folder
        /// </summary>
        /// <param name="watchedFolder">The watched folder to get the hotfolder for</param>
        /// <param name="filter">The filter used by the watcher</param>
        /// <returns>The hotfolder</returns>
        /// //
        private Hotfolder GetHotfolderByWatchedFolder(string watchedFolder, string filter)
        {
            return hotfolders.Keys.ToList().Find((currentHotfolder) =>
            {
                return currentHotfolder.WatchedFolder == watchedFolder && currentHotfolder.Filter == filter;
            });
        }

        /// <summary>
        /// Is there already an identical watcher registered
        /// </summary>
        /// <param name="hotfolder">The new hotfolder config to check</param>
        /// <returns>True if there is already an identical active configuration</returns>
        private bool IsSameWatcherRegistered(Hotfolder hotfolder)
        {
            return hotfolders.Keys.ToList().Find(currentHotfolder =>
            {
                bool isIdentical = false;
                if (hotfolder.FormatterToUse == null)
                {
                    return true;
                }
                if (currentHotfolder.FormatterToUse.GetType() == hotfolder.FormatterToUse.GetType())
                {
                    if (currentHotfolder.Mode == hotfolder.Mode && currentHotfolder.WatchedFolder == hotfolder.WatchedFolder)
                    {
                        isIdentical = true;
                    }
                }

                return isIdentical;
            }) != null;
        }

        /// <summary>
        /// Something in a hotfolder did change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (Directory.Exists(e.FullPath))
            {
                return;
            }

            if (sender is FileSystemWatcher watcher)
            {
                FileInfo fileInfo = new FileInfo(e.FullPath);
                Hotfolder hotfolder = GetHotfolderByWatchedFolder(fileInfo.DirectoryName, watcher.Filter);
                if (hotfolder == null || lastInput == e.FullPath)
                {
                    return;
                }
                if (tasks.Find((data) => { return data.InputFile == e.FullPath; }) == null)
                {
                    logger.LogInformation("Adding new convert task");
                    logger.LogInformation($"Input {e.FullPath}");
                    logger.LogInformation($"Output folder {hotfolder.OutputFolder}");
                    logger.LogInformation($"Mode {hotfolder.Mode}");
                    logger.LogInformation($"Converter {hotfolder.FormatterToUse}");
                    lastInput = e.FullPath;
                    tasks.Add(new HotfolderTask(e.FullPath, hotfolder));
                    logger.LogInformation($"Current task stack {tasks.Count}");
                }
                PerformTasks();
            }
        }

        /// <summary>
        /// Perform all the stacked tasks
        /// </summary>
        private void PerformTasks()
        {
            PerformTasks(string.Empty);
        }

        /// <summary>
        /// Perform all the stacked tasks
        /// </summary>
        /// <param name="currentFile">The current file triggering the function</param>
        private void PerformTasks(string currentFile)
        {
            if (!locked)
            {
                locked = true;
                Task<bool> convertTask = AsyncConvertFiles();
                convertTask.ContinueWith((result) =>
                {
                    locked = false;
                    if (lastInput == currentFile)
                    {
                        lastInput = "";
                    }
                    if (tasks.Count > 0)
                    {
                        PerformTasks();
                    }
                });
            }
        }

        /// <summary>
        /// Convert files async
        /// </summary>
        /// <returns>A task with the completion status</returns>
        private async Task<bool> AsyncConvertFiles()
        {
            List<HotfolderTask> tasksToDo = new List<HotfolderTask>();
            lock (tasks)
            {
                foreach (HotfolderTask task in tasks)
                {
                    tasksToDo.Add(task);
                }
                tasks.Clear();
            }

            if (tasksToDo.Count == 0)
            {
                return true;
            }

            logger.LogInformation($"Working on {tasksToDo.Count} tasks");

            foreach (HotfolderTask task in tasksToDo)
            {
                logger.LogInformation($"Start task {task.InputFile}");
                ConvertFile(task.Configuration, task.InputFile);
            }
            return await AsyncConvertFiles();
        }

        /// <summary>
        /// Convert the file to the hotfolder config
        /// </summary>
        /// <param name="hotfolderConfig">The hotfolder configuration to use</param>
        /// <param name="inputFile">The file used as input</param>
        private void ConvertFile(Hotfolder hotfolderConfig, string inputFile)
        {
            bool success = false;
            for (int i = 0; i < readAttempts; i++)
            {
                try
                {
                    using (StreamReader reader = new StreamReader(inputFile))
                    {
                        success = true;
                        break;
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(sleepTime);
                }
            }

            if (!success)
            {
                return;
            }

            FileInfo fileInfo = new FileInfo(inputFile);
            string outputFile = GetOutputFilePath(hotfolderConfig, fileInfo);
            bool result = hotfolderConfig.FormatterToUse.ConvertToFormat(inputFile, outputFile, hotfolderConfig.Mode);
            if (result && hotfolderConfig.RemoveOld)
            {
                File.Delete(inputFile);
            }

        }

        /// <summary>
        /// Get the name of the output file
        /// </summary>
        /// <param name="hotfolderConfig">The hotfolder configuration to use</param>
        /// <param name="inputFileInfo">The name of the input file</param>
        /// <returns>The output path of the file /returns>
        private string GetOutputFilePath(Hotfolder hotfolderConfig, FileInfo inputFileInfo)
        {
            string returnString = hotfolderConfig.OutputFileScheme;
            string fileName = inputFileInfo.Name.Replace(inputFileInfo.Extension, "");

            returnString = returnString.Replace("{inputfile}", fileName);
            returnString = returnString.Replace("{format}", hotfolderConfig.Mode.ToString());
            string extension = inputFileInfo.Extension.Replace(".", "");
            returnString = returnString.Replace("{extension}", extension);

            returnString = hotfolderConfig.OutputFolder + "\\" + returnString;
            return returnString;
        }

        /// <inheritdoc/>
        public List<Hotfolder> GetHotfolders()
        {
            return hotfolders.Keys.ToList();
        }

        /// <inheritdoc/>
        public bool RemoveHotfolder(Hotfolder hotfolderToRemove)
        {
            logger.LogInformation("Removing hotfolder ");
            logger.LogInformation($"Watched folder: {hotfolderToRemove.WatchedFolder}");
            logger.LogInformation($"Output folder: {hotfolderToRemove.OutputFolder}");
            logger.LogInformation($"Mode: {hotfolderToRemove.Mode}");
            logger.LogInformation($"Formatter {hotfolderToRemove.FormatterToUse}");
            if (!hotfolders.ContainsKey(hotfolderToRemove))
            {
                return false;
            }
            FileSystemWatcher watcher = hotfolders[hotfolderToRemove];
            watcher.Changed -= Watcher_Changed;
            if (hotfolderToRemove.OnRename)
            {
                watcher.Renamed -= Watcher_Changed;
            }
            return hotfolders.Remove(hotfolderToRemove);
        }

        /// <inheritdoc/>
        public void ResetManager()
        {
            logger.LogInformation("Clearing hotfolders");
            hotfolders.Clear();
        }
    }
}
