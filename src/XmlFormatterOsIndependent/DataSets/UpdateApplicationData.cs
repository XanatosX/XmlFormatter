using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using XmlFormatterModel.Setting;

namespace XmlFormatterOsIndependent.DataSets
{
    /// <summary>
    /// This class represents the data needed to upgrade the application
    /// </summary>
    internal class UpdateApplicationData
    {
        /// <summary>
        /// The plugin management data required
        /// </summary>
        public PluginManagementData PluginManagementData { get; }

        /// <summary>
        /// The version compare data required for updating
        /// </summary>
        public VersionCompare VersionCompare { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="pluginManager">The plugin manager to use</param>
        /// <param name="settingsManager">The settings manager to use</param>
        /// <param name="compare">The compare class to use</param>
        public UpdateApplicationData(IPluginManager pluginManager, ISettingsManager settingsManager, VersionCompare compare)
            : this(new PluginManagementData(pluginManager, settingsManager), compare)
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="managementData">The plugin management data class</param>
        /// <param name="compare">The compare class to use</param>
        public UpdateApplicationData(PluginManagementData managementData, VersionCompare compare)
        {
            PluginManagementData = managementData;
            VersionCompare = compare;
        }
    }
}
