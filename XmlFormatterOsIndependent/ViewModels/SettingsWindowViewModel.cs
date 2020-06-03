using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.ViewModels
{
    internal class SettingsWindowViewModel : ViewModelBase
    {
        public List<PluginMetaData> List { get; }
        public bool AskBeforeClosing
        {
            get => askBeforeClosing;
            set => this.RaiseAndSetIfChanged(ref askBeforeClosing, value);
        }
        private bool askBeforeClosing;

        public int ThemeMode
        {
            get => themeMode;
            set => this.RaiseAndSetIfChanged(ref themeMode, value);
        }
        private int themeMode;

        public bool CheckUpdateOnStart
        {
            get => checkUpdateOnStart;
            set => this.RaiseAndSetIfChanged(ref checkUpdateOnStart, value);
        }
        private bool checkUpdateOnStart;

        public PluginMetaData Updater
        {
            get => updater;
            set => this.RaiseAndSetIfChanged(ref updater, value);
        }
        private PluginMetaData updater;

        public int UpdaterIndex
        {
            get => updaterIndex;
            set => this.RaiseAndSetIfChanged(ref updaterIndex, value);
        }
        private int updaterIndex;

        private readonly ISettingScope applicationScope;

        public SettingsWindowViewModel(ViewContainer view, ISettingsManager settingsManager, IPluginManager pluginManager)
            : base(view, settingsManager, pluginManager)
        {
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

        private T GetSettingsValue<T>(string name)
        {
            ISettingPair settingPair = applicationScope.GetSetting(name);
            return settingPair == null ? default : settingPair.GetValue<T>();
        }

        public void CloseWindow()
        {
            ICommand command = new CloseWindowCommand();
            ExecuteCommand(command, new CloseWindowData(view.GetWindow()));
        }

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
            CloseWindow();
        }
    }
}
