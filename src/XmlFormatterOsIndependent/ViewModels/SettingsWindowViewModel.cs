using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using XmlFormatter.Application.Services;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.Services;

namespace XmlFormatterOsIndependent.ViewModels
{
    /// <summary>
    /// View model class for the settings window
    /// </summary>
    public partial class SettingsWindowViewModel : ObservableObject, IWindowWithId
    {
        /// <summary>
        /// The content of the backup tab for settings
        /// </summary>
        [ObservableProperty]
        private ObservableObject? settingsBackupContent;

        /// <summary>
        /// The content of the application settings tab
        /// </summary>
        [ObservableProperty]
        private ObservableObject? applicationSettingsContent;

        /// <summary>
        /// The content of the hotfolder tab
        /// </summary>
        [ObservableProperty]
        private ObservableObject? hotfolderContent;

        /// <summary>
        /// The content of the log file
        /// </summary>
        [ObservableProperty]
        private ObservableObject? loggingContent;

        /// <summary>
        /// The custom window bar to use with this window
        /// </summary>
        [ObservableProperty]
        private IWindowBar windowBar;

        /// <summary>
        /// The theme color to use in relation to the theme variant
        /// </summary>
        [ObservableProperty]
        private Color themeColor;

        /// <summary>
        /// The application service to use
        /// </summary>
        private readonly IWindowApplicationService applicationService;

        /// <inheritdoc/>
        public int WindowId => WindowBar is IWindowWithId bar ? bar.WindowId : -1;


        /// <summary>
        /// Create a new instance of this view
        /// </summary>
        /// <param name="view">The view for this model</param>
        /// <param name="settingsManager">The settings manager to use</param>
        /// <param name="pluginManager">The plugin manager to use</param>
        public SettingsWindowViewModel(
            IWindowApplicationService applicationService,
            IDependencyInjectionResolverService resolverService,
            IThemeService themeService)
        {
            this.applicationService = applicationService;
            //TODO: Fix Icon
            WindowBar = applicationService.GetWindowBar(Properties.Properties.Settings_Icon, Properties.Resources.SettingsWindow_Title, false);
            var themeResponse = WeakReferenceMessenger.Default.Send(new GetCurrentThemeMessage());
            ThemeColor = themeService.GetColorForTheme(themeResponse.Response);

            settingsBackupContent = resolverService.GetService<ApplicationSettingsBackupViewModel>();
            applicationSettingsContent = resolverService.GetService<ApplicationSettingsViewModel>();

            WeakReferenceMessenger.Default.Register<ThemeChangedMessage>(this, (_, m) => {
                ThemeColor = themeService.GetColorForTheme(m.Value);
            });
        }

        /// <summary>
        /// Method to close the settings window without saving
        /// This will send out messages to allow other modules to do some final cleanup
        /// </summary>
        [RelayCommand]
        public void CloseWindow()
        {
            WeakReferenceMessenger.Default.Send(new SettingsWindowClosingMessage(false));
            WeakReferenceMessenger.Default.Send(new CloseWindowMessage(WindowId));
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }

        /// <summary>
        /// Save the settings and close this window
        /// </summary>
        public void SaveAndClose()
        {
            WeakReferenceMessenger.Default.Send(new SettingsWindowClosingMessage(true));
            CloseWindowCommand.Execute(null);
        }
    }
}
