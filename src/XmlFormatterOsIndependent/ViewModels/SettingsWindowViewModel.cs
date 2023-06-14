using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.Services;

namespace XmlFormatterOsIndependent.ViewModels
{
    /// <summary>
    /// View model class for the settings window
    /// </summary>
    public partial class SettingsWindowViewModel : ObservableObject
    {

        /// <summary>
        /// A list with all the update strategies
        /// </summary>
        public List<PluginMetaData> List { get; }

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

        /// <summary>
        /// Private updater to use
        /// </summary>
        [ObservableProperty]
        private PluginMetaData updater;

        /// <summary>
        /// Private index of the selected updater
        /// </summary>
        [ObservableProperty]
        private int updaterIndex;

        /// <summary>
        /// The setting scope of this application
        /// </summary>
        private readonly ISettingScope applicationScope;
        private readonly ISettingsManager settingsManager;
        private readonly IPathService pathService;
        private readonly IWindowApplicationService applicationService;
        private readonly IThemeService themeService;

        /// <summary>
        /// Create a new instance of this view
        /// </summary>
        /// <param name="view">The view for this model</param>
        /// <param name="settingsManager">The settings manager to use</param>
        /// <param name="pluginManager">The plugin manager to use</param>
        public SettingsWindowViewModel(
            ISettingsManager settingsManager,
            IPathService pathService,
            IPluginManager pluginManager,
            IWindowApplicationService applicationService,
            IThemeService themeService)
        {
            this.settingsManager = settingsManager;
            this.pathService = pathService;
            this.applicationService = applicationService;
            this.themeService = themeService;

            if (settingsManager == null || pluginManager == null)
            {
                return;
            }
            settingsManager.Load(pathService.GetSettingsFile());
            applicationScope = settingsManager.GetScope(Properties.Properties.Setting_Default_Scope);
            List = pluginManager.ListPlugins<IUpdateStrategy>().ToList();

            if (applicationScope == null)
            {
                applicationScope = new SettingScope(Properties.Properties.Setting_Default_Scope);
                settingsManager.AddScope(applicationScope);
            }
            LoadSettings();

        }

        /// <summary>
        /// Load all the settings
        /// </summary>
        private void LoadSettings()
        {
            AskBeforeClosing = GetSettingsValue<bool>(Properties.Properties.Setting_Ask_Before_Closing_Key);
            CheckUpdateOnStart = GetSettingsValue<bool>(Properties.Properties.Setting_Search_Update_On_Startup_Key);

            var scope = settingsManager.GetScope(Properties.Properties.Setting_Default_Scope);
            string mode = scope.GetSetting(Properties.Properties.Setting_Theme_Key)?.GetValue<string>() ?? ThemeEnum.Light.ToString();
            Enum.TryParse(typeof(ThemeEnum), mode, out var theme);

            ThemeMode = (int)(theme ?? ThemeEnum.Light);

            string updater = GetSettingsValue<string>(Properties.Properties.Setting_Update_Strategy_Key);
            int updaterToUse = -1;
            if (List.Count > 0)
            {
                updaterToUse = 0;
            }

            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].Type.ToString() == updater)
                {
                    updaterToUse = i;
                    break;
                }
            }

            if (updaterToUse >= 0)
            {
                UpdaterIndex = updaterToUse;
            }
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

        [RelayCommand]
        public void CloseWindow()
        {
            applicationService.CloseActiveWindow();
        }

        /// <summary>
        /// Save the settings and close this window
        /// </summary>
        public void SaveAndClose()
        {
            ThemeEnum theme = ThemeMode == 0 ? ThemeEnum.Light : ThemeEnum.Dark;

            ISettingPair askClose = new SettingPair(Properties.Properties.Setting_Ask_Before_Closing_Key);
            ISettingPair searchUpdate = new SettingPair(Properties.Properties.Setting_Search_Update_On_Startup_Key);
            ISettingPair updater = new SettingPair(Properties.Properties.Setting_Update_Strategy_Key);
            ISettingPair themeMode = new SettingPair(Properties.Properties.Setting_Theme_Key);
            askClose.SetValue(AskBeforeClosing);
            searchUpdate.SetValue(CheckUpdateOnStart);
            updater.SetValue(Updater.Type.ToString());
            themeMode.SetValue(theme.ToString());

            themeService.ChangeTheme(theme);

            applicationScope.AddSetting(askClose);
            applicationScope.AddSetting(searchUpdate);
            applicationScope.AddSetting(updater);
            applicationScope.AddSetting(themeMode);
            settingsManager.Save(pathService.GetSettingsFile());
            CloseWindowCommand.Execute(null);
        }
    }
}
