using PluginFramework.src.DataContainer;
using PluginFramework.src.Update;
using System;
using System.Diagnostics;

namespace CorePlugin.src.Updating
{
    /// <summary>
    /// This class will open up the release GitHub page
    /// </summary>
    class OpenGitHubReleasesStrategy : BaseUpdate
    {
        /// <summary>
        /// Create a new instance of this strategy
        /// </summary>
        public OpenGitHubReleasesStrategy() : base(new PluginInformation("Open GitHub releases", "Open the GitHub Release", "XanatosX", new Version(1, 0)))
        {
        }

        /// <inheritdoc/>
        public override bool Update(VersionCompare versionInformation)
        {
            Process.Start("https://github.com/XanatosX/XmlFormatter/releases/tag/" + versionInformation.LatestRelease.TagName);
            return true;
        }
    }
}
