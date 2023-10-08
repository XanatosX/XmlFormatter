namespace XmlFormatter.Infrastructure.Configuration;

/// <summary>
/// Class with information about the settings directory and file
/// </summary>
public class SettingsOptions
{
    /// <summary>
    /// Path to the setting directory
    /// </summary>
    public string SettingDirectory { get; }

    /// <summary>
    /// The name of the setting file
    /// </summary>
    public string SettingName { get; }

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="settingDirectory">Path to the setting directory</param>
    /// <param name="settingName">The name of the setting file</param>
    public SettingsOptions(string settingDirectory, string settingName)
    {
        SettingDirectory = settingDirectory;
        SettingName = settingName;
    }

    /// <summary>
    /// Get the full path to the setting file
    /// </summary>
    /// <returns>The path to the setting file</returns>
    public string GetSettingPath()
    {
        return Path.Combine(SettingDirectory, SettingName);
    }
}