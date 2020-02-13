using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlFormatter.src.DataContainer
{
    class VersionCompare
    {
        private readonly bool gitHubIsNewer;
        public bool GitHubIsNewer => gitHubIsNewer;

        private readonly Version localVersion;
        public Version LocalVersion => localVersion;

        private readonly Version gitHubVersion;
        public Version GitHubVersion => gitHubVersion;

        private readonly Release lastestRelease;
        public Release LatestRelease => LatestRelease;

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
            this.localVersion = localVersion;
            this.gitHubVersion = gitHubVersion;
            this.lastestRelease = latestRelease;
        }

    }
}
