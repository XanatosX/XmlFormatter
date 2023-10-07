using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using XmlFormatter.Application;
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
    /// Private access for ask before closing setting
    /// </summary>
    [ObservableProperty]
    private bool askBeforeClosing;

    /// <summary>
    /// Private check for updates on start
    /// </summary>
    [ObservableProperty]
    private bool checkUpdateOnStart;
    private readonly IThemeService themeService;
    private readonly ISettingsRepository<ApplicationSettings> settingsRepository;

    public ApplicationSettingsViewModel(
            IThemeService themeService,
            IPluginManager pluginManager,
            ISettingsRepository<ApplicationSettings> settingsRepository)
    {
        this.themeService = themeService;
        this.settingsRepository = settingsRepository;
        AvailableUpdaters = pluginManager.ListPlugins<IUpdateStrategy>()
                                         .OfType<PluginMetaData>()
                                         .Select(entry => new PluginMetaDataViewModel(entry))
                                         .ToList();
        Themes = Enum.GetValues<ThemeEnum>()
                     .Select(theme => theme.ToString())
                     .ToList();
        Updater = AvailableUpdaters.FirstOrDefault();
        PropertyChanged += (_, data) =>
        {
            if (data.PropertyName == nameof(SelectedTheme))
            {
                ThemeEnum currentTheme = ThemeEnum.Light;
                Enum.TryParse(SelectedTheme, out currentTheme);
                themeService.ChangeTheme(currentTheme);
            }
        };
        SelectedTheme = ThemeEnum.Light.ToString();
        SetSettingsFromApplicationSetting(settingsRepository.CreateOrLoad() ?? new ApplicationSettings());

        WeakReferenceMessenger.Default.Register<SettingsImportedMessage>(this, (_, data) => SetSettingsFromApplicationSetting(data.Value));
        WeakReferenceMessenger.Default.Register<SaveSettingsWindowMessage>(this, (_, _) => SaveSettings());
    }

    /// <summary>
    /// This method will set all the setting entries into the correct field
    /// </summary>
    /// <param name="applicationSettings">The application settings to load</param>
    private void SetSettingsFromApplicationSetting(ApplicationSettings applicationSettings)
    {
        if (applicationSettings is null)
        {
            return;
        }
        AskBeforeClosing = applicationSettings.AskBeforeClosing;
        CheckUpdateOnStart = applicationSettings.CheckForUpdatesOnStartup;
        SelectedTheme = applicationSettings.Theme.ToString();
        if (applicationSettings.Updater is not null)
        {
            Updater = AvailableUpdaters?.FirstOrDefault(item => item.UpdaterType == applicationSettings.Updater.Type);
        }
    }

    /// <summary>
    /// Save the settings for the application data
    /// </summary>
    private void SaveSettings()
    {
        var updatedSettings = settingsRepository.Update(settings =>
        {
            if (settings is null)
            {
                return;
            }
            settings.AskBeforeClosing = AskBeforeClosing;
            settings.CheckForUpdatesOnStartup = CheckUpdateOnStart;
            ThemeEnum currentTheme = ThemeEnum.Light;
            Enum.TryParse(SelectedTheme, out currentTheme);
            settings.Theme = currentTheme;
            settings.Updater = Updater?.MetaData;
        });

        ThemeEnum themeToUse = updatedSettings?.Theme ?? ThemeEnum.Light;
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
