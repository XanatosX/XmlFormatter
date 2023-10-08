using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using XmlFormatter.Application.Services.UpdateFeature;
using XmlFormatter.Domain.Enums;

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

        }

        private string? GetLanguageDependedDescription(Assembly assembly)
        {
            var name = CultureInfo.CurrentCulture.ThreeLetterISOLanguageName;
            string fileName = Properties.Properties.AboutWindow_Tab_General_Description_File.Replace(".md", $".{name}.md");
            return GetDataFromResourceFile(fileName, assembly);
        }

        private string? GetBackupDependedDescription(Assembly assembly)
        {
            return GetDataFromResourceFile(Properties.Properties.AboutWindow_Tab_General_Description_File, assembly);
        }

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