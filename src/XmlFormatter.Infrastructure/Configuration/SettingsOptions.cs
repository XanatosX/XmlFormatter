namespace XmlFormatter.Infrastructure.Configuration;

public class SettingsOptions
{
    public string SettingDirectory { get; }
    public string SettingName { get; }

    public SettingsOptions(string settingDirectory, string settingName)
    {
        SettingDirectory = settingDirectory;
        SettingName = settingName;
    }
    public string GetSettingPath()
    {
        return Path.Combine(SettingDirectory, SettingName);
    }
}