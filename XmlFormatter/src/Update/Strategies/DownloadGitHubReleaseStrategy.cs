using Octokit;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using XmlFormatter.src.DataContainer;
using XmlFormatter.src.Interfaces.Updates;

namespace XmlFormatter.src.Update.Strategies
{
    /// <summary>
    /// This class descripes the download from GitHub strategy
    /// </summary>
    class DownloadGitHubReleaseStrategy : IUpdateStrategy
    {
        /// <summary>
        /// The readonly name to show in the dropdown
        /// </summary>
        private readonly string displayName;

        /// <summary>
        /// The displayname to show in the dropdown
        /// </summary>
        public string DisplayName => displayName;

        /// <summary>
        /// Create a new instance of this strategy
        /// </summary>
        public DownloadGitHubReleaseStrategy()
        {
            displayName = "Download GitHub releases";
        }

        /// <inheritdoc/>
        public bool Update(VersionCompare versionInformation)
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
