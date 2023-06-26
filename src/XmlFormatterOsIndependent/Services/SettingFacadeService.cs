using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Linq;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.Model;

namespace XmlFormatterOsIndependent.Services;

internal class SettingFacadeService
{
    private readonly ISettingsManager settingsManager;
    private readonly IPathService pathService;
    private readonly IPluginManager pluginManager;
    private ApplicationSettings? settings;

    public SettingFacadeService(
        ISettingsManager settingsManager,
        IPathService pathService,
        IPluginManager pluginManager)
    {
        this.settingsManager = settingsManager;
        this.pathService = pathService;
        this.pluginManager = pluginManager;
    }

    public ApplicationSettings? GetSettings() => GetSettings(false);

    public ApplicationSettings? GetSettings(bool forceReload)
    {
        bool settingsMissing = settings is null;
        settings ??= LoadSettings();
        if (forceReload && settingsMissing)
        {
            settings = LoadSettings();
        }
        return settings;
    }

    private ApplicationSettings? LoadSettings()
    {
        settingsManager.Load(pathService.GetSettingsFile());
        ISettingScope applicationScope = settingsManager.GetScope(Properties.Properties.Setting_Default_Scope);

        bool askBeforeClosing = GetSettingsValue<bool>(applicationScope, Properties.Properties.Setting_Ask_Before_Closing_Key);
        bool checkUpdateOnStart = GetSettingsValue<bool>(applicationScope, Properties.Properties.Setting_Search_Update_On_Startup_Key);

        string mode = applicationScope.GetSetting(Properties.Properties.Setting_Theme_Key)?.GetValue<string>() ?? ThemeEnum.Light.ToString();
        Enum.TryParse(typeof(ThemeEnum), mode, out var theme);

        string storedUpdater = GetSettingsValue<string>(applicationScope, Properties.Properties.Setting_Update_Strategy_Key);
        PluginMetaData? updater = pluginManager.ListPlugins<IUpdateStrategy>()
                                         .OfType<PluginMetaData>()
                                         .ToList().FirstOrDefault(item => item.Type.ToString() == storedUpdater);
        ThemeEnum realTheme = ThemeEnum.Light;
        if (theme is ThemeEnum loadedTheme)
        {
            realTheme = loadedTheme;
        }

        return new ApplicationSettings
        {
            AskBeforeClosing = askBeforeClosing,
            CheckForUpdatesOnStartup = checkUpdateOnStart,
            Theme = realTheme,
            Updater = updater
        };
    }

    /// <summary>
    /// Get a specific settings value
    /// </summary>
    /// <typeparam name="T">Type of the value to get</typeparam>
    /// <param name="name">Name of the value to get</param>
    /// <returns>The value casted to the given type</returns>
    private T GetSettingsValue<T>(ISettingScope scope, string name)
    {
        ISettingPair settingPair = scope.GetSetting(name);
        return settingPair == null ? default : settingPair.GetValue<T>();
    }

    public ApplicationSettings? UpdateSettings(Action<ApplicationSettings> updateSettings)
    {
        ApplicationSettings? loadedSettings = GetSettings(true);
        updateSettings(loadedSettings ?? new ApplicationSettings());
        if (loadedSettings is not null)
        {
            SaveSettings(loadedSettings);
        }

        return loadedSettings;
    }

    public ApplicationSettings SaveSettings(ApplicationSettings settings)
    {
        ISettingPair askClose = new SettingPair(Properties.Properties.Setting_Ask_Before_Closing_Key);
        ISettingPair searchUpdate = new SettingPair(Properties.Properties.Setting_Search_Update_On_Startup_Key);
        ISettingPair updater = new SettingPair(Properties.Properties.Setting_Update_Strategy_Key);
        ISettingPair themeMode = new SettingPair(Properties.Properties.Setting_Theme_Key);
        askClose.SetValue(settings.AskBeforeClosing);
        searchUpdate.SetValue(settings.CheckForUpdatesOnStartup);
        updater.SetValue(settings.Updater?.Type.ToString());
        themeMode.SetValue(settings.Theme.ToString());

        settingsManager.Load(pathService.GetSettingsFile());
        ISettingScope applicationScope = settingsManager.GetScope(Properties.Properties.Setting_Default_Scope);
        applicationScope.AddSetting(askClose);
        applicationScope.AddSetting(searchUpdate);
        applicationScope.AddSetting(updater);
        applicationScope.AddSetting(themeMode);


        settingsManager.Save(pathService.GetSettingsFile());
        return settings;
    }

}
