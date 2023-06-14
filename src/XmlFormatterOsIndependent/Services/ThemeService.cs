using Avalonia;
using Avalonia.Themes.Fluent;
using System.Linq;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.Services;
internal class ThemeService : IThemeService
{
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
