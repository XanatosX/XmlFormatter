using System.Collections.Generic;

namespace XmlFormatterModel.Setting
{
    /// <summary>
    /// This interface represents a loading provider
    /// </summary>
    public interface ISettingLoadProvider
    {
        /// <summary>
        /// Load the settings from a given file
        /// </summary>
        /// <param name="filePath">The file to get the settings from</param>
        /// <returns>A list with all the scopes in the file</returns>
        List<ISettingScope> LoadSettings(string filePath);
    }
}
