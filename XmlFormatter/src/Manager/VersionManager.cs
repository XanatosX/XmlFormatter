using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XmlFormatter.src.DataContainer;
using XmlFormatter.src.EventMessages;

namespace XmlFormatter.src.Manager
{
    class VersionManager
    {
        public event EventHandler<BaseEventArgs> Error;

        /// <summary>
        /// This method will return you the current version of the application
        /// </summary>
        /// <returns>The current version of the application</returns>
        public Version GetApplicationVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string versionString = "0.0.0";
            using (Stream stream = assembly.GetManifestResourceStream("XmlFormatter.Version.txt"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    versionString = reader.ReadLine();
                }
            }

            return ConvertInnerFormatToProperVersion(versionString);
        }

        /// <summary>
        /// This method will convert a inner string version to a proper version instance
        /// </summary>
        /// <param name="stringVersion">The string version to use for converting</param>
        /// <returns>A proper version class</returns>
        public Version ConvertInnerFormatToProperVersion(string stringVersion)
        {
            stringVersion += ".0";
            Regex regex = new Regex(@"[0-9]{1,}.[0-9]{1,}.[0-9]{1,}");
            if (!regex.IsMatch(stringVersion))
            {
                stringVersion = "0.0.0.0";
            }
            Version version = new Version(stringVersion);

            return version;
        }

        /// <summary>
        /// This method will return you the string of a given version
        /// </summary>
        /// <param name="version">The version to get the string for</param>
        /// <returns>A string representatiion of the given version</returns>
        public string GetStringVersion(Version version)
        {
            return version.Major + "." + version.Minor + "." + version.Build;
        }

        /// <summary>
        /// This method will return you an dataset to check the server and local version
        /// </summary>
        /// <returns></returns>
        public async Task<VersionCompare> GitHubVersionIsNewer()
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("XanatosX"));
            var releases = await client.Repository.Release.GetAll("XanatosX", "XmlFormatter");
            if (releases.Count == 0)
            {
                ThrowError("No version found", "Could't not find a version on GitHub");
            }
            Release latestRelease = releases[0];
            string latest = latestRelease.Name;
            Regex regex = new Regex(@"([0-9]{1,}.[0-9]{1,}.[0-9]{1,})");
            Version gitHubVersion = null;
            foreach (Match match in regex.Matches(latest))
            {
                gitHubVersion = ConvertInnerFormatToProperVersion(match.Value);
            }
            Version applicationVersion = GetApplicationVersion();
            int compareResult = applicationVersion.CompareTo(gitHubVersion);

            return new VersionCompare(compareResult < 0, applicationVersion, gitHubVersion, latestRelease);
        }

        /// <summary>
        /// Throw an error from this class
        /// </summary>
        /// <param name="title">The title of the error</param>
        /// <param name="message">The message of the error</param>
        private void ThrowError(string title, string message)
        {
            BaseEventArgs data = new BaseEventArgs(title, message);
            ThrowError(data);
        }

        /// <summary>
        /// Throw an error from this class
        /// </summary>
        /// <param name="baseEventArgs">The arguments to throw the event for</param>
        private void ThrowError(BaseEventArgs baseEventArgs)
        {
            EventHandler<BaseEventArgs> handler = Error;
            handler?.Invoke(this, baseEventArgs);
        }
    }
}
