using Avalonia.Controls;
using MessageBox.Avalonia;
using MessageBox.Avalonia.BaseWindows;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using PluginFramework.DataContainer;
using PluginFramework.Enums;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.Helper;

namespace XmlFormatterOsIndependent.ViewModels
{
    /// <summary>
    /// View model for the main window
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Text for the text box
        /// </summary>
        public string TextBoxText { get; }

        /// <summary>
        /// List of all the formatting plugins
        /// </summary>
        public List<PluginMetaData> List { get; }

        /// <summary>
        /// Current selected plugin
        /// </summary>
        public PluginMetaData CurrentPlugin
        {
            get => currentPlugin;
            set => this.RaiseAndSetIfChanged(ref currentPlugin, value);
        }
        /// <summary>
        /// Private storage for current selected plugin
        /// </summary>
        private PluginMetaData currentPlugin;

        /// <summary>
        /// Current text on the status string
        /// </summary>
        public string StatusString
        {
            get => statusString;
            set => this.RaiseAndSetIfChanged(ref statusString, value);
        }
        /// <summary>
        /// Private text of the status string
        /// </summary>
        private string statusString;

        /// <summary>
        /// Current index of the selected formatter
        /// </summary>
        public int CurrentFormatter
        {
            get => currentFormatter;
            set => this.RaiseAndSetIfChanged(ref currentFormatter, value);
        }
        /// <summary>
        /// Private index of the selected formatter
        /// </summary>
        private int currentFormatter;

        /// <summary>
        /// The currently selected conversion mode
        /// </summary>
        public int CurrentMode
        {
            get => currentMode;
            set => this.RaiseAndSetIfChanged(ref currentMode, value);
        }
        /// <summary>
        /// Private currently selected conversion mode
        /// </summary>
        private int currentMode;

        /// <summary>
        /// The currently selected file
        /// </summary>
        public string CurrentFile
        {
            get => currentFile;
            set => this.RaiseAndSetIfChanged(ref currentFile, value);
        }
        /// <summary>
        /// Private currently selected file
        /// </summary>
        private string currentFile;

        /// <summary>
        /// Is the save button enabled
        /// </summary>
        public bool SaveEnabled
        {
            get => saveEnabled;
            set => this.RaiseAndSetIfChanged(ref saveEnabled, value);
        }
        /// <summary>
        /// Private information if the save button is enabled
        /// </summary>
        private bool saveEnabled;

        /// <summary>
        /// Is the formatter selector visible at the moment
        /// </summary>
        public bool FormatterSelectorVisible
        {
            get => formatterSelectorVisible;
            set => this.RaiseAndSetIfChanged(ref formatterSelectorVisible, value);
        }
        /// <summary>
        /// Private field if the formatter selection is visible at the moment
        /// </summary>
        private bool formatterSelectorVisible;

        /// <summary>
        /// Create a new instance of this main window viewer
        /// </summary>
        /// <param name="view">The view of this model</param>
        /// <param name="settingsManager">The settings manager to use</param>
        /// <param name="pluginManager">The plugin manager to use</param>
        public MainWindowViewModel(ViewContainer view, ISettingsManager settingsManager, IPluginManager pluginManager)
            : base(view, settingsManager, pluginManager)
        {
            if (!File.Exists(settingsPath))
            {
                settingsManager.Save(settingsPath);
            }

            TextBoxText = "Selected file path";
            statusString = "Status: ";

            List = this.pluginManager.ListPlugins<IFormatter>();
            formatterSelectorVisible = List.Count > 1;

            CurrentFile = string.Empty;
            CurrentMode = 0;
            CurrentFormatter = 0;
        }

        /// <inheritdoc>/>
        protected override void IsOsX()
        {
            Window parent = view.Current;
            parent.FindControl<DockPanel>("WindowDock").IsVisible = false;
            parent.Height = parent.Height - 35;
            parent.MinHeight = parent.Height;
            parent.MaxHeight = parent.Height;
        }

        /// <summary>
        /// Trigger the file load dialog
        /// </summary>
        public void LoadFileCommand()
        {
            IDataCommand openFile = new OpenFileCommand();
            FileDialogData data = new FileDialogData(view.Current, GetCurrentFilter());
            if (openFile.CanExecute(data))
            {
                openFile.Executed += OpenFile_Executed;
                openFile.AsyncExecute(data);
            }
        }

        /// <summary>
        /// Trigger the save file dialog
        /// </summary>
        public void SaveFileCommand()
        {
            IFormatter formatter = pluginManager.LoadPlugin<IFormatter>(currentPlugin);
            if (formatter == null || !File.Exists(CurrentFile))
            {
                StatusString = "Status: Something is wrong with the input file";
                SaveEnabled = false;
                return;
            }
            IDataCommand saveCommand = new OpenSaveFileDialogCommand();
            FileInfo fileInfo = new FileInfo(CurrentFile);
            string fileName = fileInfo.Name;
            fileName = fileName.Replace(fileInfo.Extension, "");
            fileName += "_" + GetConvertionMode().ToString();
            fileName += fileInfo.Extension;
            FileDialogData data = new FileDialogData(view.Current, GetCurrentFilter(), fileName);
            if (saveCommand.CanExecute(data))
            {
                saveCommand.AsyncExecute(data);
                saveCommand.Executed += SaveCommand_Executed;
            }
        }

