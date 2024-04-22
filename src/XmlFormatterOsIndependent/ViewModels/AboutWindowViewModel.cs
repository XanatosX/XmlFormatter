using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using XmlFormatter.Application.Services.UpdateFeature;
using XmlFormatter.Domain.Enums;
using XmlFormatterOsIndependent.Model;

namespace XmlFormatterOsIndependent.ViewModels
{
    /// <summary>
    /// Model view for the about window
    /// </summary>
    internal partial class AboutWindowViewModel : ObservableObject
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
        /// Create a new instance of this class
        /// </summary>
        /// <inheritdoc>
        public AboutWindowViewModel(
            IEnumerable<IVersionReceiverStrategy> receiverStrategies,
            IVersionConvertStrategy versionConvertStrategy)
        {
            IVersionReceiverStrategy? localVersionReceiverStrategy = receiverStrategies.FirstOrDefault(strategy => strategy.Scope == ScopeEnum.Local);
            Task<Version>? versionTask = localVersionReceiverStrategy?.GetVersionAsync(versionConvertStrategy);
            versionTask?.Wait();

            Version version = versionTask?.Result ?? new Version(0, 0, 0, 0);
            Version = $"{version.Major}.{version.Minor}.{version.Build}";

            var assembly = Assembly.GetExecutingAssembly();
            Description = GetLanguageDependedDescription(assembly) ?? GetBackupDependedDescription(assembly);

            string? thirdPartyAppData = GetDataFromResourceFile(Properties.Properties.AboutWindow_Tab_General_ThirdPartyApps_File, assembly);
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
        }

        /// <summary>
        /// Get the description based on the current culture
        /// </summary>
        /// <param name="assembly">The assembly to load the file from</param>
        /// <returns>The description string or null if nothing was found</returns>
        private string? GetLanguageDependedDescription(Assembly assembly)
        {
            var name = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            string fileName = Properties.Properties.AboutWindow_Tab_General_Description_File.Replace(".md", $".{name}.md");
            return GetDataFromResourceFile(fileName, assembly);
        }

        /// <summary>
        /// Get the fallback description
        /// </summary>
        /// <param name="assembly">The assembly to load the file from</param>
        /// <returns>The description string or null if nothing was found</returns>
        private string? GetBackupDependedDescription(Assembly assembly)
        {
            return GetDataFromResourceFile(Properties.Properties.AboutWindow_Tab_General_Description_File, assembly);
        }

        /// <summary>
        /// Load a resource file from the given resource
        /// </summary>
        /// <param name="filename">The path to the resoruce file</param>
        /// <param name="assembly">The assembly to load the file from</param>
        /// <returns>The file content or an null string if nothing was found</returns>
        private string? GetDataFromResourceFile(string filename, Assembly assembly)
        {
            string? returnString = null;
            try
            {
                using (Stream? stream = assembly.GetManifestResourceStream(filename))
                {
                    if (stream is not null)
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            returnString = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                // The request failed but this is properly okay, I guess
            }

            return returnString;
        }

    }
}
;