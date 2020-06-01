using PluginFramework.EventMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XmlFormatterModel.Update.Strategies
{
    public interface IVersionRecieverStrategy
    {
        event EventHandler<BaseEventArgs> Error;

        Task<Version> GetVersion(IVersionConvertStrategy convertStrategy);
        Task<List<IRelease>> GetReleases();
        Task<IRelease> GetLatestRelease();
    }
}
