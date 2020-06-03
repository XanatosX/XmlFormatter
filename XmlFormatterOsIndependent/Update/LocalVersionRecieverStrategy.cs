using PluginFramework.EventMessages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XmlFormatterModel.Update;
using XmlFormatterModel.Update.Strategies;

namespace XmlFormatterOsIndependent.Update
{
    /// <summary>
    /// Strategy to use to get the local version
    /// </summary>
    internal class LocalVersionRecieverStrategy : IVersionRecieverStrategy
    {
        /// <summary>
        /// Was there any error in recieving the version
        /// </summary>
        public event EventHandler<BaseEventArgs> Error;

        /// <inheritdoc/>
        public async Task<IRelease> GetLatestRelease()
        {
            return default;
        }

        /// <inheritdoc/>
        public async Task<List<IRelease>> GetReleases()
        {
            return default;
        }

        /// <inheritdoc/>
        public async Task<Version> GetVersion(IVersionConvertStrategy convertStrategy)
        {
            return new Version(0, 0);
        }
    }
}
