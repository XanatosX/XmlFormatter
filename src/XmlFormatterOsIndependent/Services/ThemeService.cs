using Avalonia;
using Avalonia.Themes.Fluent;
using System.Linq;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Default implementation for the theme service
/// </summary>
internal class ThemeService : IThemeService
{
    /// <inheritdoc/>
    public void ChangeTheme(ThemeEnum newTheme)
    {
        FluentThemeMode theme = newTheme switch
        {
            ThemeEnum.Light => FluentThemeMode.Light,
            ThemeEnum.Dark => FluentThemeMode.Dark,
            _ => FluentThemeMode.Light
        };

        ChangeTheme(theme);
    }

    /// <inheritdoc/>
    public void ChangeTheme(FluentThemeMode fluentTheme)
    {
        var app = Application.Current;
        if (app is not null)
        {
            var loadedTheme = app.Styles.OfType<FluentTheme>().FirstOrDefault();
            if (loadedTheme is not null)
            {
                loadedTheme.Mode = fluentTheme;
            }
        }
    }
}
