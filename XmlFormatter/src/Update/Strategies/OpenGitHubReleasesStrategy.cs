using System.Diagnostics;
using XmlFormatter.src.DataContainer;
using XmlFormatter.src.Interfaces.Updates;

namespace XmlFormatter.src.Update.Strategies
{
    /// <summary>
    /// This class will open up the release GitHub page
    /// </summary>
    class OpenGitHubReleasesStrategy : IUpdateStrategy
    {
        /// <summary>
        /// The readonly name to show in the dropdown
        /// </summary>
        private readonly string displayName;

        /// <summary>
        /// The displayname to show in the dropdown
        /// </summary>
        public string DisplayName => displayName;

        /// <summary>
        /// Create a new instance of this strategy
        /// </summary>
        public OpenGitHubReleasesStrategy()
        {
            displayName = "Open GitHub releases";
        }

        /// <inheritdoc/>
        public bool Update(VersionCompare versionInformation)
        {
            Process.Start("https://github.com/XanatosX/XmlFormatter/releases/tag/" + versionInformation.LatestRelease.TagName);
            return true;
        }
    }
}
