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
    public partial class SettingsWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableObject? settingsBackupContent;

        [ObservableProperty]
        private ObservableObject? applicationSettingsContent;

        [ObservableProperty]
        private ObservableObject? loggingContent;

        [ObservableProperty]
        private ObservableObject? hotfolderContent;

        private readonly IWindowApplicationService applicationService;

        /// <summary>
        /// Create a new instance of this view
        /// </summary>
        /// <param name="view">The view for this model</param>
        /// <param name="settingsManager">The settings manager to use</param>
        /// <param name="pluginManager">The plugin manager to use</param>
        public SettingsWindowViewModel(
            IWindowApplicationService applicationService,
            IDependencyInjectionResolverService resolverService)
        {
            this.applicationService = applicationService;

            settingsBackupContent = resolverService.GetService<ApplicationSettingsBackupViewModel>();
            applicationSettingsContent = resolverService.GetService<ApplicationSettingsViewModel>();
        }

        [RelayCommand]
        public void CloseWindow()
        {
            applicationService.CloseActiveWindow();
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
