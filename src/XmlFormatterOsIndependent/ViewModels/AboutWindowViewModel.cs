using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using XmlFormatter.Application.Services.UpdateFeature;
using XmlFormatter.Domain.Enums;
using XmlFormatterOsIndependent.Model;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.Services;

namespace XmlFormatterOsIndependent.ViewModels
{
    /// <summary>
    /// Model view for the about window
    /// </summary>
    internal partial class AboutWindowViewModel : ObservableObject, IWindowWithId
    {
        /// <summary>
        /// The version to show on the screen
        /// </summary>
        [ObservableProperty]
        public string version;

        /// <summary>
        /// The description markdown for the application
        /// </summary>
        [ObservableProperty]
        public string? description;

        /// <summary>
        /// A list with third party apps
        /// </summary>
        [ObservableProperty]
        public IReadOnlyList<ThirdPartyAppViewModel> thirdPartyApps;
        
        /// <summary>
        /// The custom window bar to use
        /// </summary>
        [ObservableProperty]
        private IWindowBar windowBar;

        /// <summary>
        /// The theme color for this window
        /// </summary>
        [ObservableProperty]
        private Color themeColor;
        
        /// <summary>
        /// The theme service to use
        /// </summary>
        private readonly IThemeService themeService;

        /// <summary>
        /// Service to use for getting application resources
        /// </summary>
        private readonly ResourceLoaderService resourceLoaderService;

        /// <inheritdoc/>
        public int WindowId => WindowBar is IWindowWithId bar ? bar.WindowId : -1;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <inheritdoc>
        public AboutWindowViewModel(
            IEnumerable<IVersionReceiverStrategy> receiverStrategies,
            IVersionConvertStrategy versionConvertStrategy,
            IWindowApplicationService applicationService,
            IThemeService themeService,
            ResourceLoaderService resourceLoaderService)
        {
            this.themeService = themeService;
            this.resourceLoaderService = resourceLoaderService;

            WindowBar = applicationService.GetWindowBar(Properties.Properties.Default_Window_Icon, Properties.Resources.AboutWindow_Title, false);
            IVersionReceiverStrategy? localVersionReceiverStrategy = receiverStrategies.FirstOrDefault(strategy => strategy.Scope == ScopeEnum.Local);
            Task<Version>? versionTask = localVersionReceiverStrategy?.GetVersionAsync(versionConvertStrategy);
            versionTask?.Wait();

            var theme = themeService.GetCurrentThemeVariant();
            SetThemeColor(theme);

            Version version = versionTask?.Result ?? new Version(0, 0, 0, 0);
            Version = $"{version.Major}.{version.Minor}.{version.Build}";

            var assembly = Assembly.GetExecutingAssembly();
            LoadAndSetDescription(assembly);

            string? thirdPartyAppData = resourceLoaderService.GetResource(Properties.Properties.AboutWindow_Tab_General_ThirdPartyApps_File);
            if (thirdPartyAppData is null)
            {
                return;
            }
            List<ThirdPartyApp> apps = new();
            try
            {
                apps = JsonSerializer.Deserialize<List<ThirdPartyApp>>(thirdPartyAppData) ?? new();
            }
            catch (System.Exception)
            {
                //Third party app loading did fail
            }
            ThirdPartyApps = apps.Where(app => !string.IsNullOrEmpty(app.Name))
                                 .OrderBy(app => app.Name)
                                 .Select(app => new ThirdPartyAppViewModel(app))
                                 .ToList();

            WeakReferenceMessenger.Default.Register<ThemeChangedMessage>(this, (_, data) =>
            {
                SetThemeColor(data.Value);
            });
        }

        /// <summary>
        /// Method to load and set then description for the about window
        /// </summary>
        /// <param name="assembly">The assembly to request the data from</param>
        private void LoadAndSetDescription(Assembly assembly)
        {
            string rawDescription = resourceLoaderService.GetLocalizedString(Properties.Properties.AboutWindow_Tab_General_Description_File);
            Description = rawDescription.Replace("%DISCUSSION_URL%", Properties.Properties.GitHub_Discuss)
                                        .Replace("%ISSUES_URL%", Properties.Properties.GitHub_Issue);
        }

        /// <summary>
        /// Set the theme color for this window based on the theme variant
        /// </summary>
        /// <param name="theme">The theme variant to get set color for</param>
        private void SetThemeColor(ThemeVariant theme)
        {
            ThemeColor = themeService.GetColorForTheme(theme);
        }

    }
}
;