using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using PluginFramework.DataContainer;
using PluginFramework.Enums;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Windows.Input;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Helper;

namespace XmlFormatterOsIndependent.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string TextBoxText { get; }

        public List<PluginMetaData> List { get; }
        public PluginMetaData CurrentPlugin
        {
            get => currentPlugin;
            set => this.RaiseAndSetIfChanged(ref currentPlugin, value);
        }
        private PluginMetaData currentPlugin;

        public string StatusString
        {
            get => statusString;
            set => this.RaiseAndSetIfChanged(ref statusString, value);
        }
        private string statusString;

        public int CurrentFormatter
        {
            get => currentFormatter;
            set => this.RaiseAndSetIfChanged(ref currentFormatter, value);
        }
        private int currentFormatter;

        public int CurrentMode
        {
            get => currentMode;
            set => this.RaiseAndSetIfChanged(ref currentMode, value);
        }
        private int currentMode;

        public string CurrentFile
        {
            get => currentFile;
            set => this.RaiseAndSetIfChanged(ref currentFile, value);
        }
        private string currentFile;

        public bool SaveEnabled
        {
            get => saveEnabled;
            set => this.RaiseAndSetIfChanged(ref saveEnabled, value);
        }
        private bool saveEnabled;

        public bool FormatterSelectorVisible
        {
            get => formatterSelectorVisible;
            set => this.RaiseAndSetIfChanged(ref formatterSelectorVisible, value);
        }
        private bool formatterSelectorVisible;


        private readonly ViewContainer view;

        public MainWindowViewModel(ViewContainer view, ISettingsManager settingsManager, IPluginManager pluginManager)
            : base(settingsManager, pluginManager)
        {

            if (!File.Exists(settingsPath))
            {
                settingsManager.Save(settingsPath);
            }
            this.view = view;

            TextBoxText = "Selected file path";
            statusString = "Status: ";

            List = this.pluginManager.ListPlugins<IFormatter>();
            formatterSelectorVisible = List.Count > 1;

            CurrentFile = string.Empty;
            CurrentMode = 0;
            CurrentFormatter = 0;
        }

        private List<FileDialogFilter> GetCurrentFilter()
        {
            List<FileDialogFilter> filters = new List<FileDialogFilter>();

            IFormatter formatter = pluginManager.LoadPlugin<IFormatter>(currentPlugin);
            if (formatter == null)
            {
                return filters;
            }
            List<string> extensions = new List<string>();
            extensions.Add(formatter.Extension.ToLower());
            filters.Add(new FileDialogFilter()
            {
                Name = formatter.Extension.ToUpper() + "-File",
                Extensions = extensions
            });

            return filters;
        }

        public void LoadFileCommand()
        {
            IDataCommand openFile = new OpenFileCommand();
            FileDialogData data = new FileDialogData(view.GetWindow(), GetCurrentFilter());
            if (openFile.CanExecute(data))
            {
                openFile.Executed += OpenFile_Executed;
                openFile.AsyncExecute(data);
            }
        }

        public void SaveFileCommand()
        {
            IFormatter formatter = pluginManager.LoadPlugin<IFormatter>(currentPlugin);
            if (formatter == null || !File.Exists(CurrentFile))
            {
                StatusString = "Status: Something is wrong with the input file";
                SaveEnabled = false;
                return;
            }
            IDataCommand saveCommand = new SaveFileCommand();
            FileInfo fileInfo = new FileInfo(CurrentFile);
            string fileName = fileInfo.Name;
            fileName = fileName.Replace(fileInfo.Extension, "");
            fileName += "_" + GetConvertionMode().ToString();
            fileName += fileInfo.Extension;
            FileDialogData data = new FileDialogData(view.GetWindow(), GetCurrentFilter(), fileName);
            if (saveCommand.CanExecute(data))
            {
                saveCommand.AsyncExecute(data);
                saveCommand.Executed += SaveCommand_Executed;
            }
        }

        public void SearchForUpdate()
        {
            IDataCommand command = new CheckForUpdateCommand();
            command.Executed += UpdateExecuted_Executed;
            ExecuteAsyncCommand(command, null);

        }

        private void UpdateExecuted_Executed(object sender, EventArgs e)
        {
            if (sender is CheckForUpdateCommand updateCommand)
            {
                VersionCompare versionInfo = updateCommand.GetData<VersionCompare>();
                if (versionInfo.GitHubIsNewer)
                {

                }
            }
        }

        public void OpenAboutCommand()
        {
            IDataCommand command = new OpenAboutCommand();
            ExecuteAsyncCommand(command, view);
        }

        public void OpenSettingsCommand()
        {
            IDataCommand command = new OpenSettingsCommand();
            ExecuteAsyncCommand(command, view);
        }

        private void SaveCommand_Executed(object sender, System.EventArgs e)
        {
            if (sender is IDataCommand command)
            {
                if (!command.IsExecuted())
                {
                    return;
                }

                IFormatter formatter = pluginManager.LoadPlugin<IFormatter>(currentPlugin);

                formatter.StatusChanged += Formatter_StatusChanged;
                formatter.ConvertToFormat(CurrentFile, command.GetData<string>(), GetConvertionMode());
            }
        }

        private ModesEnum GetConvertionMode()
        {
            return currentMode == 0 ? ModesEnum.Formatted : ModesEnum.Flat; ;
        }

        private void Formatter_StatusChanged(object sender, PluginFramework.EventMessages.BaseEventArgs e)
        {
            StatusString = "Status: " + e.Message;
        }

        private void OpenFile_Executed(object sender, System.EventArgs e)
        {
            if (sender is IDataCommand command)
            {
                CurrentFile = command.IsExecuted() ? command.GetData<string>() : CurrentFile;
                SaveEnabled = !string.IsNullOrWhiteSpace(CurrentFile);
            }
        }

        public void ReportIssue()
        {
            UrlOpener urlOpener = new UrlOpener("https://github.com/XanatosX/XmlFormatter/issues");
            urlOpener.OpenUrl();
        }

        public void ExitApplication()
        {
            ICommand command = new CloseWindowCommand();
            ExecuteCommand(command, new CloseWindowData(view.GetWindow()));
        }
    }
}
