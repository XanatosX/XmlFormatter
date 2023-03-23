﻿using PluginFramework.Interfaces.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XmlFormatterModel.Update.Strategies;
using XmlFormatterOsIndependent.Update;

namespace XmlFormatterOsIndependent.ViewModels
{
    /// <summary>
    /// Model view for the about window
    /// </summary>
    public class AboutWindowViewModel
    {
        /// <summary>
        /// The version to show on the screen
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <inheritdoc>
        public AboutWindowViewModel(IEnumerable<IVersionRecieverStrategy> recieverStrategies)
        {
            IVersionRecieverStrategy? localVersionRecieverStrategy = recieverStrategies.FirstOrDefault(strategy => strategy is LocalVersionRecieverStrategy);
            Task<Version>? versionTask = localVersionRecieverStrategy?.GetVersionAsync(new DefaultStringConvertStrategy());
            versionTask?.Wait();

            Version version = versionTask?.Result ?? new Version(0, 0, 0, 0);
            Version = $"{version.Major}.{version.Minor}.{version.Build}";
        }
    }
}
;