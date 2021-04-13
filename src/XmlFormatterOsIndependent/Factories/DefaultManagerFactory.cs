using PluginFramework.Interfaces.Manager;
using PluginFramework.LoadStrategies;
using PluginFramework.Manager;
using System.IO;
using System.Reflection;
using System.Text;
using XmlFormatterModel.Setting;
using XmlFormatterModel.Update;
using XMLFormatterModel.Setting.InputOutput;
using XmlFormatterOsIndependent.Update;

namespace XmlFormatterOsIndependent.Factories
{
    /// <summary>
    /// Factory to create the manager used for this application
    /// </summary>
    internal class DefaultManagerFactory
    {
        /// <summary>
        /// The factory used to create the version managment
        /// </summary>
        private IVersionManagerFactory versionManagerFactory;

        /// <summary>
        /// The plugin manager to use
        /// </summary>
        private IPluginManager pluginManager;

        /// <summary>
        /// The settings manager to use
        /// </summary>
        private ISettingsManager settingsManager;

        /// <summary>
        /// Get the current plugin manager
        /// </summary>
        /// <returns>The plugin manager to use</returns>
        public IPluginManager GetPluginManager()
        {
            if (pluginManager == null)
            {
                pluginManager = new DefaultManager();
                StringBuilder builder = new StringBuilder();


                FileInfo folderInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
                string folder = folderInfo.DirectoryName;
                builder.AppendFormat("{0}{1}Plugins{1}", folder, Path.DirectorySeparatorChar);
                pluginManager.SetDefaultLoadStrategy(new PluginFolder(builder.ToString()));
            }

            return pluginManager;
        }

        /// <summary>
        /// Get the settings manager for this application
        /// </summary>
        /// <returns>The settings manager to use</returns>
        public ISettingsManager GetSettingsManager()
        {
            if (settingsManager == null)
            {
                settingsManager = new SettingsManager();
                settingsManager.SetPersistendFactory(new XmlProviderFactory());
            }
            return settingsManager;
        }

        /// <summary>
        /// Get the version manager of this application
        /// </summary>
        /// <returns>The version manager to use</returns>
        public IVersionManager GetVersionManager()
        {
            if (versionManagerFactory == null)
            {
                versionManagerFactory = new UpdateManagerFactory();
            }
            return versionManagerFactory.GetVersionManager();
        }
    }
}
