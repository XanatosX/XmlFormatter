using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.Interfaces.Formatter;
using XmlFormatter.src.Interfaces.Hotfolder;

namespace XmlFormatter.src.Hotfolder
{
    /// <summary>
    /// A default hotfolder manager
    /// </summary>
    class HotfolderManager : IHotfolderManager
    {

        /// <summary>
        /// All the hotfolder configurations and there file watcher
        /// </summary>
        private readonly Dictionary<IHotfolder, FileSystemWatcher> hotfolders;

        /// <summary>
        /// The last file we did create
        /// </summary>
        private string lastCreatedFile;

        /// <summary>
        /// Create a new instance of this manager class
        /// </summary>
        public HotfolderManager()
        {
            hotfolders = new Dictionary<IHotfolder, FileSystemWatcher>();
            lastCreatedFile = "";
        }


        /// <inheritdoc/>
        public bool AddHotfolder(IHotfolder newHotfolder)
        {
            if (GetHotfolderByWatchedFolder(newHotfolder.WatchedFolder) != null)
            {
                return false;
            }

            FileSystemWatcher watcher = new FileSystemWatcher(newHotfolder.WatchedFolder, newHotfolder.Filter);
            watcher.EnableRaisingEvents = true;
            watcher.Changed += Watcher_Changed;

            if (!Directory.Exists(newHotfolder.OutputFolder))
            {
                Directory.CreateDirectory(newHotfolder.OutputFolder);
            }

            if (newHotfolder.OnRename)
            {
                watcher.Renamed += Watcher_Changed;
            }
            hotfolders.Add(newHotfolder, watcher);
            return true;
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

            if (e.FullPath == lastCreatedFile)
            {
                return;
            }
            FileInfo fileInfo = new FileInfo(e.FullPath);
            IHotfolder hotfolder = GetHotfolderByWatchedFolder(fileInfo.DirectoryName);
            if (hotfolder == null)
            {
                return;
            }
            ConvertFile(hotfolder, e.FullPath);
        }

        private void ConvertFile(IHotfolder hotfolderConfig, string inputFile)
        {
            FileInfo fileInfo = new FileInfo(inputFile);
            string outputFile = GetOutputFileName(hotfolderConfig, fileInfo);
            lastCreatedFile = outputFile;
            bool result = hotfolderConfig.FormatterToUse.ConvertToFormat(inputFile, outputFile, hotfolderConfig.Mode);
            if (result && hotfolderConfig.RemoveOld)
            {
                File.Delete(inputFile);
            }

        }

        private string GetOutputFileName(IHotfolder hotfolderConfig, FileInfo inputFileInfo)
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
            hotfolders.Clear();
        }
    }
}
