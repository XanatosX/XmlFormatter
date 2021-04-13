using Octokit;
using System.Collections.Generic;

namespace XmlFormatterModel.Update.Adapter
{
    /// <summary>
    /// Adapter class between octokit release and internal release
    /// </summary>
    internal class GitHubReleaseAdapter : IRelease
    {
        /// <summary>
        /// The octokit release 
        /// </summary>
        private readonly Release release;

        /// <inheritdoc/>
        public string Author => release.Author.Login;

        /// <inheritdoc/>
        public string Name => release.Name;

        /// <inheritdoc/>
        public string Url => release.Url;

        /// <inheritdoc/>
        public string TagName => release.TagName;

        /// <inheritdoc/>
        public IReadOnlyList<IReleaseAsset> Assets { get; }

        /// <summary>
        /// Create a new instance of this adapter
        /// </summary>
        /// <param name="release">The octokit release this is based on</param>
        public GitHubReleaseAdapter(Release release)
        {
            this.release = release;
            List<IReleaseAsset> newAssets = new List<IReleaseAsset>();
            foreach (ReleaseAsset asset in release.Assets)
            {
                newAssets.Add(new GitHubReleaseAssetAdapter(asset));
            }
            Assets = newAssets.AsReadOnly();
        }
    }
}
