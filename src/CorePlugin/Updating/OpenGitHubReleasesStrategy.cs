using PluginFramework.DataContainer;
using PluginFramework.Update;
using System;
using XmlFormatterModel.Update;
using XmlFormatterOsIndependent.Helper;

namespace CorePlugin.Updating
{
    /// <summary>
    /// This class will open up the release GitHub page
    /// </summary>
    class OpenGitHubReleasesStrategy : BaseUpdate
    {
        /// <summary>
        /// Create a new instance of this strategy
        /// </summary>
        public OpenGitHubReleasesStrategy() : base(new PluginInformation("Open GitHub releases", "Open the GitHub Release", "XanatosX", new Version(1, 1)))
        {
        }

        /// <inheritdoc/>
        public override bool Update(VersionCompare versionInformation, Predicate<IReleaseAsset> assetFilter)
        {
            UrlOpener opener = new UrlOpener("https://github.com/XanatosX/XmlFormatter/releases/tag/" + versionInformation.LatestRelease.TagName);
            opener.OpenUrl();
            return true;
        }
    }
}
