using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using CommunityToolkit.Mvvm.Messaging;
using System.Linq;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.Model.Messages;

namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Default implementation for the theme service
/// </summary>
internal class ThemeService : IThemeService
{
    /// <inheritdoc/>
    public void ChangeTheme(ThemeEnum newTheme)
    {
        WeakReferenceMessenger.Default.Send(new ThemeChangedMessage(newTheme == ThemeEnum.Light ? ThemeVariant.Light : ThemeVariant.Dark));
    }

    public Color GetColorForTheme(ThemeVariant themeVariant)
    {
        return themeVariant == ThemeVariant.Light ? Colors.White : Colors.Black;;
    }

}
