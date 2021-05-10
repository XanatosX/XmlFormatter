using PluginFramework.Interfaces.Manager;
using System;
using System.Threading.Tasks;
using XmlFormatterModel.Setting;
using XmlFormatterModel.Update.Strategies;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Update;

namespace XmlFormatterOsIndependent.MVVM.ViewModels
{
    /// <summary>
    /// Model view for the about window
    /// </summary>
    class AboutWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// The version to show on the screen
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <inheritdoc>
        public AboutWindowViewModel(ViewContainer view, ISettingsManager settingsManager, IPluginManager pluginManager) : base(view, settingsManager, pluginManager)
        {
            LocalVersionRecieverStrategy localVersionRecieverStrategy = new LocalVersionRecieverStrategy();
            Task<Version> versionTask = localVersionRecieverStrategy.GetVersion(new DefaultStringConvertStrategy());

            Version version = versionTask.Result;
            Version = version.Major + "." + version.Minor + "." + version.Build;
        }
    }
}
