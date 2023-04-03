using Avalonia.Controls;
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
using System.IO;
using System.Linq;
using System.Text;
using XmlFormatterModel.Setting;
using XmlFormatterModel.Update;
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
        private bool useNativeMenu;

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
            if (operationSystem?.Response == Model.OperationSystemEnum.MacOS)
            {
                UseNativeMenu = true;
            }

            WeakReferenceMessenger.Default.Register<IsDragDropFileValidMessage>(this, (_, data) =>
            {
                if (data.HasReceivedResponse)
                {
                    return;
                }
                if (string.IsNullOrEmpty(data.FileName) || !File.Exists(data.FileName))
                {
                    data.Reply(false);
                    return;
                }

                var info = new FileInfo(data.FileName);
                var plugin = pluginManager.LoadPlugin<IFormatter>(CurrentPlugin);
                bool valid = info.Exists && plugin?.Extension.ToLower() == info.Extension.Replace(".", string.Empty).ToLower();
                data.Reply(valid);
            });

            WeakReferenceMessenger.Default.Register<DragDropFileChanged>(this, (_, data) =>
            {
                CurrentFile = data.Value;
            });
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
            CurrentFile = data ?? CurrentFile;
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
            IFormatter formatter = pluginManager.LoadPlugin<IFormatter>(CurrentPlugin);
            bool formatIsAllowed = fileIsExisting && new FileInfo(CurrentFile!).Extension.Replace(".", string.Empty) == formatter.Extension;
            return modeAndConverterSelected
                && fileIsExisting
                && formatIsAllowed
                && formatter is not null;
        }

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
