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
        private readonly string displayName;
        public string DisplayName => displayName;

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
