using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PluginFramework.Interfaces.Manager;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Services;

namespace XmlFormatterOsIndependent.ViewModels
{
    /// <summary>
    /// View model class for the settings window
    /// </summary>
    public partial class SettingsWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableObject? settingsBackupContent;

        [ObservableProperty]
        private ObservableObject? applicationSettingsContent;

        [ObservableProperty]
        private ObservableObject? loggingContent;

        [ObservableProperty]
        private ObservableObject? hotfolderContent;

        private readonly IWindowApplicationService applicationService;
        private readonly IThemeService themeService;

        /// <summary>
        /// Create a new instance of this view
        /// </summary>
        /// <param name="view">The view for this model</param>
        /// <param name="settingsManager">The settings manager to use</param>
        /// <param name="pluginManager">The plugin manager to use</param>
        public SettingsWindowViewModel(
            IWindowApplicationService applicationService,
            IThemeService themeService,
            IDependecyInjectionResolverService resolverService)
        {
            this.applicationService = applicationService;
            this.themeService = themeService;

            applicationSettingsContent = resolverService.GetService<ApplicationSettingsViewModel>();
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
            /**
            * @TODO: Mode this into the ApplicationSettingsViewModel Triggered via message!
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

            */
            CloseWindowCommand.Execute(null);
        }
    }
}
