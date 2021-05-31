using Avalonia.Controls;
using Avalonia.Styling;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.Commands.Settings;
using XmlFormatterOsIndependent.DataLoader;
using XmlFormatterOsIndependent.DataSets.Attributes;
using XmlFormatterOsIndependent.DataSets.Themes;
using XmlFormatterOsIndependent.DataSets.Themes.LoadableClasses;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.Manager;
using XmlFormatterOsIndependent.MVVM.Views;
using XmlFormatterOsIndependent.MVVM.Views.Setting;

namespace XmlFormatterOsIndependent.MVVM.ViewModels.Main
{
    internal class SettingsViewModel : ReactiveObject
    {

        public object LogfileView { get; }

        public object HotfolderView { get; }

        [SettingProperty]
        public bool AskBeforeClosing { get; set; }

        [SettingProperty]
        public bool CheckUpdateOnStart { get; set; }

        public List<Theme> AvailableThemes { get; private set; }

        public int SelectedThemeIndex { get; set; }

        public Theme SelectedTheme { get; set; }

        [SettingProperty]
        public string CurrentTheme => SelectedTheme.Name;

        public List<PluginMetaData> AvailableUpdaters { get; private set; }

        public PluginMetaData Updater
        {
            get => updater;
            set
            {
                this.RaiseAndSetIfChanged(ref updater, value);
            }
        }

        private PluginMetaData updater;

        [SettingProperty]
        public string selectedUpdater => DefaultManagerFactory.GetPluginManager().LoadPlugin<IUpdateStrategy>(updater).GetType().ToString();

        public int UpdaterIndex { get; set; }

        public ITriggerCommand SaveSettings { get; }

        public ISettingScope ISettingScope { get; private set; }

        public SettingsViewModel()
        {
            LogfileView = new LogfileView();
            HotfolderView = new HotfolderSettingView();

            SaveSettings = new SaveSettingsCommand(new List<object>() {
                this,
                GetDataContext(LogfileView),
                GetDataContext(HotfolderView)
            });
            SaveSettings.ContinueWith += (sender, data) =>
            {
                ThemeManager.ChangeTheme(SelectedTheme);
            };

            LoadSettings();
        }

        private void LoadSettings()
        {
            DefaultManagerFactory.GetSettingsManager().Load(DefaultManagerFactory.GetSettingPath());
            IPluginManager pluginManager = DefaultManagerFactory.GetPluginManager();
            CheckUpdateOnStart = GetSettingValue<bool>("SearchUpdateOnStartup");
            AskBeforeClosing = GetSettingValue<bool>("AskBeforeClosing");

            IDataLoader<SerializeableThemeContainer> themeLoader = new EmbeddedXmlDataLoader<SerializeableThemeContainer>();
            SerializeableThemeContainer loadedThemes = themeLoader.Load("XmlFormatterOsIndependent.EmbeddedData.ThemesLibrary.xml");
            if (loadedThemes != null)
            {
                AvailableThemes = loadedThemes.GetThemeContainer().Themes;
                AvailableThemes.Sort((themeA, themeB) => themeA.Name.CompareTo(themeB.Name));
                SelectedThemeIndex = AvailableThemes.FindIndex(theme => theme.Name == GetSettingValue<string>("CurrentTheme"));
                SelectedThemeIndex = SelectedThemeIndex == -1 ? 0 : SelectedThemeIndex;
                SelectedTheme = AvailableThemes[SelectedThemeIndex];
            }

            AvailableUpdaters = pluginManager.ListPlugins<IUpdateStrategy>().OrderBy((item) => item.Information.Name).ToList();
            if (AvailableUpdaters.Count > 0)
            {
                int index = AvailableUpdaters.Select(metaData => pluginManager.LoadPlugin<IUpdateStrategy>(metaData))
                                             .ToList()
                                             .FindIndex(updater => updater.GetType().FullName == GetSettingValue<string>("SelectedUpdater"));
                UpdaterIndex = index == -1 ? 0 : index;
            }
        }

        private T GetSettingValue<T>(string name)
        {
            ISettingScope applicationScope = DefaultManagerFactory.GetSettingsManager().GetScope("Default");
            ISettingPair settingPair = applicationScope?.GetSetting(name);
            return settingPair == null ? default : settingPair.GetValue<T>();
        }

        public object GetDataContext(object dataObject)
        {
            if (dataObject is UserControl userControl)
            {
                return userControl.DataContext;
            }

            return null;
        }
    }
}
