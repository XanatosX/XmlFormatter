using Avalonia.Controls;
using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MessageBox.Avalonia;
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
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.Commands.Conversion;
using XmlFormatterOsIndependent.Commands.Gui;
using XmlFormatterOsIndependent.Commands.SystemCommands;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.EventArg;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.Models;
using XmlFormatterOsIndependent.Services;
using XmlFormatterOsIndependent.Views;

namespace XmlFormatterOsIndependent.ViewModels
{
    /// <summary>
    /// View model for the main window
    /// </summary>
    public partial class MainWindowViewModel : ObservableObject
    {
        /// <summary>
        /// Command to open the plugin window
        /// </summary>
        public ICommand OpenPluginCommand { get; }

        /// <summary>
        /// Command to open the about window
        /// </summary>
        public ICommand OpenAboutCommand { get; }

        /// <summary>
        /// Command to open the settings window
        /// </summary>
        public ITriggerCommand OpenSettingsCommand { get; }

        /// <summary>
        /// Command to convert and save the files
        /// </summary>
        public ITriggerCommand ConvertFileCommand { get; }

        /// <summary>
        /// The modes you could convert to
        /// </summary>
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
        /**
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
        */

        /// <summary>
        /// Private storage for current selected plugin
        /// </summary>
        private PluginMetaData currentPlugin;

        /// <summary>
        /// Current text on the status string
        /// </summary>
        /**
        public string StatusString
        {
            get => statusString;
            set => this.RaiseAndSetIfChanged(ref statusString, value);
        }
        */

        /// <summary>
        /// Private text of the status string
        /// </summary>
        [ObservableProperty]
        private string statusString;

        /// <summary>
        /// Current index of the selected formatter
        /// </summary>

        /**
        public int CurrentFormatter
        {
            get => currentFormatter;
            set
            {
                this.RaiseAndSetIfChanged(ref currentFormatter, value);
                ConvertFileCommand?.DataHasChanged();
                CurrentFile = string.Empty;
            }
        }
        */

        /// <summary>
        /// Private index of the selected formatter
        /// </summary>
        private int currentFormatter;

        /// <summary>
        /// The currently selected conversion mode
        /// </summary>
        /**
        public int CurrentMode
        {
            get => currentMode;
            set
            {
                this.RaiseAndSetIfChanged(ref currentMode, value);
                ConvertFileCommand?.DataHasChanged();
            }
        }
        */

        /// <summary>
        /// Private currently selected conversion mode
        /// </summary>
        private int currentMode;

        /// <summary>
        /// The currently selected file
        /// </summary>
        /**
        public string CurrentFile
        {
            get => currentFile;
            set => this.RaiseAndSetIfChanged(ref currentFile, value);
        }
        */

        /// <summary>
        /// Private currently selected file
        /// </summary>
        private string currentFile;
        private readonly IIOInteractionService interactionService;
        private readonly IWindowApplicationService applicationService;

        /// <summary>
        /// Is the formatter selector visible at the moment
        /// </summary>
        public bool FormatterSelectorVisible { get; }

        /// <summary>
        /// Is the mode of the formatter selector visible right now
        /// </summary>
        public bool FormatterModeSelectionVisible { get; }

