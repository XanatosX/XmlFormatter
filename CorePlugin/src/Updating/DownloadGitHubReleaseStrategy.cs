using Octokit;
using PluginFramework.src.DataContainer;
using PluginFramework.src.Update;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;

namespace CorePlugin.src.Updating
{
    /// <summary>
    /// This class descripes the download from GitHub strategy
    /// </summary>
    class DownloadGitHubReleaseStrategy : BaseUpdate
    {
        /// <summary>
        /// Create a new instance of this strategy
        /// </summary>
        public DownloadGitHubReleaseStrategy() : base(new PluginInformation("Download GitHub releases", "Download the GitHub release", "XanatosX", new Version(1, 0)))
        {
        }

        /// <inheritdoc/>
        public override bool Update(VersionCompare versionInformation)
        {
            bool returnValue = true;
            string tempFolder = Path.GetTempPath();
            using (WebClient client = new WebClient())
            {
                foreach (ReleaseAsset release in versionInformation.Assets)
                {
                    string localFile = tempFolder;
                    string[] splittet = release.BrowserDownloadUrl.Split('/');
                    localFile += splittet.Last();
                    if (File.Exists(localFile))
                    {
                        try
                        {
                            using (FileStream fileStream = new FileStream(localFile, System.IO.FileMode.Open))
                            {
                                if (!fileStream.CanWrite)
                                {
                                    returnValue &= false;
                                    continue;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            returnValue &= false;
                            continue;
                        }
                    }
                    client.DownloadFile(release.BrowserDownloadUrl, localFile);
                    Process.Start(localFile);
                }
            }

            if (returnValue)
            {
                Process.Start(Environment.CurrentDirectory);
            }
            
            return returnValue;
        }
    }
}
