using PluginFramework.DataContainer;
using PluginFramework.Enums;

namespace XmlFormatterOsIndependent.DataSets.Files
{
    class SaveFileConversionData
    {
        public string InputFile { get; }
        public ModesEnum Mode { get; }
        public PluginMetaData PluginMeta { get; }

        public SaveFileConversionData(string inputFile, ModesEnum mode, PluginMetaData pluginMeta)
        {
            InputFile = inputFile;
            Mode = mode;
            PluginMeta = pluginMeta;
        }
    }
}
