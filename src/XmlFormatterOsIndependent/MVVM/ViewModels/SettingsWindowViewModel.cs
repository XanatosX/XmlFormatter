using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using ReactiveUI;
using System.Collections.Generic;
using System.Windows.Input;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.MVVM.ViewModels
{
    /// <summary>
    /// View model class for the settings window
    /// </summary>
    internal class SettingsWindowViewModel : ViewModelBase
    {

        /// <summary>
        /// Command to close a specific window
        /// </summary>
        public ICommand CloseWindowCommand { get; }

        /// <summary>
        /// A list with all the update strategies
        /// </summary>
        public List<PluginMetaData> List { get; }

        /// <summary>
        /// Ask before closing setting 
        /// </summary>
        public bool AskBeforeClosing
        {
            get => askBeforeClosing;
            set => this.RaiseAndSetIfChanged(ref askBeforeClosing, value);
        }
        /// <summary>
        /// Private acces for ask before closing setting
        /// </summary>
        private bool askBeforeClosing;

        /// <summary>
        /// The selected theme index
        /// </summary>
        public int ThemeMode
        {
            get => themeMode;
            set => this.RaiseAndSetIfChanged(ref themeMode, value);
        }
        /// <summary>
        /// Private selected theme index
        /// </summary>
        private int themeMode;

        /// <summary>
        /// Check for updates on start
        /// </summary>
        public bool CheckUpdateOnStart
        {
            get => checkUpdateOnStart;
            set => this.RaiseAndSetIfChanged(ref checkUpdateOnStart, value);
        }
        /// <summary>
        /// Private check for updates on start
        /// </summary>
        private bool checkUpdateOnStart;

        /// <summary>
        /// The current updater to use
        /// </summary>
        public PluginMetaData Updater
        {
            get => updater;
            set => this.RaiseAndSetIfChanged(ref updater, value);
        }
        /// <summary>
        /// Private updater to use
        /// </summary>
        private PluginMetaData updater;

        /// <summary>
        /// Index of the selected updater
        /// </summary>
        public int UpdaterIndex
        {
            get => updaterIndex;
            set => this.RaiseAndSetIfChanged(ref updaterIndex, value);
        }
        /// <summary>
        /// Private index of the selected updater
        /// </summary>
        private int updaterIndex;

        /// <summary>
        /// The setting scope of this application
        /// </summary>
        private readonly ISettingScope applicationScope;

        /// <summary>
        /// Create a new instance of this view
        /// </summary>
        /// <param name="view">The view for this model</param>
        /// <param name="settingsManager">The settings manager to use</param>
        /// <param name="pluginManager">The plugin manager to use</param>
        public SettingsWindowViewModel(ViewContainer view, ISettingsManager settingsManager, IPluginManager pluginManager)
            : base(view, settingsManager, pluginManager)
        {
            CloseWindowCommand = new CloseWindowCommand(view.GetWindow());
            if (this.settingsManager == null || this.pluginManager == null)
            {
                return;
            }
            this.settingsManager.Load(settingsPath);
            applicationScope = this.settingsManager.GetScope("Default");
            List = pluginManager.ListPlugins<IUpdateStrategy>();

            if (applicationScope == null)
            {
                applicationScope = new SettingScope("Default");
                this.settingsManager.AddScope(applicationScope);
            }
            LoadSettings();
        }

        /// <summary>
        /// Load all the settings
        /// </summary>
        private void LoadSettings()
        {
            AskBeforeClosing = GetSettingsValue<bool>("AskBeforeClosing");
            CheckUpdateOnStart = GetSettingsValue<bool>("SearchUpdateOnStartup");
            IDataCommand getTheme = new GetThemeCommand();
            ExecuteCommand(getTheme, settingsManager);
            ThemeMode = (int)getTheme.GetData<ThemeEnum>();

            string updater = GetSettingsValue<string>("UpdateStrategy");
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

        /// <summary>
        /// Save the settings and close this window
        /// </summary>
        public void SaveAndClose()
        {
            ThemeEnum theme = ThemeMode == 0 ? ThemeEnum.Light : ThemeEnum.Dark;

            ISettingPair askClose = new SettingPair("AskBeforeClosing");
            ISettingPair searchUpdate = new SettingPair("SearchUpdateOnStartup");
            ISettingPair updater = new SettingPair("UpdateStrategy");
            ISettingPair themeMode = new SettingPair("Theme");
            askClose.SetValue(AskBeforeClosing);
            searchUpdate.SetValue(CheckUpdateOnStart);
            updater.SetValue(Updater.Type.ToString());
            themeMode.SetValue(theme.ToString());

            applicationScope.AddSetting(askClose);
            applicationScope.AddSetting(searchUpdate);
            applicationScope.AddSetting(updater);
            applicationScope.AddSetting(themeMode);
            settingsManager.Save(settingsPath);
            CloseWindowCommand.Execute(null);
        }
    }
}
