using PluginFramework.DataContainer;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.Model;

internal class ApplicationSettings
{
    public bool CheckForUpdatesOnStartup;
    public bool AskBeforeClosing;

    public PluginMetaData? Updater;
    public ThemeEnum Theme;
}
