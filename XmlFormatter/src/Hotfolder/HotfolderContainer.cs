using PluginFramework.src.Enums;
using PluginFramework.src.Interfaces.PluginTypes;
using XmlFormatter.src.Enums;
using XmlFormatter.src.Interfaces.Hotfolder;

namespace XmlFormatter.src.Hotfolder
{
    /// <summary>
    /// This class is a default hotfolder configuration
    /// </summary>
    class HotfolderContainer : IHotfolder
    {
        /// <inheritdoc/>
        public ModesEnum Mode
        {
            get => mode;
            set => mode = value;
        }

        /// <summary>
        /// The mode to use for formatting
        /// </summary>
        private ModesEnum mode;

        /// <inheritdoc/>
        public IFormatter FormatterToUse => formatterToUse;

        /// <summary>
        /// The formatter to use
        /// </summary>
        private readonly IFormatter formatterToUse;

        /// <inheritdoc/>
        public string WatchedFolder
        {
            get => watchedFolder;
            set => watchedFolder = value;
        }

        /// <summary>
        /// The folder which is getting watched
        /// </summary>
        private string watchedFolder;

        /// <inheritdoc/>
        public string Filter
        {
            get => filter;
            set => filter = value; 
        }

        /// <summary>
        /// The filter to use in the watched folder
        /// </summary>
        private string filter;

        /// <inheritdoc/>
        public string OutputFolder
        {
            get => outputFolder;
            set => outputFolder = value;
        }

        /// <summary>
        /// The output folder to save the new file in
        /// </summary>
        private string outputFolder;

        /// <inheritdoc/>
        public string OutputFileScheme
        {
            get => outputFileScheme;
            set => outputFileScheme = value;
        }

        /// <summary>
        /// The scheme of the output file
        /// </summary>
        private string outputFileScheme;

        /// <inheritdoc/>
        public bool OnRename
        {
            get => onRename;
            set => onRename = value;
        }

        /// <summary>
        /// Should we trigger on rename as well
        /// </summary>
        private bool onRename;

        /// <inheritdoc/>
        public bool RemoveOld
        {
            get => removeOld;
            set => removeOld = value;
        }

        /// <summary>
        /// Should we delete the old file
        /// </summary>
        private bool removeOld;

        /// <summary>
        /// Create a new instance of the hotfolder configuration
        /// </summary>
        /// <param name="formatter">The formatter class to use</param>
        /// <param name="watchedFolder">The hotfolder to watch</param>
        public HotfolderContainer(IFormatter formatter, string watchedFolder)
        {
            formatterToUse = formatter;
            this.watchedFolder = watchedFolder;
            filter = "*.*";
            outputFolder = this.watchedFolder + "\\output";
            outputFileScheme = "{inputfile}_{format}.{extension}";
        }
    }
}
