using PluginFramework.DataContainer;
using PluginFramework.EventMessages;
using System;
using System.Threading.Tasks;
using XmlFormatterModel.Update.Strategies;

namespace XmlFormatterModel.Update
{
    /// <summary>
    /// Create a new version manager
    /// </summary>
    public class VersionManager : IVersionManager
    {
        /// <summary>
        /// The strategy to convert version to string and the other way round
        /// </summary>
        private readonly IVersionConvertStrategy versionConvert;

        /// <summary>
        /// The receiver to get the local version
        /// </summary>
        private readonly IVersionReceiverStrategy localReciever;

        /// <summary>
        /// The receiver to get the remote version
        /// </summary>
        private readonly IVersionReceiverStrategy remoteReciever;

        /// <summary>
        /// Error event if something went wrong
        /// </summary>
        public event EventHandler<BaseEventArgs> Error;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="versionConvert">The strategy to use for version class to string converting</param>
        /// <param name="localReciever">The reciever to use to get local version</param>
        /// <param name="remoteReciever">The reciever to use to get remote version</param>
        public VersionManager(IVersionConvertStrategy versionConvert, IVersionReceiverStrategy localReciever, IVersionReceiverStrategy remoteReciever)
        {
            this.versionConvert = versionConvert;
            this.localReciever = localReciever;
            this.remoteReciever = remoteReciever;

            this.remoteReciever.Error += Reciever_Error;
            this.localReciever.Error += Reciever_Error;
        }

        /// <summary>
        /// Event if any of the recievers did throw one
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void Reciever_Error(object sender, BaseEventArgs e)
        {
            ThrowError(e);
        }

        /// <inheritdoc/>
        public Version ConvertStringToVersion(string stringVersion)
        {
            return versionConvert.ConvertStringToVersion(stringVersion);
        }

        /// <inheritdoc/>
        public string GetStringVersion(Version version)
        {
            return versionConvert.GetStringVersion(version);
        }

        /// <inheritdoc/>
        public Task<Version> GetRemoteVersionAsync()
        {
            return remoteReciever.GetVersionAsync(versionConvert);
        }

        /// <inheritdoc/>
        public Task<Version> GetLocalVersionAsync()
        {
            return localReciever.GetVersionAsync(versionConvert);
        }

        /// <inheritdoc/>
        public async Task<VersionCompare> RemoteVersionIsNewerAsync()
        {
            Version remoteVersion = await GetRemoteVersionAsync();
            Version localVersion = await GetLocalVersionAsync();
            int compareResult = localVersion.CompareTo(remoteVersion);
            IRelease latestRelease = await remoteReciever.GetLatestReleaseAsync();

            return new VersionCompare(compareResult < 0, localVersion, remoteVersion, latestRelease);
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

