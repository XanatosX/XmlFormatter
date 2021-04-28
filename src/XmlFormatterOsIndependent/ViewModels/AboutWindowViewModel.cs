using PluginFramework.Interfaces.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XmlFormatterModel.Setting;
using XmlFormatterModel.Update.Strategies;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Update;

namespace XmlFormatterOsIndependent.ViewModels
{
    class AboutWindowViewModel : ViewModelBase
    {
        public string Version { get; }

        public AboutWindowViewModel(ViewContainer view, ISettingsManager settingsManager, IPluginManager pluginManager) : base(view, settingsManager, pluginManager)
        {
            LocalVersionRecieverStrategy localVersionRecieverStrategy = new LocalVersionRecieverStrategy();
            Task<Version> versionTask = localVersionRecieverStrategy.GetVersion(new DefaultStringConvertStrategy());







            Version version = versionTask.Result;
            Version = version.Major + "." + version.Minor + "." + version.Build;
        }
    }
}
