using Avalonia.Media;
using Avalonia.Styling;
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
    
    /// <summary>
    /// Get the color for the provided theme variant
    /// </summary>
    /// <param name="themeVariant">The theme variant to get the color for</param>
    /// <returns>The color of the given theme variant</returns>
    Color GetColorForTheme(ThemeVariant themeVariant);

    /// <summary>
    /// Get the current used theme variant of the application
    /// </summary>
    /// <returns>The currently used theme variant</returns>
    ThemeVariant GetCurrentThemeVariant();

    /// <summary>
    /// Get the currently used theme variant converted to the theme enum
    /// </summary>
    /// <returns>The currently used theme variant converted</returns>
    ThemeEnum GetCurrentAppTheme();
}