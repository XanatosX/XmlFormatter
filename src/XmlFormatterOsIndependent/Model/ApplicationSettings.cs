using PluginFramework.DataContainer;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.Model;

/// <summary>
/// Class to represent the baked settings for this application
/// </summary>
internal class ApplicationSettings
{
    /// <summary>
    /// Should we check for updates on startup
    /// </summary>
    public bool CheckForUpdatesOnStartup;

    /// <summary>
    /// Show a confirmation dialog if the application is getting closed
    /// </summary>
    public bool AskBeforeClosing;

    /// <summary>
    /// The current updater to use with the application
    /// </summary>
    public PluginMetaData? Updater;

    /// <summary>
    /// The current theme for the application
    /// </summary>
    public ThemeEnum Theme;
}
