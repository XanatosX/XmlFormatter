using PluginFramework.DataContainer;
using PluginFramework.Enums;

namespace XmlFormatterOsIndependent.DataSets.Files
{
    /// <summary>
    /// Data set required for saving a file
    /// </summary>
    class SaveFileConversionData
    {
        /// <summary>
        /// The input file which should be converted
        /// </summary>
        public string InputFile { get; }

        /// <summary>
        /// The mode to convert the file with
        /// </summary>
        public ModesEnum Mode { get; }

        /// <summary>
        /// The plugin meta data used for loading
        /// </summary>
        public PluginMetaData PluginMeta { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="inputFile">The input file which should be converted</param>
        /// <param name="mode">The mode used for converting the input file</param>
        /// <param name="pluginMeta">The meta data of the plugin to use for conversion</param>
        public SaveFileConversionData(string inputFile, ModesEnum mode, PluginMetaData pluginMeta)
        {
            InputFile = inputFile;
            Mode = mode;
            PluginMeta = pluginMeta;
        }
    }
}
