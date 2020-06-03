using PluginFramework.EventMessages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XmlFormatterModel.Update;
using XmlFormatterModel.Update.Strategies;

namespace XmlFormatterOsIndependent.Update
{
    internal class LocalVersionRecieverStrategy : IVersionRecieverStrategy
    {
        public event EventHandler<BaseEventArgs> Error;

        public async Task<IRelease> GetLatestRelease()
        {
            return default;
        }

        public async Task<List<IRelease>> GetReleases()
        {
            return default;
        }

        public async Task<Version> GetVersion(IVersionConvertStrategy convertStrategy)
        {
            return new Version(0, 0);
        }
    }
}
