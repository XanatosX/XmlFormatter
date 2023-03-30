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
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Input;
using XmlFormatterModel.Setting;
using XmlFormatterModel.Update;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.Models;
using XmlFormatterOsIndependent.Services;
using XmlFormatterOsIndependent.Views;
using static CommunityToolkit.Mvvm.ComponentModel.__Internals.__TaskExtensions.TaskAwaitableWithoutEndValidation;

namespace XmlFormatterOsIndependent.ViewModels
{
    /// <summary>
    /// View model for the main window
    /// </summary>
    public partial class MainWindowViewModel : ObservableObject
    {
        /// <summary>
        /// The modes you could convert to
        /// </summary>
        public List<ModeSelection> ConversionModes { get; }


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

        [ObservableProperty]
        private bool directAppMenuVisible;

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

        private readonly ISettingsManager settingsManager;
        private readonly IPluginManager pluginManager;
        private readonly IVersionManager versionManager;
        private readonly ApplicationUpdateService updateService;
        private readonly IPathService pathService;
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
                                   IVersionManager versionManager,
                                   ApplicationUpdateService updateService,
                                   IPathService pathService,
                                   IIOInteractionService interactionService,
                                   IWindowApplicationService applicationService) // ViewContainer view, 
                                                                                 //: base(settingsManager, pluginManager)
        {
            ConversionModes = new List<ModeSelection>();
            foreach (ModesEnum value in (ModesEnum[])Enum.GetValues(typeof(ModesEnum)))
            {
                ConversionModes.Add(new ModeSelection(value.ToString(), value));
            }

            if (!File.Exists(pathService.GetSettingsFile()))
            {
                settingsManager.Save(pathService.GetSettingsFile());
            }
            StatusString = string.Format(Properties.Resources.MainWindow_Status_Template, string.Empty);

            AvailablePlugins = pluginManager.ListPlugins<IFormatter>().ToList();
            CurrentPlugin = AvailablePlugins.FirstOrDefault();
            SelectedMode = ConversionModes.FirstOrDefault();


            FormatterSelectorVisible = CurrentPlugin is not null;
            FormatterModeSelectionVisible = FormatterSelectorVisible;
            if (CurrentPlugin is null)
            {
                FormatterModeSelectionVisible = false;
                StatusString = string.Format(Properties.Resources.MainWindow_Status_Template, "Missing plugins for conversion!");
            }

            this.settingsManager = settingsManager;
            this.pluginManager = pluginManager;
            this.versionManager = versionManager;
            this.updateService = updateService;
            this.pathService = pathService;
            this.interactionService = interactionService;
            this.applicationService = applicationService;
            availablePlugins ??= new List<PluginMetaData>();

            var operationSystem = WeakReferenceMessenger.Default.Send(new GetOsPlatformMessage());
            DirectAppMenuVisible = true;
            if (operationSystem?.Response == Model.OperationSystemEnum.MacOS)
            {
                DirectAppMenuVisible = false;
            }

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
        public void OpenPlugin()
        {
            applicationService.OpenNewWindow<PluginManagerWindow>();
        }

        [RelayCommand]
        public void OpenAbout()
        {
            applicationService.OpenNewWindow<AboutWindow>();
        }

        [RelayCommand]
        public void OpenSettings()
        {
            applicationService.OpenNewWindow<SettingsWindow>();
        }

        [RelayCommand]
        public void CloseApplication()
        {
            applicationService.CloseApplication();
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
                StatusString = string.Format(Properties.Resources.MainWindow_Status_Template, e.Message);
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
        [RelayCommand]
        public async void SearchForUpdate()
        {
            var compare = await versionManager.RemoteVersionIsNewerAsync();
            var topWindow = applicationService.GetTopMostWindow();
            if (compare is null || topWindow is null)
            {
                return;
            }

            string title = "Version is up to date";
            string content = "You version is up to date";
            ButtonEnum buttons = ButtonEnum.Ok;

            MessageBoxStandardParams parameter = new MessageBoxStandardParams();
            if (compare.GitHubIsNewer)
            {
                title = "Update Available";
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat(
                    "There is a new version available{0}{0}Your version: {1}{0}Remote version: {2}{0}{0}Do you want to update?",
                    Environment.NewLine,
                    compare.LocalVersion,
                    compare.GitHubVersion
                    );
                content = stringBuilder.ToString();
                buttons = ButtonEnum.YesNo;
            }
            parameter.ContentTitle = title;
            parameter.ContentMessage = content;
            parameter.ButtonDefinitions = buttons;
            IMsBoxWindow<ButtonResult> window = MessageBoxManager.GetMessageBoxStandardWindow(parameter);
            var buttonResult = await window.ShowDialog(topWindow);
            if (buttonResult == ButtonResult.Yes)
            {
                bool update = updateService.UpdateApplication(compare);
            }
        }
    }
}
