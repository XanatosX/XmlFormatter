using PluginFramework.Enums;
using PluginFramework.Interfaces.PluginTypes;

namespace XMLFormatterModel.Hotfolder
{
    /// <summary>
    /// This class is a default hotfolder configuration
    /// </summary>
    public class HotfolderContainer : IHotfolder
    {
        /// <inheritdoc/>
        public ModesEnum Mode { get; set; }

        /// <inheritdoc/>
        public IFormatter FormatterToUse { get; set; }

        /// <inheritdoc/>
        public string WatchedFolder { get; set; }

        /// <inheritdoc/>
        public string Filter { get; set; }

        /// <inheritdoc/>
        public string OutputFolder { get; set; }

        /// <inheritdoc/>
        public string OutputFileScheme { get; set; }

        /// <inheritdoc/>
        public bool OnRename { get; set; }

        /// <inheritdoc/>
        public bool RemoveOld { get; set; }

        /// <summary>
        /// Create a new instance of the hotfolder configuration
        /// </summary>
        /// <param name="formatter">The formatter class to use</param>
        /// <param name="watchedFolder">The hotfolder to watch</param>
        public HotfolderContainer(IFormatter formatter, string watchedFolder)
        {
            FormatterToUse = formatter;
            WatchedFolder = watchedFolder;
            Filter = "*.*";
            OutputFolder = WatchedFolder + "\\output";
            OutputFileScheme = "{inputfile}_{format}.{extension}";
        }
    }
}
