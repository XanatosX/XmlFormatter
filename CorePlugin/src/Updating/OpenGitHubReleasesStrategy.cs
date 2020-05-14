using PluginFramework.src.DataContainer;
using PluginFramework.src.Interfaces.PluginTypes;
using System;
using System.Diagnostics;

namespace CorePlugin.src.Updating
{
    /// <summary>
    /// This class will open up the release GitHub page
    /// </summary>
    class OpenGitHubReleasesStrategy : IUpdateStrategy
    {
        /// <inheritdoc/>
        public PluginInformation Information => information;

        /// <summary>
        /// Private readonly information about this plugin
        /// </summary>
        private readonly PluginInformation information;

        /// <summary>
        /// Create a new instance of this strategy
        /// </summary>
        public OpenGitHubReleasesStrategy()
        {
            information = new PluginInformation("Open GitHub releases", "Open the GitHub Release", "XanatosX", new Version(1, 0));
        }

        /// <inheritdoc/>
        public bool Update(VersionCompare versionInformation)
        {
            Process.Start("https://github.com/XanatosX/XmlFormatter/releases/tag/" + versionInformation.LatestRelease.TagName);
            return true;
        }
    }
}
