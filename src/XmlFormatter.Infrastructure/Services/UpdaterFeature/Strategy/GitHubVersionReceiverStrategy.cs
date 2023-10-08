using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XmlFormatter.Application.Services.UpdateFeature;
using XmlFormatter.Domain.Enums;
using XmlFormatter.Domain.PluginFeature;
using XmlFormatter.Domain.PluginFeature.UpdateStrategyFeature;
using XmlFormatterModel.Update.Adapter;

namespace XmlFormatterModel.Update.Strategies
{
    /// <summary>
    /// Get the newest version from GitHub
    /// </summary>
    public class GitHubVersionReceiverStrategy : IVersionReceiverStrategy
    {
        /// <inheritdoc/>
        public ScopeEnum Scope => ScopeEnum.Remote;

        /// <inheritdoc/>
        public event EventHandler<BaseEventArgs> Error;

        /// <summary>
        /// The regex used to correctly extract the version number
        /// </summary>
        private readonly Regex regex;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public GitHubVersionReceiverStrategy()
        {
            regex = new Regex(@"([0-9]{1,}.[0-9]{1,}.[0-9]{1,})");
        }

        /// <inheritdoc/>
        public async Task<List<IRelease>> GetReleasesAsync()
        {
            List<IRelease> returnReleases = new List<IRelease>();
            GitHubClient client = new GitHubClient(new ProductHeaderValue("XanatosX"));
            IReadOnlyList<Release> releases = await client.Repository.Release.GetAll("XanatosX", "XmlFormatter");
            foreach (Release gitHubRelease in releases)
            {
                returnReleases.Add(new GitHubReleaseAdapter(gitHubRelease));
            }
            return returnReleases;
        }

        /// <inheritdoc/>
        public async Task<IRelease> GetLatestReleaseAsync()
        {
            List<IRelease> releases = await GetReleasesAsync();
            if (releases.Count == 0)
            {
                EventHandler<BaseEventArgs> handler = Error;
                handler?.Invoke(this, new BaseEventArgs("No version found", "Could't not find a version on GitHub"));
                return null;
            }

            List<IRelease> correctTags = releases.Where(release => regex.IsMatch(release.TagName)).ToList();
            IRelease latestRelease = correctTags.Count > 0 ? correctTags[0] : null;
            return latestRelease;
        }

        /// <inheritdoc/>
        public async Task<Version> GetVersionAsync(IVersionConvertStrategy convertStrategy)
        {
            Version gitHubVersion = null;
            IRelease latestRelease = await GetLatestReleaseAsync();
            if (latestRelease != null)
            {
                string version = regex.Match(latestRelease.TagName).Value;
                gitHubVersion = convertStrategy.ConvertStringToVersion(version);
            }
            return gitHubVersion;
        }
    }
}
