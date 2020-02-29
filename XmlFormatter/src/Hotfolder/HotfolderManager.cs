using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XmlFormatter.src.DataContainer;
using XmlFormatter.src.DataContainer.Logging;
using XmlFormatter.src.Interfaces.Hotfolder;
using XmlFormatter.src.Interfaces.Logging;

namespace XmlFormatter.src.Hotfolder
{
    /// <summary>
    /// A default hotfolder manager
    /// </summary>
    class HotfolderManager : IHotfolderManager, ILoggable
    {
        /// <summary>
        /// All the hotfolder configurations and there file watcher
        /// </summary>
        private readonly Dictionary<IHotfolder, FileSystemWatcher> hotfolders;

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
        /// Is the conversion currently locked
        /// </summary>
        private bool locked;

        /// <summary>
        /// Logging manager to use
        /// </summary>
        private ILoggingManager loggingManager;

        /// <summary>
        /// The last file which was converted
        /// </summary>
        private string lastInput;

        /// <summary>
        /// Create a new instance of this manager class
        /// </summary>
        public HotfolderManager()
        {
            hotfolders = new Dictionary<IHotfolder, FileSystemWatcher>();
            tasks = new List<HotfolderTask>();
            readAttempts = 25;
            sleepTime = 200;
        }


        public void SetLoggingManager(ILoggingManager loggingManager)
        {
            this.loggingManager = loggingManager;
        }

        private void LogMessage(LoggingMessage message)
        {
            if (loggingManager == null)
            {
                return;
            }

            loggingManager.LogMessage(message);
        }


        /// <inheritdoc/>
        public bool AddHotfolder(IHotfolder newHotfolder)
        {
            if (GetHotfolderByWatchedFolder(newHotfolder.WatchedFolder) != null)
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
            LogMessage(
                new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Adding new hotfolder ")
            );
            LogMessage(
                new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Watched folder: " + newHotfolder.WatchedFolder)
            );
            LogMessage(
                new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Output folder: " + newHotfolder.OutputFolder)
            );
            LogMessage(
                new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Mode: " + newHotfolder.Mode)
            );
            LogMessage(
                new LoggingMessage(
                    Enums.LogScopesEnum.Hotfolder,
                    this,
                    "Formatter " + newHotfolder.FormatterToUse.ToString()
                )
            );
            newHotfolder.FormatterToUse.StatusChanged += FormatterToUse_StatusChanged;
            hotfolders.Add(newHotfolder, watcher);
            return true;
        }

        private void FormatterToUse_StatusChanged(object sender, EventMessages.BaseEventArgs e)
        {
            LogMessage(
                new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Convert status: " + e.Message)
            );
        }

        /// <summary>
        /// Get the hotfolder by the watched folder
        /// </summary>
        /// <param name="watchedFolder">The watched folder to get the hotfolder for</param>
        /// <returns>The hotfolder</returns>
        private IHotfolder GetHotfolderByWatchedFolder(string watchedFolder)
        {
            return hotfolders.Keys.ToList().Find((currentHotfolder) =>
            {
                return currentHotfolder.WatchedFolder == watchedFolder;
            });
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

            FileInfo fileInfo = new FileInfo(e.FullPath);
            IHotfolder hotfolder = GetHotfolderByWatchedFolder(fileInfo.DirectoryName);
            if (hotfolder == null || lastInput == e.FullPath)
            {
                return;
            }
            if (tasks.Find((data) => { return data.InputFile == e.FullPath; }) == null)
            {
                LogMessage(
                    new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Adding new convert task")
                );
                LogMessage(
                    new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Input " + e.FullPath)
                );
                LogMessage(
                    new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Output folder " + hotfolder.OutputFolder)
                );
                LogMessage(
                    new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Mode " + hotfolder.Mode)
                );
                LogMessage(
                    new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Converter " + hotfolder.FormatterToUse.ToString())
                );
                lastInput = e.FullPath;
                tasks.Add(new HotfolderTask(e.FullPath, hotfolder));
                LogMessage(
                    new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Current task stack " + tasks.Count)
                );
            }
            if (!locked)
            {
                locked = true;
                Task<bool> convertTask = AsyncConvertFiles();
                convertTask.ContinueWith((result) =>
                {
                    locked = false;
                    if (lastInput == e.FullPath)
                    {
                        lastInput = "";
                    }
                });
            }
        }

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

            LogMessage(
                new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Working on " + tasksToDo.Count + " tasks")
            );

            foreach (HotfolderTask task in tasksToDo)
            {
                LogMessage(
                    new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Start task " + task.InputFile)
                ); ;
                ConvertFile(task.Configuration, task.InputFile);
            }
            return await AsyncConvertFiles();
        }

        /// <summary>
        /// Convert the file to the hotfolder config
        /// </summary>
        /// <param name="hotfolderConfig">The hotfolder configuration to use</param>
        /// <param name="inputFile">The file used as input</param>
        private void ConvertFile(IHotfolder hotfolderConfig, string inputFile)
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
        private string GetOutputFilePath(IHotfolder hotfolderConfig, FileInfo inputFileInfo)
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
        public List<IHotfolder> GetHotfolders()
        {
            return hotfolders.Keys.ToList();
        }

        /// <inheritdoc/>
        public bool RemoveHotfolder(IHotfolder hotfolderToRemove)
        {
            LogMessage(
                new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Removing hotfolder ")
            );
            LogMessage(
                new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Watched folder: " + hotfolderToRemove.WatchedFolder)
            );
            LogMessage(
                new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Output folder: " + hotfolderToRemove.OutputFolder)
            );
            LogMessage(
                new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Mode: " + hotfolderToRemove.Mode)
            );
            LogMessage(
                new LoggingMessage(
                    Enums.LogScopesEnum.Hotfolder,
                    this,
                    "Formatter " + hotfolderToRemove.FormatterToUse.ToString()
                )
            );
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
            LogMessage(
                new LoggingMessage(Enums.LogScopesEnum.Hotfolder, this, "Clearing hotfolders")
            );
            hotfolders.Clear();
        }
    }
}
