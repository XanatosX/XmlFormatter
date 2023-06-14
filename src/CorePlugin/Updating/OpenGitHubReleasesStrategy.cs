using PluginFramework.DataContainer;
using PluginFramework.Update;
using PluginFramework.Utils;
using System;
using XmlFormatterModel.Update;

namespace CorePlugin.Updating
{
    /// <summary>
    /// This class will open up the release GitHub page
    /// </summary>
    class OpenGitHubReleasesStrategy : BaseUpdate
    {
        /// <summary>
        /// The url opener to use
        /// </summary>
        private readonly UrlOpener urlOpener;

        /// <summary>
        /// Create a new instance of this strategy
        /// </summary>
        public OpenGitHubReleasesStrategy()
        {
            Information = new PluginInformation(
                "Open GitHub releases",
                "Open the latest github release in the browser",
                "XanatosX",
                new Version(1, 1, 3),
                "https://github.com/XanatosX",
                "https://github.com/XanatosX/XmlFormatter");
            Information.SetMarkdownDescription(LoadFromEmbeddedResource("CorePlugin.Resources.OpenGitHubDescription.md"));
            urlOpener = new UrlOpener();
        }

        /// <inheritdoc/>
        public override bool Update(VersionCompare versionInformation, Predicate<IReleaseAsset> assetFilter)
        {
            urlOpener?.OpenUrl("https://github.com/XanatosX/XmlFormatter/releases/tag/" + versionInformation.LatestRelease.TagName);
            return true;
        }
    }
}
