using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PluginFramework.DataContainer
{
    /// <summary>
    /// This data container class which will combine different informations.
    /// This class will tell you which version is newer and what versions are present
    /// </summary>
    public class VersionCompare
    {
        /// <summary>
        /// Is the GitHub version newer
        /// </summary>
        private readonly bool gitHubIsNewer;

        /// <summary>
        /// Readonly acces if the GitHub version is newer
        /// </summary>
        public bool GitHubIsNewer => gitHubIsNewer;

        /// <summary>
        /// The version of the current client application 
        /// </summary>
        private readonly Version clientVersion;

        /// <summary>
        /// Readonly access to the current client version of this application
        /// </summary>
        public Version LocalVersion => clientVersion;

        /// <summary>
        /// The version on GitHub
        /// </summary>
        private readonly Version gitHubVersion;

        /// <summary>
        /// Readonly access to the GitHub version
        /// </summary>
        public Version GitHubVersion => gitHubVersion;

        /// <summary>
        /// The newest release which can be found on GitHub
        /// </summary>
        private readonly Release lastestRelease;

        /// <summary>
        /// Readonly access to the newest release on GitHub
        /// </summary>
        public Release LatestRelease => lastestRelease;

        /// <summary>
        /// A readonly list of all the assets in the release build
        /// </summary>
        private readonly List<ReleaseAsset> assets;

        /// <summary>
        /// Public access to the assets in the release build
        /// </summary>
        public List<ReleaseAsset> Assets => assets;

        /// <summary>
        /// Create an new instance of this class
        /// </summary>
        /// <param name="gitHubIsNewer">Is the version on GitHub newer</param>
        /// <param name="localVersion">The local version number</param>
        /// <param name="gitHubVersion">The GitHub version number</param>
        /// <param name="latestRelease">The latest release</param>
        public VersionCompare(bool gitHubIsNewer, Version localVersion, Version gitHubVersion, Release latestRelease)
        {
            this.gitHubIsNewer = gitHubIsNewer;
            clientVersion = localVersion;
            this.gitHubVersion = gitHubVersion;
            lastestRelease = latestRelease;
            assets = latestRelease.Assets.ToList();
        }

    }
}
