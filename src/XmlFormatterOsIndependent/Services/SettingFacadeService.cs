using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Linq;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.Model;

namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Facade class to simplify working with the fixed application settings
/// </summary>
internal class SettingFacadeService
{
    /// <summary>
    /// The settings manager to use
    /// </summary>
    private readonly ISettingsManager settingsManager;

    /// <summary>
    /// The path service used to get the path to the save file
    /// </summary>
    private readonly IPathService pathService;

    /// <summary>
    /// The plugin manager used to load available plugins
    /// </summary>
    private readonly IPluginManager pluginManager;

    /// <summary>
    /// Current instance of the application settings,
    /// this is working as a cache
    /// </summary>
    private ApplicationSettings? settingsCache;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="settingsManager">The settings manager to use for saving and loading of settings</param>
    /// <param name="pathService">The path service to use which does provide the path to the settings file</param>
    /// <param name="pluginManager">The plugin manager used to check for loaded plugins</param>
    public SettingFacadeService(
        ISettingsManager settingsManager,
        IPathService pathService,
        IPluginManager pluginManager)
    {
        this.settingsManager = settingsManager;
        this.pathService = pathService;
        this.pluginManager = pluginManager;
    }

    /// <summary>
    /// Get the current application settings, does return cache if data was already loaded
    /// </summary>
    /// <returns>The application settings or null if loading did fail</returns>
    public ApplicationSettings? GetSettings() => GetSettings(false);

    /// <summary>
    /// Get the current application settings, allows to force reload them from disc
    /// </summary>
    /// <param name="forceReload">Should the settings be loaded even if the are loaded in the cache</param>
    /// <returns>The application settings or null if loading did fail</returns>
    public ApplicationSettings? GetSettings(bool forceReload)
    {
        bool settingWasLoaded = settingsCache is not null;
        settingsCache ??= LoadSettings();
        if (forceReload && settingWasLoaded)
        {
            settingsCache = LoadSettings();
        }
        return settingsCache;
    }

/// <summary>
    /// Get the current application settings, allows to force reload them from disc
    /// </summary>
    /// <param name="path">The path to load the settings from</param>
    /// <returns>The application settings or null if loading did fail</returns>
    public ApplicationSettings? GetSettings(string path)
    {
        bool settingWasLoaded = settingsCache is not null;
        return LoadSettings(path);
    }

    /// <summary>
    /// Load the settings and parse them into a application settings class
    /// </summary>
    /// <returns>If loading was successful a valid application settings class will be returned</returns>
    private ApplicationSettings? LoadSettings() => LoadSettings(pathService.GetSettingsFile());

        /// <summary>
    /// Load the settings and parse them into a application settings class
    /// </summary>
    /// <returns>If loading was successful a valid application settings class will be returned</returns>
    private ApplicationSettings? LoadSettings(string path)
    {
        settingsManager.Load(path);
        ISettingScope? applicationScope = settingsManager.GetScope(Properties.Properties.Setting_Default_Scope);
        if (applicationScope is null)
        {
            return null;
        }

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

    /// <summary>
    /// Update the setting for the application
    /// </summary>
    /// <param name="updateSettings">The update action</param>
    /// <returns>The updated settings if saving was completed successfully</returns>
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

    /// <summary>
    /// Save the given settings to the disc
    /// </summary>
    /// <param name="settings">The settings which should be saved</param>
    /// <returns>The newly saved settings</returns>
    public ApplicationSettings SaveSettings(ApplicationSettings settings)=> SaveSettings(settings, pathService.GetSettingsFile());

    /// <summary>
    /// Save the given settings to the disc
    /// </summary>
    /// <param name="settings">The settings which should be saved</param>
    /// <param name="path">The path to save the setting to</param>
    /// <returns>The newly saved settings</returns>
    public ApplicationSettings SaveSettings(ApplicationSettings settings, string path)
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

        settingsManager.Save(path);
        return settings;
    }

}
