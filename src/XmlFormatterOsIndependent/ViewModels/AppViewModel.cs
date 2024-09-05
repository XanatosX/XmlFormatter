using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using XmlFormatter.Application;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.Model;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.Services;

internal partial class AppViewModel : ObservableObject
{
        /// <summary>
    /// The theme variant to use
    /// </summary>
    [ObservableProperty]
    private ThemeVariant themeVariant;

    public AppViewModel(ISettingsRepository<ApplicationSettings> settingsRepository)
    {
        var settings = settingsRepository.CreateOrLoad();
        var settingTheme = settings?.Theme;

        themeVariant = settingTheme == ThemeEnum.Light ? ThemeVariant.Light : ThemeVariant.Dark;

        WeakReferenceMessenger.Default.Register<GetCurrentThemeMessage>(this, (_, e) => {
            if (e.HasReceivedResponse)
            {
                return;
            }
            e.Reply(ThemeVariant);
        });

        WeakReferenceMessenger.Default.Register<ThemeChangedMessage>(this, (_, e) => {
            ThemeVariant = e.Value;
        });
    }

    ~AppViewModel()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }
}