using Avalonia.Controls;
using MessageBox.Avalonia;
using MessageBox.Avalonia.BaseWindows;
using MessageBox.Avalonia.BaseWindows.Base;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using PluginFramework.DataContainer;
using PluginFramework.Enums;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.Commands.Conversion;
using XmlFormatterOsIndependent.Commands.Gui;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.EventArg;
using XmlFormatterOsIndependent.Helper;
using XmlFormatterOsIndependent.Models;
using XmlFormatterOsIndependent.Views;

namespace XmlFormatterOsIndependent.ViewModels
{
    /// <summary>
    /// View model for the main window
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand OpenPluginCommand { get; }
        public ICommand OpenAboutCommand { get; }
        public ICommand OpenSettingsCommand { get; }

        public ICommand CloseWindowCommand { get; }

        public ITriggerCommand OpenFileCommand { get; }

        public ITriggerCommand ConvertFileCommand { get; }

        public List<ModeSelection> ConversionModes { get; }

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
            set
            {
                this.RaiseAndSetIfChanged(ref currentPlugin, value);
                OpenFileCommand?.DataHasChanged();
                ConvertFileCommand?.DataHasChanged();
            }
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
            set
            {
                this.RaiseAndSetIfChanged(ref currentFormatter, value);
                ConvertFileCommand?.DataHasChanged();
            }
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
            set
            {
                this.RaiseAndSetIfChanged(ref currentMode, value);
                ConvertFileCommand?.DataHasChanged();
            }
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
            CloseWindowCommand = new CloseWindowCommand(view.GetWindow());
            OpenAboutCommand = new OpenWindowCommand(typeof(AboutWindow), view.GetParent());
            OpenSettingsCommand = new OpenWindowCommand(typeof(SettingsWindow), view.GetParent());
            OpenPluginCommand = new OpenWindowCommand(typeof(PluginManagerWindow), view.GetParent());
            OpenFileCommand = new OpenConversionFileCommand(view.Parent, pluginManager);
            OpenFileCommand.ContinueWith += (sender, data) =>
            {
                if (data is FileSelectedArg selectedArg)
                {
                    CurrentFile = selectedArg.SelectedFile;
                }
            };
            ConvertFileCommand = new ConvertFileCommand(view.GetParent(), pluginManager, (sender, data) => {
                StatusString = "Status: " + data.Message;
            });
            ConversionModes = new List<ModeSelection>();
            foreach(ModesEnum value in (ModesEnum[])Enum.GetValues(typeof(ModesEnum)))
            {
                ConversionModes.Add(new ModeSelection(value.ToString(), value));
            }

            if (!File.Exists(settingsPath))
            {
                settingsManager.Save(settingsPath);
            }

            TextBoxText = "Selected file path";
            statusString = "Status: ";

            List = this.pluginManager.ListPlugins<IFormatter>();
            formatterSelectorVisible = List.Count > 1;
            if (!formatterSelectorVisible)
            {
                statusString += "Missing plugins for conversion!";
            }

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
        /// Event if the settings window was shown
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The parameters of this event</param>
        private void ShowSettings_Executed(object sender, EventArgs e)
        {
            ChangeTheme();
        }

        /// <summary>
        /// Get the current conversion mode
        /// </summary>
        /// <returns>The mode of conversion</returns>
        private ModesEnum GetConvertionMode()
        {
            return currentMode == 0 ? ModesEnum.Formatted : ModesEnum.Flat;
        }

        /// <summary>
        /// Report a new issue
        /// </summary>
        public void ReportIssue()
        {
            UrlOpener urlOpener = new UrlOpener("https://github.com/XanatosX/XmlFormatter/issues");
            urlOpener.OpenUrl();
        }
    }
}
