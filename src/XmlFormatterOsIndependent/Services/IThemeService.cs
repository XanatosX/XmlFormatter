using Avalonia.Themes.Fluent;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Service interface to change the theme of the application
/// </summary>
public interface IThemeService
{
    /// <summary>
    /// Change the theme to the provided theme
    /// </summary>
    /// <param name="newTheme">The theme to use</param>
    void ChangeTheme(ThemeEnum newTheme);
}