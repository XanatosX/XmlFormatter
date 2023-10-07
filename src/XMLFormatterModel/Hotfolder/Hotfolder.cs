using PluginFramework.Enums;
using PluginFramework.Interfaces.PluginTypes;

namespace XMLFormatterModel.Hotfolder
{
    /// <summary>
    /// This class is a default hotfolder configuration
    /// </summary>
    public class Hotfolder
    {
        /// <summary>
        /// The mode to use for this hotfolder
        /// </summary>
        public ModesEnum Mode { get; set; }

        /// <summary>
        /// The formatter to use
        /// </summary>
        public IFormatter FormatterToUse { get; }

        /// <summary>
        /// The folder to watch
        /// </summary>
        public string WatchedFolder { get; set; }

        /// <summary>
        /// The filter to use for the input files
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// The folder to write the output to
        /// </summary>
        public string OutputFolder { get; set; }

        /// <summary>
        /// The scheme of the output file name
        /// </summary>
        public string OutputFileScheme { get; set; }

        /// <summary>
        /// Should be triggered on rename as well
        /// </summary>
        public bool OnRename { get; set; }

        /// <summary>
        /// Should we keep the input file
        /// </summary>
        public bool RemoveOld { get; set; }


        /// <summary>
        /// Create a new instance of the hotfolder configuration
        /// </summary>
        /// <param name="formatter">The formatter class to use</param>
        /// <param name="watchedFolder">The hotfolder to watch</param>
        public Hotfolder(IFormatter formatter, string watchedFolder)
        {
            FormatterToUse = formatter;
            WatchedFolder = watchedFolder;
            Filter = "*.*";
            OutputFolder = WatchedFolder + "\\output";
            OutputFileScheme = "{inputfile}_{format}.{extension}";
        }
    }
}
