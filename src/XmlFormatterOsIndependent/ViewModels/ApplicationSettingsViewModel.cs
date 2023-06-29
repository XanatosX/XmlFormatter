using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.Model;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.Services;

namespace XmlFormatterOsIndependent.ViewModels;
internal partial class ApplicationSettingsViewModel : ObservableObject
{
    /// <summary>
    /// A list with all the available updaters
    /// </summary>
    [ObservableProperty]
    private List<PluginMetaDataViewModel> availableUpdaters;

    /// <summary>
    /// The currently selected updater
    /// </summary>
    [ObservableProperty]
    private PluginMetaDataViewModel? updater;

    /// <summary>
    /// A list with all the themes to choose from
    /// </summary>
    [ObservableProperty]
    private List<string> themes;

    /// <summary>
    /// The currently selected theme
    /// </summary>
    [ObservableProperty]
    private string selectedTheme;

    /// <summary>
    /// Private acces for ask before closing setting
    /// </summary>
    [ObservableProperty]
    private bool askBeforeClosing;

    /// <summary>
    /// Private check for updates on start
    /// </summary>
    [ObservableProperty]
    private bool checkUpdateOnStart;
    private readonly IThemeService themeService;
    private readonly SettingFacadeService settingFacadeService;

    public ApplicationSettingsViewModel(
            IThemeService themeService,
            IPluginManager pluginManager,
            SettingFacadeService settingFacadeService)
    {
        this.themeService = themeService;
        this.settingFacadeService = settingFacadeService;

        AvailableUpdaters = pluginManager.ListPlugins<IUpdateStrategy>()
                                         .OfType<PluginMetaData>()
                                         .Select(entry => new PluginMetaDataViewModel(entry))
                                         .ToList();

        Updater = AvailableUpdaters.FirstOrDefault();

        var settings = settingFacadeService.GetSettings(true) ?? new ApplicationSettings();

        AskBeforeClosing = settings.AskBeforeClosing;
        CheckUpdateOnStart = settings.CheckForUpdatesOnStartup;
        if (settings.Updater is not null)
        {
            Updater = AvailableUpdaters?.FirstOrDefault(item => item.UpdaterType == settings.Updater.Type);
        }

        Themes = Enum.GetValues<ThemeEnum>()
                     .Select(theme => theme.ToString())
                     .ToList();
        selectedTheme = settings.Theme.ToString();

        WeakReferenceMessenger.Default.Register<SaveSettingsWindowMessage>(this, (_, _) => SaveSettings());

        PropertyChanged += (_, data) =>
        {
            if (data.PropertyName == nameof(SelectedTheme))
            {
                ThemeEnum currentTheme = ThemeEnum.Light;
                Enum.TryParse(SelectedTheme, out currentTheme);
                themeService.ChangeTheme(currentTheme);
            }
        };
    }

    /// <summary>
    /// Save the settings for the application data
    /// </summary>
    private void SaveSettings()
    {
        settingFacadeService.UpdateSettings(settings =>
        {
            settings.AskBeforeClosing = AskBeforeClosing;
            settings.CheckForUpdatesOnStartup = CheckUpdateOnStart;
            ThemeEnum currentTheme = ThemeEnum.Light;
            Enum.TryParse(SelectedTheme, out currentTheme);
            settings.Theme = currentTheme;
            settings.Updater = Updater?.MetaData;
        });

        ThemeEnum themeToUse = settingFacadeService.GetSettings()?.Theme ?? ThemeEnum.Light;
        themeService.ChangeTheme(themeToUse);
    }

    /// <summary>
    /// Destructor for this user view, this is required to enforce unregister of message hook
    /// </summary>
    ~ApplicationSettingsViewModel()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }


}
