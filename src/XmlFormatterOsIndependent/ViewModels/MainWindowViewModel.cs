using Avalonia.Controls;
using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MessageBox.Avalonia;
using MessageBox.Avalonia.BaseWindows.Base;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using PluginFramework.DataContainer;
using PluginFramework.Enums;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.Models;
using XmlFormatterOsIndependent.Services;

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
        /// The modes you could convert to
        /// </summary>
        public List<ModeSelection> ConversionModes { get; }

        /// <summary>
        /// Text for the text box
        /// </summary>
        public string TextBoxText { get; }

        [ObservableProperty]
        private List<PluginMetaData> availablePlugins;

        /// <summary>
        /// Private storage for current selected plugin
        /// </summary>
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConvertFileCommand))]
        [NotifyCanExecuteChangedFor(nameof(OpenFileCommand))]
        private PluginMetaData? currentPlugin;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConvertFileCommand))]
        [NotifyCanExecuteChangedFor(nameof(OpenFileCommand))]
        private ModeSelection? selectedMode;

        /// <summary>
        /// Private currently selected file
        /// </summary>
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConvertFileCommand))]
        private string? currentFile;



        /// <summary>
        /// Private text of the status string
        /// </summary>
        [ObservableProperty]
        private string? statusString;

        /// <summary>
        /// Is the formatter selector visible at the moment
        /// </summary>
        public bool FormatterSelectorVisible { get; }

        /// <summary>
        /// Is the mode of the formatter selector visible right now
        /// </summary>
        public bool FormatterModeSelectionVisible { get; }

        private readonly IPluginManager pluginManager;
        private readonly IIOInteractionService interactionService;
        private readonly IWindowApplicationService applicationService;


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

            AvailablePlugins = pluginManager.ListPlugins<IFormatter>().ToList();
            CurrentPlugin = AvailablePlugins.FirstOrDefault();
            SelectedMode = ConversionModes.FirstOrDefault();


            FormatterSelectorVisible = CurrentPlugin is not null;
            FormatterModeSelectionVisible = FormatterSelectorVisible;
            if (CurrentPlugin is null)
            {
                FormatterModeSelectionVisible = false;
                StatusString += "Missing plugins for conversion!";
            }

            this.pluginManager = pluginManager;
            this.interactionService = interactionService;
            this.applicationService = applicationService;

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

        [RelayCommand(CanExecute = nameof(CanOpenFile))]
        public async void OpenFile()
        {
            var plugin = pluginManager.LoadPlugin<IFormatter>(CurrentPlugin);
            if (plugin is null)
            {
                return;
            }
            var data = await applicationService.OpenFileAsync(new() { CreatePluginFileFilter(plugin) });

            CurrentFile = data;
        }

        private static FileDialogFilter CreatePluginFileFilter(IFormatter? plugin)
        {
            return new FileDialogFilter { Extensions = new() { plugin.Extension }, Name = $"{plugin.Extension}-file" };
        }

        public bool CanOpenFile()
        {
            return CurrentPlugin is not null
                   && SelectedMode is not null
                   && pluginManager.LoadPlugin<IFormatter>(CurrentPlugin) is not null;
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


        [RelayCommand(CanExecute = nameof(CanConvertFile))]
        public async void ConvertFile()
        {
            var plugin = pluginManager.LoadPlugin<IFormatter>(CurrentPlugin);
            if (plugin is null || SelectedMode is null)
            {
                return;
            }
            List<FileDialogFilter> fitlers = new() { CreatePluginFileFilter(plugin) };
            var saveFile = await applicationService.SaveFileAsync(fitlers);
            if (saveFile is null)
            {
                return;
            }

            plugin.StatusChanged += (_, e) =>
            {
                StatusString = $"Status: {e.Message}";
            };

            bool success = SelectedMode.Value switch
            {
                ModesEnum.Flat => plugin.ConvertToFlat(CurrentFile, saveFile),
                ModesEnum.Formatted => plugin.ConvertToFormatted(CurrentFile, saveFile),
                _ => false
            };

        }

        public bool CanConvertFile()
        {
            bool modeAndConverterSelected = SelectedMode is not null && CurrentPlugin is not null;
            bool fileIsExisting = File.Exists(CurrentFile);
            ;
            return modeAndConverterSelected
                && fileIsExisting
                && pluginManager.LoadPlugin<IFormatter>(CurrentPlugin) is not null;
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