        /// <summary>
        /// Get the current filter for file open or save dialog
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Search if there is an update
        /// </summary>
        public void SearchForUpdate()
        {
            IDataCommand command = new CheckForUpdateCommand();
            command.Executed += UpdateExecuted_Executed;
            ExecuteAsyncCommand(command, null);
        }

        /// <summary>
        /// Show information if there is an update
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments of the event</param>
        private void UpdateExecuted_Executed(object sender, EventArgs e)
        {
            if (sender is CheckForUpdateCommand updateCommand)
            {
                IDataCommand themeCommand = new GetThemeCommand();
                ExecuteCommand(themeCommand, settingsManager);

                VersionCompare versionInfo = updateCommand.GetData<VersionCompare>();
                string title = "Version is up to date";
                string content = "You version is up to date";

                MessageBoxStandardParams parameter = new MessageBoxStandardParams()
                {
                    Icon = Icon.Info
                };

                if (themeCommand.IsExecuted() && themeCommand.GetData<ThemeEnum>() == ThemeEnum.Dark)
                {
                    parameter.Style = Style.DarkMode;
                }

                ButtonEnum buttons = ButtonEnum.Ok;
                if (versionInfo.GitHubIsNewer)
                {
                    title = "Update Available";
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendFormat(
                        "There is a new version available{0}{0}Your version: {1}{0}Remote version: {2}{0}{0}Do you want to update?",
                        Environment.NewLine,
                        versionInfo.LocalVersion,
                        versionInfo.GitHubVersion
                        );
                    content = stringBuilder.ToString();
                    buttons = ButtonEnum.YesNo;
                }
                parameter.ContentTitle = title;
                parameter.ContentMessage = content;
                parameter.ButtonDefinitions = buttons;

                IMsBoxWindow<ButtonResult> window = MessageBoxManager.GetMessageBoxStandardWindow(parameter);
                TaskAwaiter<ButtonResult> awaiter = window.ShowDialog(view.GetWindow()).GetAwaiter();
                awaiter.OnCompleted(() =>
                {
                    ButtonResult buttonResult = awaiter.GetResult();
                    if (buttonResult == ButtonResult.Yes)
                    {
                        ICommand command = new UpdateApplicationCommand();
                        ExecuteCommand(command, new UpdateApplicationData(pluginManager, settingsManager, versionInfo));
                    }
                });
            }
        }

        /// <summary>
        /// Open the about window
        /// </summary>
        public void OpenAboutCommand()
        {
            IDataCommand command = new OpenAboutCommand();
            ExecuteAsyncCommand(command, view);
        }

        /// <summary>
        /// Open the settings window
        /// </summary>
        public void OpenSettingsCommand()
        {
            IDataCommand command = new OpenSettingsCommand();
            command.Executed += ShowSettings_Executed;
            ExecuteAsyncCommand(command, view);
        }

        /// <summary>
        /// Event if the settings window was shown
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The parameters of this event</param>
        private void ShowSettings_Executed(object sender, EventArgs e)
        {
            ChangeTheme();
        }

        /// <summary>
        /// Save was executed successfully
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
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

        /// <summary>
        /// Get the current conversion mode
        /// </summary>
        /// <returns>The mode of conversion</returns>
        private ModesEnum GetConvertionMode()
        {
            return currentMode == 0 ? ModesEnum.Formatted : ModesEnum.Flat; ;
        }

        /// <summary>
        /// Did the status of the formttaer change
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The parameter of the event</param>
        private void Formatter_StatusChanged(object sender, PluginFramework.EventMessages.BaseEventArgs e)
        {
            StatusString = "Status: " + e.Message;
        }

        /// <summary>
        /// Open file dialog was executed
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The parameters of the event</param>
        private void OpenFile_Executed(object sender, EventArgs e)
        {
            if (sender is IDataCommand command)
            {
                CurrentFile = command.IsExecuted() ? command.GetData<string>() : CurrentFile;
                SaveEnabled = !string.IsNullOrWhiteSpace(CurrentFile);
            }
        }

        /// <summary>
        /// Report a new issue
        /// </summary>
        public void ReportIssue()
        {
            UrlOpener urlOpener = new UrlOpener("https://github.com/XanatosX/XmlFormatter/issues");
            urlOpener.OpenUrl();
        }

        /// <summary>
        /// Exit this application
        /// </summary>
        public void ExitApplication()
        {
            ICommand command = new CloseWindowCommand();
            ExecuteCommand(command, new CloseWindowData(view.Current));
        }
    }
}
