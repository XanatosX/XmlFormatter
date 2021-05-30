using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using System.Xml.Serialization;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.Commands.Settings;
using XmlFormatterOsIndependent.DataLoader;
using XmlFormatterOsIndependent.DataSets.Attributes;
using XmlFormatterOsIndependent.Factories;

namespace XmlFormatterOsIndependent.MVVM.ViewModels
{
    internal class SettingsViewModel : ReactiveObject
    {
        private readonly string settingsPath;

        private readonly string defaultLogPath;

        [SettingProperty]
        public bool AskBeforeClosing { get; set; }

        [SettingProperty]
        public bool CheckUpdateOnStart { get; set; }

        [SettingProperty]
        public bool LoggingActive { get; set; }

        [SettingProperty]
        public bool HotfolderActive { get; set; }

        
        public List<string> AvailableThemes { get; private set; }

        public int SelectedThemeIndex { get; set; }

        [SettingProperty]
        public string SelectedTheme { get; set; }

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
        public string selectedUpdater => managerFactory.GetPluginManager().LoadPlugin<IUpdateStrategy>(updater).GetType().ToString();

        public int UpdaterIndex { get; set; }

        public ObservableCollection<FileInfo> LogFiles { get; private set; }

        public FileInfo SelectedLogFile
        {
            get => selectedLogFile;
            set
            {
                selectedLogFile = value;
                if (SelectedLogFile != null)
                {
                    LogfileText = logFileLoader?.Load(selectedLogFile.FullName);
                }
                
            }
        }

        private FileInfo selectedLogFile;

        public string LogfileText
        {
            get => logFileText;
            private set => this.RaiseAndSetIfChanged(ref logFileText, value);
        }

        private string logFileText;

        public ICommand DeleteLogFile { get; }

        public ICommand OpenLogFolder { get; }

        public ITriggerCommand SaveSettings { get; }

        public ISettingScope ISettingScope { get; private set; }

        private DefaultManagerFactory managerFactory;

        private IDataLoader<List<FileInfo>> folderDataLoader;

        private IDataLoader<string> logFileLoader;

        public SettingsViewModel()
        {
            managerFactory = new DefaultManagerFactory();
            logFileLoader = new FileContentLoader();
            folderDataLoader = new FolderDataLoader(fileInfo => fileInfo.Extension.ToLower() == ".log");

            LogFiles = new ObservableCollection<FileInfo>();
            settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "XMLFormatter");
            defaultLogPath = Path.Combine(settingsPath, "logs");
            settingsPath = Path.Combine(settingsPath, "settings.set");

            DeleteLogFile = new RelayCommand(
                paramter =>
                {
                    return paramter != null && paramter is FileInfo info && File.Exists(info.FullName);
                },
                paramter =>
                {
                    if (paramter is FileInfo info)
                    {
                        File.Delete(info.FullName);
                        if (!File.Exists(info.FullName))
                        {
                            LogFiles.Remove(info);
                            LogfileText = string.Empty;
                        }
                    }
                }
                );

            OpenLogFolder = new RelayCommand(
                paramter => Directory.Exists(GetLogFolder()),
                paramter =>
                {
                    ProcessStartInfo processStart = new ProcessStartInfo()
                    {
                        FileName = GetLogFolder(),
                        UseShellExecute = true
                    };
                    Process.Start(processStart);
                }
            );

            SaveSettings = new SaveSettingsCommand(managerFactory, this, settingsPath);

            LoadSettings();
            LoadLogFiles();
        }

        private void LoadSettings()
        {
            managerFactory.GetSettingsManager().Load(settingsPath);
            IPluginManager pluginManager = managerFactory.GetPluginManager();
            CheckUpdateOnStart = GetSettingValue<bool>("SearchUpdateOnStartup");
            AskBeforeClosing = GetSettingValue<bool>("AskBeforeClosing");
            LoggingActive = false;
            HotfolderActive = false;
            AvailableThemes = new List<string>() { "Light", "Dark" };
            SelectedThemeIndex = AvailableThemes.FindIndex(theme => theme == GetSettingValue<string>("Theme"));
            SelectedThemeIndex = SelectedThemeIndex == -1 ? 0 : SelectedThemeIndex;
            SelectedTheme = AvailableThemes[SelectedThemeIndex];
            AvailableUpdaters = pluginManager.ListPlugins<IUpdateStrategy>().OrderBy((item) => item.Information.Name).ToList();
            if (AvailableUpdaters.Count > 0)
            {
                int index = AvailableUpdaters.Select(metaData => pluginManager.LoadPlugin<IUpdateStrategy>(metaData))
                                             .ToList()
                                             .FindIndex(updater => updater.GetType().FullName == GetSettingValue<string>("SelectedUpdater"));
                UpdaterIndex = index == -1 ? 0 : index;
            }


        }

        private void LoadLogFiles()
        {

            LogFiles = new ObservableCollection<FileInfo>(folderDataLoader.Load(GetLogFolder()));
        }

        private string GetLogFolder()
        {
            string logFilePath = GetSettingValue<string>("logFilePath");
            if (logFilePath == null || !Directory.Exists(logFilePath))
            {
                logFilePath = defaultLogPath;
            }
            return logFilePath;
        }

        private T GetSettingValue<T>(string name)
        {
            ISettingScope applicationScope = managerFactory.GetSettingsManager().GetScope("Default");
            ISettingPair settingPair = applicationScope?.GetSetting(name);
            return settingPair == null ? default(T) : settingPair.GetValue<T>();
        }
    }
}
