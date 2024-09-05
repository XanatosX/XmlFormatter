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
        [ObservableProperty]
        private ObservableObject? settingsBackupContent;

        [ObservableProperty]
        private ObservableObject? applicationSettingsContent;

        [ObservableProperty]
        private ObservableObject? loggingContent;

        [ObservableProperty]
        private ObservableObject? hotfolderContent;

        [ObservableProperty]
        private IWindowBar windowBar;

        [ObservableProperty]
        private Color themeColor;

        private readonly IWindowApplicationService applicationService;

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
            WindowBar = applicationService.GetWindowBar(Properties.Properties.Default_Window_Icon, Properties.Resources.SettingsWindow_Title);
            var themeResponse = WeakReferenceMessenger.Default.Send(new GetCurrentThemeMessage());
            ThemeColor = themeService.GetColorForTheme(themeResponse.Response);

            settingsBackupContent = resolverService.GetService<ApplicationSettingsBackupViewModel>();
            applicationSettingsContent = resolverService.GetService<ApplicationSettingsViewModel>();

            WeakReferenceMessenger.Default.Register<ThemeChangedMessage>(this, (_, m) => {
                ThemeColor = themeService.GetColorForTheme(m.Value);
            });
        }

        [RelayCommand]
        public void CloseWindow()
        {
            WeakReferenceMessenger.Default.Send(new CloseWindowMessage(WindowId));
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }

        /// <summary>
        /// Save the settings and close this window
        /// </summary>
        public void SaveAndClose()
        {
            WeakReferenceMessenger.Default.Send(new SaveSettingsWindowMessage(true));
            CloseWindowCommand.Execute(null);
        }
    }
}
