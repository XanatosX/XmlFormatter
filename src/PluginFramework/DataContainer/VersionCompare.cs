using System;
using System.Collections.Generic;
using System.Linq;
using XmlFormatterModel.Update;

namespace PluginFramework.DataContainer
{
    /// <summary>
    /// This data container class which will combine different information.
    /// This class will tell you which version is newer and what versions are present
    /// </summary>
    public class VersionCompare
    {
        /// <summary>
        /// Readonly access if the GitHub version is newer
        /// </summary>
        public bool GitHubIsNewer { get; }

        /// <summary>
        /// Readonly access to the current client version of this application
        /// </summary>
        public Version LocalVersion { get; }

        /// <summary>
        /// Readonly access to the GitHub version
        /// </summary>
        public Version GitHubVersion { get; }

        /// <summary>
        /// Readonly access to the newest release on GitHub
        /// </summary>
        public IRelease LatestRelease { get; }

        /// <summary>
        /// Public access to the assets in the release build
        /// </summary>
        public List<IReleaseAsset> Assets { get; }

        /// <summary>
        /// Create an new instance of this class
        /// </summary>
        /// <param name="gitHubIsNewer">Is the version on GitHub newer</param>
        /// <param name="localVersion">The local version number</param>
        /// <param name="gitHubVersion">The GitHub version number</param>
        /// <param name="latestRelease">The latest release</param>
        public VersionCompare(bool gitHubIsNewer, Version localVersion, Version gitHubVersion, IRelease latestRelease)
        {
            GitHubIsNewer = gitHubIsNewer;
            LocalVersion = localVersion;
            GitHubVersion = gitHubVersion;
            LatestRelease = latestRelease;
            Assets = latestRelease.Assets.ToList();
        }

    }
}
