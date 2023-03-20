using System;
using System.IO;

namespace XmlFormatterOsIndependent.Services;
public class PathService : IPathService
{
    public string GetSettingPath()
    {
        string settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return Path.Combine(settingsPath, "XmlFormatter");
    }

    public string GetSettingsFile()
    {
        return Path.Combine(GetSettingPath(), "settings.set");
    }
}
