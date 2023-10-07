using PluginFramework.Update;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using XmlFormatter.Domain.PluginFeature;
using XmlFormatter.Domain.PluginFeature.UpdateStrategyFeature;

namespace CorePlugin.Updating
{
    /// <summary>
    /// This class describes the download from GitHub strategy
    /// </summary>
    class DownloadGitHubReleaseStrategy : BaseUpdate
    {
        /// <summary>
        /// Create a new instance of this strategy
        /// </summary>
        public DownloadGitHubReleaseStrategy()
        {
            Information = new PluginInformation("Download GitHub releases",
                                                "Download the github release as zip and open the file",
                                                "XanatosX",
                                                new Version(1, 1, 1),
                                                "https://github.com/XanatosX",
                                                "https://github.com/XanatosX/XmlFormatter");
            Information.SetMarkdownDescription(LoadFromEmbeddedResource("CorePlugin.Resources.DownloadGitHubDescription.md"));
        }

        /// <inheritdoc/>
        public override bool Update(VersionCompare versionInformation, Predicate<IReleaseAsset> assetFilter)
        {
            bool returnValue = true;
            int downloadCount = 0;
            string tempFolder = Path.GetTempPath();
            using (WebClient client = new WebClient())
            {
                foreach (IReleaseAsset release in versionInformation.Assets.FindAll(assetFilter))
                {
                    string localFile = tempFolder;
                    string[] splitted = release.DownloadUrl.Split('/');
                    localFile += splitted.Last();
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
                                downloadCount++;
                            }
                        }
                        catch (Exception)
                        {
                            returnValue &= false;
                            continue;
                        }
                    }
                    client.DownloadFile(release.DownloadUrl, localFile);
                    try
                    {
                        Process starter = new Process();
                        starter.StartInfo = new ProcessStartInfo(localFile)
                        {
                            UseShellExecute = true
                        };
                        starter.Start();
                    }
                    catch (Exception)
                    {
                        // Could not open downloaded artifact but this is fine
                    }
                }
            }

            if (returnValue && downloadCount > 0)
            {
                Process starter = new Process();
                starter.StartInfo = new ProcessStartInfo(Environment.CurrentDirectory)
                {
                    UseShellExecute = true
                };
                starter.Start();
            }

            return returnValue;
        }
    }
}
