using CorePlugin.Assets;
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
            ResourceLoader loader = new ResourceLoader();

            string description = loader.LoadResource("Updating.OpenGitHubDescription.txt");
            Information = new PluginInformation("Open GitHub releases", description, "XanatosX", new Version(1, 1, 2));
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
