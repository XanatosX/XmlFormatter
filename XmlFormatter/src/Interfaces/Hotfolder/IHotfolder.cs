using PluginFramework.src.Enums;
using PluginFramework.src.Interfaces.PluginTypes;

namespace XmlFormatter.src.Interfaces.Hotfolder
{
    /// <summary>
    /// This interface will define a single hotfolder
    /// </summary>
    public interface IHotfolder
    {
        /// <summary>
        /// The mode to use for this hotfolder
        /// </summary>
        ModesEnum Mode { get; set; }

        /// <summary>
        /// The formatter to use
        /// </summary>
        IFormatter FormatterToUse { get; }

        /// <summary>
        /// The folder to watch
        /// </summary>
        string WatchedFolder { get; set; }

        /// <summary>
        /// The filter to use for the input files
        /// </summary>
        string Filter { get; set; }

        /// <summary>
        /// The folder to write the output to
        /// </summary>
        string OutputFolder { get; set; }

        /// <summary>
        /// The scheme of the output file name
        /// </summary>
        string OutputFileScheme { get; set; }

        /// <summary>
        /// Should be triggerd on rename as well
        /// </summary>
        bool OnRename { get; set; }

        /// <summary>
        /// Should we keep the input file
        /// </summary>
        bool RemoveOld { get; set; }
    }
}
