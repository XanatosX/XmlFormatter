using Avalonia.Themes.Fluent;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.Services;
public interface IThemeService
{
    void ChangeTheme(FluentThemeMode fluentTheme);

    void ChangeTheme(ThemeEnum newTheme);
}