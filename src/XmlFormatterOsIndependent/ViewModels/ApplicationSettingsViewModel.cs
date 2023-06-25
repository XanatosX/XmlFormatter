using CommunityToolkit.Mvvm.ComponentModel;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Enums;
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

    private readonly ISettingScope applicationScope;
    private readonly ISettingsManager settingsManager;

    public ApplicationSettingsViewModel(ISettingsManager settingsManager,
            IPathService pathService,
            IPluginManager pluginManager)
    {
        this.settingsManager = settingsManager;

        AvailableUpdaters = pluginManager.ListPlugins<IUpdateStrategy>()
                                         .OfType<PluginMetaData>()
                                         .Select(entry => new PluginMetaDataViewModel(entry))
                                         .ToList();

        Updater = AvailableUpdaters.FirstOrDefault();
        applicationScope = settingsManager.GetScope(Properties.Properties.Setting_Default_Scope);

        LoadSettings();
    }

    private void LoadSettings()
    {
        AskBeforeClosing = GetSettingsValue<bool>(Properties.Properties.Setting_Ask_Before_Closing_Key);
        CheckUpdateOnStart = GetSettingsValue<bool>(Properties.Properties.Setting_Search_Update_On_Startup_Key);

        var scope = settingsManager.GetScope(Properties.Properties.Setting_Default_Scope);
        string mode = scope.GetSetting(Properties.Properties.Setting_Theme_Key)?.GetValue<string>() ?? ThemeEnum.Light.ToString();
        Enum.TryParse(typeof(ThemeEnum), mode, out var theme);

        ThemeMode = (int)(theme ?? ThemeEnum.Light);
        string storedUpdater = GetSettingsValue<string>(Properties.Properties.Setting_Update_Strategy_Key);

        Updater = AvailableUpdaters?.FirstOrDefault(item => item.UpdaterType.ToString() == storedUpdater);
    }

    /// <summary>
    /// Get a specific settings value
    /// </summary>
    /// <typeparam name="T">Type of the value to get</typeparam>
    /// <param name="name">Name of the value to get</param>
    /// <returns>The value casted to the given type</returns>
    private T GetSettingsValue<T>(string name)
    {
        ISettingPair settingPair = applicationScope.GetSetting(name);
        return settingPair == null ? default : settingPair.GetValue<T>();
    }
}
