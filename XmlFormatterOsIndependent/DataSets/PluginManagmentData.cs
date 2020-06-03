using PluginFramework.Interfaces.Manager;
using XmlFormatterModel.Setting;

namespace XmlFormatterOsIndependent.DataSets
{
    internal class PluginManagmentData
    {
        public IPluginManager PluginManager { get; }
        public ISettingsManager SettingsManager { get; }

        public PluginManagmentData(IPluginManager pluginManager, ISettingsManager settingsManager)
        {
            PluginManager = pluginManager;
            SettingsManager = settingsManager;
        }

    }
}
