using PluginFramework.Interfaces.Manager;
using XmlFormatterModel.Setting;

namespace XmlFormatterOsIndependent.DataSets
{
    /// <summary>
    /// Data container to manage plugin data
    /// </summary>
    internal class PluginManagmentData
    {
        /// <summary>
        /// The plugin manager to use for loading and listing
        /// </summary>
        public IPluginManager PluginManager { get; }

        /// <summary>
        /// The settings manager to use to get the plugins
        /// </summary>
        public ISettingsManager SettingsManager { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="pluginManager">The plugin manager to use</param>
        /// <param name="settingsManager">The settings manager to use</param>
        public PluginManagmentData(IPluginManager pluginManager, ISettingsManager settingsManager)
        {
            PluginManager = pluginManager;
            SettingsManager = settingsManager;
        }

    }
}
