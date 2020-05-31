using Avalonia.Controls.Shapes;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using XmlFormatterModel.Setting;
using XmlFormatterModel.Update;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.DataSets;

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
        private readonly ViewContainer view;
        private readonly ISettingsManager settingsManager;
        private readonly IPluginManager pluginManager;
        private readonly ISettingScope applicationScope;
        private readonly string settingsPath;

        public SettingsWindowViewModel(ViewContainer view, ISettingsManager settingsManager, IPluginManager pluginManager)
        {
            this.view = view;
            this.settingsManager = settingsManager;
            this.pluginManager = pluginManager;
            if (this.settingsManager == null || this.pluginManager == null)
            {
                return;
            }
            settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            settingsPath += System.IO.Path.DirectorySeparatorChar + "XmlFormatter";
            settingsPath += System.IO.Path.DirectorySeparatorChar + "settings.set";
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
            ISettingPair askClose = new SettingPair("AskBeforeClosing");
            ISettingPair searchUpdate = new SettingPair("SearchUpdateOnStartup");
            ISettingPair updater = new SettingPair("UpdateStrategy");
            askClose.SetValue(AskBeforeClosing);
            searchUpdate.SetValue(CheckUpdateOnStart);
            updater.SetValue(Updater.Type.ToString());
            applicationScope.AddSetting(askClose);
            applicationScope.AddSetting(searchUpdate);
            applicationScope.AddSetting(updater);
            settingsManager.Save(settingsPath);
            CloseWindow();
        }

        private void ExecuteCommand(ICommand command, object parameter)
        {
            if (command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }
        }
    }
}
