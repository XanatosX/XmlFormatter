using XmlFormatter.src.Enum;
using XmlFormatter.src.Interfaces.Formatter;

namespace XmlFormatter.src.Interfaces.Hotfolder
{
    /// <summary>
    /// This interface will define a single hotfolder
    /// </summary>
    interface IHotfolder
    {
        /// <summary>
        /// The mode to use for this hotfolder
        /// </summary>
        ModesEnum Mode { get; }

        /// <summary>
        /// The formatter to use
        /// </summary>
        IFormatter FormatterToUse { get; }

        /// <summary>
        /// The folder to watch
        /// </summary>
        string WatchedFolder { get;  }

        /// <summary>
        /// The folder to write the output to
        /// </summary>
        string OutputFolder { get; }

        /// <summary>
        /// The scheme of the output file name
        /// </summary>
        string OutputFileScheme { get; }

        /// <summary>
        /// Should be triggerd on change as well
        /// </summary>
        bool OnChange { get;  }
    }
}
