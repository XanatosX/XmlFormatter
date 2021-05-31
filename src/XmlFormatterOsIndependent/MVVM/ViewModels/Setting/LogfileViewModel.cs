using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.DataLoader;
using XmlFormatterOsIndependent.DataSets.Attributes;
using XmlFormatterOsIndependent.Factories;

namespace XmlFormatterOsIndependent.MVVM.ViewModels.Setting
{
    class LogfileViewModel : ReactiveObject
    {
        private readonly string defaultLogPath;

        [SettingProperty]
        public bool LoggingActive { get; set; }

        private IDataLoader<List<FileInfo>> folderDataLoader;

        private IDataLoader<string> logFileLoader;

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

        public string LogfileText
        {
            get => logFileText;
            private set => this.RaiseAndSetIfChanged(ref logFileText, value);
        }

        private string logFileText;

        private FileInfo selectedLogFile;

        public ICommand DeleteLogFile { get; }

        public ICommand OpenLogFolder { get; }

        public LogfileViewModel()
        {
            string appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "XMLFormatter");
            defaultLogPath = Path.Combine(appFolder, "logs");

            logFileLoader = new FileContentLoader();
            folderDataLoader = new FolderDataLoader(fileInfo => fileInfo.Extension.ToLower() == ".log");
            LogFiles = new ObservableCollection<FileInfo>();

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

            ISettingPair pair = DefaultManagerFactory.GetSettingsManager().GetSetting("Default", "LoggingActive");
            LoggingActive = pair == null ? false : pair.GetValue<bool>();

            LoadLogFiles();
        }

        private void LoadLogFiles()
        {
            LogFiles = new ObservableCollection<FileInfo>(folderDataLoader.Load(GetLogFolder()));
        }

        private string GetLogFolder()
        {
            return defaultLogPath;
            /**
            string logFilePath = GetSettingValue<string>("logFilePath");
            if (logFilePath == null || !Directory.Exists(logFilePath))
            {
                logFilePath = defaultLogPath;
            }
            return logFilePath;
            */
        }
    }
}
