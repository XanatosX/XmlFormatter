﻿using Avalonia.Media;
using Avalonia.Platform.Storage;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MsBox.Avalonia;
using MsBox.Avalonia.Base;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.Application;
using XmlFormatter.Application.Services;
using XmlFormatter.Application.Services.UpdateFeature;
using XmlFormatter.Domain.Enums;
using XmlFormatter.Domain.PluginFeature.FormatterFeature;
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
    internal partial class MainWindowViewModel : ObservableObject, IWindowWithId
    {
        /// <summary>
        /// The modes you could convert to
        /// </summary>
        public List<ModeSelection> ConversionModes { get; }

        /// <summary>
        /// All list with all the available plugins
        /// </summary>
        [ObservableProperty]
        private List<PluginMetaData> availablePlugins;

        /// <summary>
        /// Private storage for current selected plugin
        /// </summary>
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConvertFileCommand))]
        [NotifyCanExecuteChangedFor(nameof(OpenFileCommand))]
        private PluginMetaData? currentPlugin;

        /// <summary>
        /// The selected mode of operation
        /// </summary>
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
        /// Use the native menu of the os
        /// </summary>
        [ObservableProperty]
        private bool useNativeMenu;

        /// <summary>
        /// Private text of the status string
        /// </summary>
        [ObservableProperty]
        private string? statusString;   

        /// <summary>
        /// The custom window bar to use
        /// </summary>
        [ObservableProperty]
        private IWindowBar windowBar;

        /// <summary>
        /// The color to use related to the current theme
        /// </summary>
        [ObservableProperty]
        private Color themeColor;

        /// <inheritdoc/>
        public int WindowId { get => WindowBar is IWindowWithId bar ? bar.WindowId : -1; }

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
        private readonly IThemeService themeService;
        private readonly ResourceLoaderService resourceLoaderService;



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
                                     ResourceLoaderService resourceLoaderService)
        {
            this.settingsRepository = settingsRepository;
            this.pluginManager = pluginManager;
            this.versionManager = versionManager;
            this.updateService = updateService;
            this.urlService = urlService;
            this.applicationService = applicationService;
            this.themeService = themeService;
            this.resourceLoaderService = resourceLoaderService;
            var theme = themeService.GetCurrentThemeVariant();
            SetThemeColor(theme);

            windowBar = applicationService.GetWindowBar();
        

            ConversionModes = Enum.GetValues(typeof(ModesEnum))
                                  .Cast<ModesEnum>()
                                  .OfType<ModesEnum>()
                                  .Select(item => new ModeSelection(item.ToString(), item))
                                  .ToList();

            var settingFile = settingsRepository.CreateOrLoad();
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

            WeakReferenceMessenger.Default.Register<ThemeChangedMessage>(this, (_, data) =>
            {
                SetThemeColor(data.Value);
            });
        }

        /// <summary>
        /// Set the color of the theme related to the current theme variant
        /// </summary>
        /// <param name="theme">The theme variant to set the color for</param>
        private void SetThemeColor(ThemeVariant theme)
        {
            ThemeColor = themeService.GetColorForTheme(theme);
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

        private static FilePickerFileType CreatePluginFileFilter(IFormatter plugin)
        {
            return new FilePickerFileType($"*.{plugin.Extension}-file") { Patterns = new List<string>{ $"*.{plugin.Extension}"  }};
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
            if (CurrentFile is null)
            {
                return;
            }
            var plugin = pluginManager.LoadPlugin<IFormatter>(CurrentPlugin);
            if (plugin is null || SelectedMode is null)
            {
                return;
            }
            List<FilePickerFileType> filters = new() { CreatePluginFileFilter(plugin) };
            var saveFile = await applicationService.SaveFileAsync(filters);
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
        public async Task SearchForUpdate()
        {
            var compare = await versionManager.RemoteVersionIsNewerAsync();
            var topWindow = applicationService.GetTopMostWindow();
            if (compare is null || topWindow is null)
            {
                return;
            }

            string title = Properties.Resources.UpdateDialog_UpToDate_Title;
            string content = resourceLoaderService.GetLocalizedString(Properties.Properties.Update_App_Is_Up_To_Date_File);
            ButtonEnum buttons = ButtonEnum.Ok;

            MessageBoxStandardParams parameter = new MessageBoxStandardParams();
            if (compare.GitHubIsNewer)
            {
                title = Properties.Resources.UpdateDialog_NewVersion_Title;
                content = resourceLoaderService.GetLocalizedString(Properties.Properties.Update_New_Version_Available_File);
                buttons = ButtonEnum.YesNo;
            }
            parameter.ContentTitle = title;
            parameter.ContentMessage = content;
            parameter.ButtonDefinitions = buttons;

            content = content.Replace("%local_version%", compare.LocalVersion.ToString())
                             .Replace("%remote_version%", compare.GitHubVersion.ToString());

            var result = await applicationService.OpenDialogBoxAsync(null, title, new YesNoDialogViewModel(content));
            if (result == Enums.DialogButtonResponses.Yes)
            {
                updateService.UpdateApplication(compare);
            }

        }
    }
}
