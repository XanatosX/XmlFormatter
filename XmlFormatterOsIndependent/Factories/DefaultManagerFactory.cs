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
    internal class DefaultManagerFactory
    {
        private readonly IVersionManagerFactory versionManagerFactory;

        public DefaultManagerFactory()
        {
            versionManagerFactory = new UpdateManagerFactory();
        }
        public IPluginManager GetPluginManager()
        {
            IPluginManager manager = new DefaultManager();
            StringBuilder builder = new StringBuilder();


            FileInfo folderInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            string folder = folderInfo.DirectoryName;
            builder.AppendFormat("{0}{1}Plugins{1}", folder, Path.DirectorySeparatorChar);
            manager.SetDefaultLoadStrategy(new PluginFolder(builder.ToString()));

            return manager;
        }

        public ISettingsManager GetSettingsManager()
        {
            ISettingsManager manager = new SettingsManager();
            manager.SetPersistendFactory(new XmlProviderFactory());
            return manager;
        }

        public IVersionManager GetVersionManager()
        {
            return versionManagerFactory.GetVersionManager();
        }
    }
}
