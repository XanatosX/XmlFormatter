using PluginFramework.DataContainer;
using PluginFramework.EventMessages;
using System;
using System.Threading.Tasks;
using XmlFormatterModel.Update.Strategies;

namespace XmlFormatterModel.Update
{
    public class VersionManager : IVersionManager
    {
        private readonly IVersionConvertStrategy versionConvert;
        private readonly IVersionRecieverStrategy localReciever;
        private readonly IVersionRecieverStrategy remoteReciever;

        public event EventHandler<BaseEventArgs> Error;

        public VersionManager(IVersionConvertStrategy versionConvert, IVersionRecieverStrategy localReciever, IVersionRecieverStrategy remoteReciever)
        {
            this.versionConvert = versionConvert;
            this.localReciever = localReciever;
            this.remoteReciever = remoteReciever;

            this.remoteReciever.Error += Reciever_Error;
            this.localReciever.Error += Reciever_Error;
        }

        private void Reciever_Error(object sender, BaseEventArgs e)
        {
            ThrowError(e);
        }

        public Version ConvertStringToVersion(string stringVersion)
        {
            return versionConvert.ConvertStringToVersion(stringVersion);
        }

        public string GetStringVersion(Version version)
        {
            return versionConvert.GetStringVersion(version);
            return version.Major + "." + version.Minor + "." + version.Build;
        }

        public Task<Version> GetRemoteVersion()
        {
            return remoteReciever.GetVersion(versionConvert);
        }

        public Task<Version> GetLocalVersion()
        {
            return localReciever.GetVersion(versionConvert);
        }

        public async Task<VersionCompare> RemoteVersionIsNewer()
        {
            Version remoteVersion = await GetRemoteVersion();
            Version localVersion = await GetLocalVersion();
            int compareResult = localVersion.CompareTo(remoteVersion);
            IRelease latestRelease = await remoteReciever.GetLatestRelease();

            return new VersionCompare(compareResult < 0, localVersion, remoteVersion, latestRelease);
        }

        /// <summary>
        /// Throw an error from this class
        /// </summary>
        /// <param name="title">The title of the error</param>
        /// <param name="message">The message of the error</param>
        private void ThrowError(string title, string message)
        {
            BaseEventArgs data = new BaseEventArgs(title, message);
            ThrowError(data);
        }

        /// <summary>
        /// Throw an error from this class
        /// </summary>
        /// <param name="baseEventArgs">The arguments to throw the event for</param>
        private void ThrowError(BaseEventArgs baseEventArgs)
        {
            EventHandler<BaseEventArgs> handler = Error;
            handler?.Invoke(this, baseEventArgs);
        }
    }
}

