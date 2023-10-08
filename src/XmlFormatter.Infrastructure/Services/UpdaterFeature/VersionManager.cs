using XmlFormatter.Application.Services.UpdateFeature;
using XmlFormatter.Domain.PluginFeature;
using XmlFormatter.Domain.PluginFeature.UpdateStrategyFeature;

namespace XmlFormatter.Infrastructure.Services.UpdateFeature
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
        private readonly IVersionReceiverStrategy localReceiver;

        /// <summary>
        /// The receiver to get the remote version
        /// </summary>
        private readonly IVersionReceiverStrategy remoteReceiver;

        /// <summary>
        /// Error event if something went wrong
        /// </summary>
        public event EventHandler<BaseEventArgs> Error;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="versionConvert">The strategy to use for version class to string converting</param>
        /// <param name="localReceiver">The receiver to use to get local version</param>
        /// <param name="remoteReceiver">The receiver to use to get remote version</param>
        public VersionManager(IVersionConvertStrategy versionConvert, IVersionReceiverStrategy localReceiver, IVersionReceiverStrategy remoteReceiver)
        {
            this.versionConvert = versionConvert;
            this.localReceiver = localReceiver;
            this.remoteReceiver = remoteReceiver;

            this.remoteReceiver.Error += Receiver_Error;
            this.localReceiver.Error += Receiver_Error;
        }

        /// <summary>
        /// Event if any of the receivers did throw one
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void Receiver_Error(object sender, BaseEventArgs e)
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
            return remoteReceiver.GetVersionAsync(versionConvert);
        }

        /// <inheritdoc/>
        public Task<Version> GetLocalVersionAsync()
        {
            return localReceiver.GetVersionAsync(versionConvert);
        }

        /// <inheritdoc/>
        public async Task<VersionCompare> RemoteVersionIsNewerAsync()
        {
            Version remoteVersion = await GetRemoteVersionAsync();
            Version localVersion = await GetLocalVersionAsync();
            int compareResult = localVersion.CompareTo(remoteVersion);
            IRelease latestRelease = await remoteReceiver.GetLatestReleaseAsync();

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

