using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MessageBox.Avalonia;
using MessageBox.Avalonia.BaseWindows.Base;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using XmlFormatter.Application;
using XmlFormatter.Application.Services;
using XmlFormatter.Application.Services.UpdateFeature;
using XmlFormatter.Domain.Enums;
using XmlFormatter.Domain.PluginFeature.FormatterFeature;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.Model;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.Models;
using XmlFormatterOsIndependent.Services;
using XmlFormatterOsIndependent.Views;

namespace XmlFormatterOsIndependent.ViewModels
{
    /// <summary>
    /// View model for the main window
    /// </summary>
    internal partial class MainWindowViewModel : ObservableObject
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

        /// <summary>
        /// Use the native menur of the os
        /// </summary>
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

        /// <summary>
        /// The repository to use for loading application settings
        /// </summary>
        private readonly ISettingsRepository<ApplicationSettings> settingsRepository;

        /// <summary>
        /// The plugin manager used for loading the data
        /// </summary>
        private readonly IPluginManager pluginManager;

        /// <summary>
        /// The version manager used to get version information
        /// </summary>
        private readonly IVersionManager versionManager;

        /// <summary>
        /// The service used to update the application
        /// </summary>
        private readonly ApplicationUpdateService updateService;

        /// <summary>
        /// Service used to interaction with the io of the system
        /// </summary>
        private readonly IUrlService urlService;

        /// <summary>
        /// Service used for the everything related to windows
        /// </summary>
        private readonly IWindowApplicationService applicationService;

        /// <summary>
        /// Create a new instance of this main window viewer
        /// </summary>
        /// <param name="settingsRepository">The settings repository to use</param>
        /// <param name="pluginManager">The plugin manager to use</param>
        /// <param name="versionManager">The version manager to use</param>
        /// <param name="updateService">The update service to use</param>
        /// <param name="urlService">The interaction service to use</param>
        /// <param name="applicationService">The application service to use</param>
        /// <param name="themeService">The theme service to use</param>
        public MainWindowViewModel(ISettingsRepository<ApplicationSettings> settingsRepository,
                                     IPluginManager pluginManager,
                                     IVersionManager versionManager,
                                     ApplicationUpdateService updateService,
                                     IUrlService urlService,
                                     IWindowApplicationService applicationService,
                                     IThemeService themeService,
                                     ResourceTranslationService resourceTranslationService)
        {
            this.settingsRepository = settingsRepository;
            this.pluginManager = pluginManager;
            this.versionManager = versionManager;
            this.updateService = updateService;
            this.urlService = urlService;
            this.applicationService = applicationService;

            ConversionModes = Enum.GetValues(typeof(ModesEnum))
                                  .Cast<ModesEnum>()
                                  .OfType<ModesEnum>()
                                  .Select(item => new ModeSelection(resourceTranslationService.GetTranslation($"Mode_{item}") ?? item.ToString(), item))
                                  .ToList();

            var settingFile = settingsRepository.CreateOrLoad();
            themeService.ChangeTheme(settingFile?.Theme ?? ThemeEnum.Light);
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

            var operationSystem = WeakReferenceMessenger.Default.Send(new GetOsPlatformMessage());
            UseNativeMenu = operationSystem?.Response == Model.OperationSystemEnum.MacOS;

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

        /// <summary>
        /// Open a file for the currently selected formatter plugin
        /// </summary>
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

        private static FileDialogFilter CreatePluginFileFilter(IFormatter plugin)
        {
            return new FileDialogFilter { Extensions = new() { plugin.Extension }, Name = $"{plugin.Extension}-file" };
        }

        /// <summary>
        /// Check if it is currently possible to open a file and set it for conversion
        /// </summary>
        /// <returns>True if open file is enabled</returns>
        public bool CanOpenFile()
        {
            return CurrentPlugin is not null
                   && SelectedMode is not null
                   && pluginManager.LoadPlugin<IFormatter>(CurrentPlugin) is not null;
        }

        /// <summary>
        /// Open the plugin manager window of the application
        /// </summary>
        [RelayCommand]
        public void OpenPlugin()
        {
            applicationService.OpenNewWindow<PluginManagerWindow>();
        }

        /// <summary>
        /// Open the about window of the application
        /// </summary>
        [RelayCommand]
        public void OpenAbout()
        {
            applicationService.OpenNewWindow<AboutWindow>();
        }

        /// <summary>
        /// Open the settings window of the application
        /// </summary>
        [RelayCommand]
        public void OpenSettings()
        {
            applicationService.OpenNewWindow<SettingsWindow>();
        }

        /// <summary>
        /// Close the application
        /// </summary>
        [RelayCommand]
        public void CloseApplication()
        {
            applicationService.CloseApplication();
        }

        /// <summary>
        /// Open an web url on the system default browser
        /// </summary>
        /// <param name="url">The url to open</param>
        [RelayCommand]
        public void OpenUrl(string url)
        {

            urlService.OpenUrl(url);
        }

        /// <summary>
        /// Convert the current file with the current plugin and mode.
        /// Save the data to a new file
        /// </summary>
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

        /// <summary>
        /// Check if the file can be converted
        /// </summary>
        /// <returns>True if the conversion can be done</returns>
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
