using System;
using System.Threading.Tasks;
using XmlFormatter.Domain.PluginFeature;
using XmlFormatter.Domain.PluginFeature.UpdateStrategyFeature;

namespace XmlFormatterModel.Update
{
    /// <summary>
    /// This interface represents a version manager
    /// which can check if there are new versions available
    /// </summary>
    public interface IVersionManager
    {
        /// <summary>
        /// was there any error while trying to check for a new version
        /// </summary>
        event EventHandler<BaseEventArgs> Error;

        /// <summary>
        /// Get the version of the remote instance
        /// </summary>
        /// <returns>The remote version</returns>
        Task<Version> GetRemoteVersionAsync();

        /// <summary>
        /// Get the version of the local instance
        /// </summary>
        /// <returns>The local version</returns>
        Task<Version> GetLocalVersionAsync();

        /// <summary>
        /// Convert the given string to a proper version class
        /// </summary>
        /// <param name="stringVersion">The string version to convert</param>
        /// <returns>The proper string version</returns>
        Version ConvertStringToVersion(string stringVersion);

        /// <summary>
        /// Get the string version of a given version class
        /// </summary>
        /// <param name="version">The proper version class</param>
        /// <returns>The converted string</returns>
        string GetStringVersion(Version version);

        /// <summary>
        /// Tells you if the remote version is newer
        /// </summary>
        /// <returns>A version compare instance with additional information</returns>
        Task<VersionCompare> RemoteVersionIsNewerAsync();
    }
}
