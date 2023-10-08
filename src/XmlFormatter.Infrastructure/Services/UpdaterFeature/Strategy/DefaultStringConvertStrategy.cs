using System.Text.RegularExpressions;
using XmlFormatter.Application.Services.UpdateFeature;

namespace XmlFormatter.Infrastructure.Services.UpdaterFeature.Strategy
{
    /// <summary>
    /// This class will convert the strings in a simple way
    /// </summary>
    public class DefaultStringConvertStrategy : IVersionConvertStrategy
    {
        /// <inheritdoc/>
        public Version ConvertStringToVersion(string version)
        {
            if (version.Count(v => v == '.') < 3)
            {
                version += ".0";
            }

            Regex regex = new Regex(@"[0-9]{1,}.[0-9]{1,}.[0-9]{1,}");
            if (!regex.IsMatch(version))
            {
                version = "0.0.0.0";
            }
            Version returnVersion = new Version(version);

            return returnVersion;
        }

        /// <inheritdoc/>
        public string GetStringVersion(Version version)
        {
            return version.Major + "." + version.Minor + "." + version.Build;
        }
    }
}
