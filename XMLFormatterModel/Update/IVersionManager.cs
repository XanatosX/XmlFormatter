using PluginFramework.DataContainer;
using PluginFramework.EventMessages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XmlFormatterModel.Update
{
    public interface IVersionManager
    {
        event EventHandler<BaseEventArgs> Error;

        Task<Version> GetRemoteVersion();
        Task<Version> GetLocalVersion();

        Version ConvertStringToVersion(string stringVersion);
        string GetStringVersion(Version version);
        Task<VersionCompare> RemoteVersionIsNewer();
    }
}
