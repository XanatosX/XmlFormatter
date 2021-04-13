using PluginFramework.EventMessages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using XmlFormatterModel.Update;
using XmlFormatterModel.Update.Strategies;

namespace XmlFormatter.Update
{
    class LocalVersionReciever : IVersionRecieverStrategy
    {
        public event EventHandler<BaseEventArgs> Error;

        public Task<IRelease> GetLatestRelease()
        {
            return default;
        }

        public Task<List<IRelease>> GetReleases()
        {
            return default;
        }

        public async Task<Version> GetVersion(IVersionConvertStrategy convertStrategy)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string versionString = "0.0.0";
            using (Stream stream = assembly.GetManifestResourceStream("XmlFormatter.Version.txt"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    versionString = reader.ReadLine();
                }
            }

            return convertStrategy.ConvertStringToVersion(versionString);
        }
    }
}
