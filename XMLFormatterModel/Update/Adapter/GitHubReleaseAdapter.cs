using Octokit;
using System.Collections.Generic;

namespace XmlFormatterModel.Update.Adapter
{
    internal class GitHubReleaseAdapter : IRelease
    {
        private readonly Release release;

        public string Author => release.Author.Login;

        public string Name => release.Name;

        public string Url => release.Url;

        public string TagName => release.TagName;

        public IReadOnlyList<IReleaseAsset> Assets => assets;
        private readonly IReadOnlyList<IReleaseAsset> assets;

        public GitHubReleaseAdapter(Release release)
        {
            this.release = release;
            List<IReleaseAsset> newAssets = new List<IReleaseAsset>();
            foreach (ReleaseAsset asset in release.Assets)
            {
                newAssets.Add(new GitHubReleaseAssetAdapter(asset));
            }
            assets = newAssets.AsReadOnly();
        }
    }
}
