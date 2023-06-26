using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.Model;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.Services;

namespace XmlFormatterOsIndependent.ViewModels;
internal partial class ApplicationSettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private List<PluginMetaDataViewModel> availableUpdaters;

    [ObservableProperty]
    private PluginMetaDataViewModel? updater;

    /// <summary>
    /// Private acces for ask before closing setting
    /// </summary>
    [ObservableProperty]
    private bool askBeforeClosing;

    /// <summary>
    /// Private selected theme index
    /// </summary>
    [ObservableProperty]
    private int themeMode;

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

        ThemeMode = (int)settings.Theme;

        WeakReferenceMessenger.Default.Register<SaveSettingsWindowMessage>(this, (_, data) => SaveSettings(data));

        PropertyChanged += (_, data) =>
        {
            if (data.PropertyName == nameof(ThemeMode))
            {
                themeService.ChangeTheme(ThemeMode == 0 ? ThemeEnum.Light : ThemeEnum.Dark);
            }
        };
    }

    private void SaveSettings(SaveSettingsWindowMessage message)
    {
        settingFacadeService.UpdateSettings(settings =>
        {
            settings.AskBeforeClosing = AskBeforeClosing;
            settings.CheckForUpdatesOnStartup = CheckUpdateOnStart;
            settings.Theme = ThemeMode == 0 ? ThemeEnum.Light : ThemeEnum.Dark;
            settings.Updater = Updater?.MetaData;
        });

        ThemeEnum themeToUse = settingFacadeService.GetSettings()?.Theme ?? ThemeEnum.Light;
        themeService.ChangeTheme(themeToUse);
    }

    ~ApplicationSettingsViewModel()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }


}
