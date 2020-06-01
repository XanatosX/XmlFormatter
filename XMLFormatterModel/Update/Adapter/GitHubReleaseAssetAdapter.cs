using Octokit;
using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterModel.Update.Adapter
{
    internal class GitHubReleaseAssetAdapter : IReleaseAsset
    {
        private readonly ReleaseAsset asset;

        public string Url => asset.Url;

        public int Id => asset.Id;

        public string Name => asset.Name;

        public string Label => asset.Label;

        public int Size => asset.Size;

        public GitHubReleaseAssetAdapter(ReleaseAsset asset)
        {
            this.asset = asset;
        }


    }
}
