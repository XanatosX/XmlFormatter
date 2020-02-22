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
        /// All the active hotfolders
        /// </summary>
        private readonly List<IHotfolder> hotfolders;

        /// <summary>
        /// The file system watcher to use
        /// </summary>
        private readonly List<FileSystemWatcher> fileSystemWatcher;

        /// <summary>
        /// Create a new instance of this manager class
        /// </summary>
        public HotfolderManager()
        {
            hotfolders = new List<IHotfolder>();
            fileSystemWatcher = new List<FileSystemWatcher>();
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
            hotfolders.Add(newHotfolder);
            fileSystemWatcher.Add(watcher);
            return true;
        }

        private IHotfolder GetHotfolderByWatchedFolder(string watchedFolder)
        {
            return hotfolders.Find((currentFolder) =>
            {
                return currentFolder.WatchedFolder == watchedFolder;
            });
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (Directory.Exists(e.FullPath))
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
            returnString = returnString.Replace("{extension}", inputFileInfo.Extension);
            returnString = hotfolderConfig.OutputFolder + "\\" + returnString;
            return returnString;
        }

        /// <inheritdoc/>
        public List<IHotfolder> GetHotfolders()
        {
            return hotfolders;
        }

        /// <inheritdoc/>
        public bool RemoveHotfolder(IHotfolder hotfolderToRemove)
        {
            return hotfolders.Remove(hotfolderToRemove);
        }

        /// <inheritdoc/>
        public void ResetManager()
        {
            hotfolders.Clear();
            fileSystemWatcher.Clear();
        }
    }
}
