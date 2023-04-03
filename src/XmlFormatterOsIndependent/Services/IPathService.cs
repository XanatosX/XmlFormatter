namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Service to get the path of important folders and files of the application
/// </summary>
public interface IPathService
{
    /// <summary>
    /// Get the path to the setting folder
    /// </summary>
    /// <returns>The path to the settings folder</returns>
    string GetSettingPath();

    /// <summary>
    /// Get the path to the setting file
    /// </summary>
    /// <returns>The path to the setting file</returns>
    string GetSettingsFile();
}