using PluginFramework.Interfaces.Manager;
using PluginFramework.LoadStrategies;
using PluginFramework.Manager;
using System;
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
    internal static class DefaultManagerFactory
    {
        /// <summary>
        /// The factory used to create the version managment
        /// </summary>
        private static IVersionManagerFactory versionManagerFactory;

        /// <summary>
        /// The plugin manager to use
        /// </summary>
        private static IPluginManager pluginManager;

        /// <summary>
        /// The settings manager to use
        /// </summary>
        private static ISettingsManager settingsManager;

        /// <summary>
        /// Get the current plugin manager
        /// </summary>
        /// <returns>The plugin manager to use</returns>
        public static IPluginManager GetPluginManager()
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
        public static ISettingsManager GetSettingsManager()
        {
            if (settingsManager == null)
            {
                settingsManager = new SettingsManager();
                settingsManager.SetPersistendFactory(new XmlProviderFactory());
                settingsManager.Load(GetSettingPath());
            }
            return settingsManager;
        }

        /// <summary>
        /// Get the version manager of this application
        /// </summary>
        /// <returns>The version manager to use</returns>
        public static IVersionManager GetVersionManager()
        {
            if (versionManagerFactory == null)
            {
                versionManagerFactory = new UpdateManagerFactory();
            }
            return versionManagerFactory.GetVersionManager();
        }

        /// <summary>
        /// Get the settings path to use
        /// </summary>
        /// <returns>The path to the settings folder</returns>
        public static string GetSettingPath()
        {
           string settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "XMLFormatter");
           return Path.Combine(settingsPath, "settings.set");
        }
    }
}
