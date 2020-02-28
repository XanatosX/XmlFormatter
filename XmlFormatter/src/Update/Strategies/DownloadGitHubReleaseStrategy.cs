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
    class DownloadGitHubReleaseStrategy : IUpdateStrategy
    {
        private readonly string displayName;
        public string DisplayName => displayName;

        public DownloadGitHubReleaseStrategy()
        {
            displayName = "Download GitHub releases";
        }

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
