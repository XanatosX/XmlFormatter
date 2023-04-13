using System;
using System.IO;

namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Default implementation for the <see cref="IPathService"/>
/// </summary>
public class PathService : IPathService
{
    /// <inheritdoc/>
    public string GetSettingPath()
    {
        string settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return Path.Combine(settingsPath, "XmlFormatter");
    }

    /// <inheritdoc/>
    public string GetSettingsFile()
    {
        return Path.Combine(GetSettingPath(), "settings.set");
    }
}
