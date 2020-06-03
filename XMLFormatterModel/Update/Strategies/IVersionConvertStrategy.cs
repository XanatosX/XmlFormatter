using System;

namespace XmlFormatterModel.Update
{
    /// <summary>
    /// This interface describes the strategy to convert
    /// versions between string and the version class
    /// </summary>
    public interface IVersionConvertStrategy
    {
        /// <summary>
        /// Get the string version of a given version
        /// </summary>
        /// <param name="version">The version to convert</param>
        /// <returns>The resulting string</returns>
        string GetStringVersion(Version version);

        /// <summary>
        /// Convert a string to a proper version class
        /// </summary>
        /// <param name="version">The string version to convert</param>
        /// <returns>The proper version class</returns>
        Version ConvertStringToVersion(string version);
    }
}