        /// <summary>
        /// Create a new instance of this main window viewer
        /// </summary>
        /// <param name="view">The view of this model</param>
        /// <param name="settingsManager">The settings manager to use</param>
        /// <param name="pluginManager">The plugin manager to use</param>
        public MainWindowViewModel(ISettingsManager settingsManager,
                                   IPluginManager pluginManager,
                                   IPathService pathService,
                                   IIOInteractionService interactionService,
                                   IWindowApplicationService applicationService) // ViewContainer view, 
                                                                                 //: base(settingsManager, pluginManager)
        {
            /**
            OpenAboutCommand = new OpenWindowCommand(typeof(AboutWindow), view.GetParent());
            OpenSettingsCommand = new OpenWindowCommand(typeof(SettingsWindow), view.GetParent());
            OpenSettingsCommand.ContinueWith += (sender, data) => ChangeTheme();
            OpenPluginCommand = new OpenWindowCommand(typeof(PluginManagerWindow), view.GetParent());
            OpenFileCommand = new OpenConversionFileCommand(view.Parent, pluginManager);
            OpenFileCommand.ContinueWith += (sender, data) =>
            {
                if (data is FileSelectedArg selectedArg)
                {
                    CurrentFile = selectedArg.SelectedFile;
                }
            };
            ConvertFileCommand = new ConvertFileCommand(view.GetParent(), pluginManager, (sender, data) =>
            {
                StatusString = "Status: " + data.Message;
            });
            */
            ConversionModes = new List<ModeSelection>();
            foreach (ModesEnum value in (ModesEnum[])Enum.GetValues(typeof(ModesEnum)))
            {
                ConversionModes.Add(new ModeSelection(value.ToString(), value));
            }

            if (!File.Exists(pathService.GetSettingsFile()))
            {
                settingsManager.Save(pathService.GetSettingsFile());
            }

            TextBoxText = "Selected file path";
            StatusString = "Status: ";

            List = pluginManager.ListPlugins<IFormatter>().ToList();
            FormatterSelectorVisible = List.Count > 1;
            FormatterModeSelectionVisible = true;
            if (List.Count == 0)
            {
                FormatterModeSelectionVisible = false;
                StatusString += "Missing plugins for conversion!";
            }

            this.interactionService = interactionService;
            this.applicationService = applicationService;
            /**
CurrentFile = string.Empty;
CurrentMode = 0;
CurrentFormatter = 0;
*/

            /**
            view.Current.AddHandler(DragDrop.DragOverEvent, (sender, data) =>
            {
                if (!CheckDragDropFile(data))
                {
                    data.DragEffects = DragDropEffects.None;
                    return;
                }

                data.DragEffects = DragDropEffects.Copy;
            });

            view.Current.AddHandler(DragDrop.DropEvent, (sender, data) =>
            {
                if (!CheckDragDropFile(data))
                {
                    return;
                }

                CurrentFile = data.Data.GetFileNames().First();
            });
            */
        }

        [RelayCommand]
        public async void OpenFile()
        {
            //@TODO: Replace filter with proper one!
            var data = await applicationService.OpenFileAsync(new() { new FileDialogFilter() {
            Extensions =  new List<string>()
            {
                "txt"
            },
            Name = "txt-Files"
            }}
            );
        }

        [RelayCommand]
        public void CloseApplication()
        {
            applicationService.CloseAplication();
        }

        [RelayCommand]
        public void OpenUrl(string url)
        {
            interactionService.OpenWebsite(url);
        }

        /// <summary>
        /// Check if the file which was dragged dropped into is correct
        /// </summary>
        /// <param name="data">The dataset to check</param>
        /// <returns>True if the file is valid</returns>
        private bool CheckDragDropFile(DragEventArgs data)
        {
            //IFormatter currentFormatter = pluginManager.LoadPlugin<IFormatter>(CurrentPlugin);
            IFormatter currentFormatter = null;
            IReadOnlyList<string> files = (List<string>)data.Data.GetFileNames();
            if (currentFormatter == null
                || files.Count == 0
                || files.Count > 1)
            {
                return false;
            }
            string firstFile = files.First();
            FileInfo info = new FileInfo(firstFile);
            if (info.Extension.Replace(".", string.Empty) != currentFormatter.Extension)
            {
                return false;
            }
            return true;
        }

        /// <inheritdoc>/>
        //protected override void IsOsX()
        //{
        /**
        Window parent = view.Current;
        parent.FindControl<DockPanel>("WindowDock").IsVisible = false;
        parent.Height = parent.Height - 35;
        parent.MinHeight = parent.Height;
        parent.MaxHeight = parent.Height;
        */
        //}

        /// <summary>
        /// Search if there is an update
        /// </summary>
        public void SearchForUpdate()
        {
            IDataCommand command = new CheckForUpdateCommand();
            command.Executed += UpdateExecuted_Executed;
            //ExecuteAsyncCommand(command, null);
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
                //ExecuteCommand(themeCommand, settingsManager);

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
                //TaskAwaiter<ButtonResult> awaiter = window.ShowDialog(view.GetWindow()).GetAwaiter();
                //awaiter.OnCompleted(() =>
                //{
                //ButtonResult buttonResult = awaiter.GetResult();
                //if (buttonResult == ButtonResult.Yes)
                //{
                //ICommand command = new UpdateApplicationCommand();
                //ExecuteCommand(command, new UpdateApplicationData(pluginManager, settingsManager, versionInfo));
                //}
                //});
            }
        }
    }
}
