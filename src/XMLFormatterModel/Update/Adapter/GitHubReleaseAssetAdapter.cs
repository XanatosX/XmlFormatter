using Octokit;

namespace XmlFormatterModel.Update.Adapter
{
    /// <summary>
    /// Adapter class for octokit asset and internal asset
    /// </summary>
    internal class GitHubReleaseAssetAdapter : IReleaseAsset
    {
        /// <summary>
        /// The octokit asset to use
        /// </summary>
        private readonly ReleaseAsset asset;

        /// <inheritdoc/>
        public string Url => asset.Url;

        /// <inheritdoc/>
        public int Id => asset.Id;

        /// <inheritdoc/>
        public string Name => asset.Name;

        /// <inheritdoc/>
        public string Label => asset.Label;

        /// <inheritdoc/>
        public int Size => asset.Size;

        /// <inheritdoc/>
        public string DownloadUrl => asset.BrowserDownloadUrl;

        /// <summary>
        /// Create a new instance of this adapter class
        /// </summary>
        /// <param name="asset">The octokit asset this is based on</param>
        public GitHubReleaseAssetAdapter(ReleaseAsset asset)
        {
            this.asset = asset;
        }


    }
}
