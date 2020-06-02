﻿using PluginFramework.EventMessages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XmlFormatterModel.Update.Strategies
{
    public interface IVersionRecieverStrategy
    {
        /// <summary>
        /// Event if there was any error while reciving the version
        /// </summary>
        event EventHandler<BaseEventArgs> Error;

        /// <summary>
        /// Get the version of the target
        /// </summary>
        /// <param name="convertStrategy">The strategy to use to build the version class from string</param>
        /// <returns>An async task which will get you the version</returns>
        Task<Version> GetVersion(IVersionConvertStrategy convertStrategy);

        /// <summary>
        /// Get all the releases of the target
        /// </summary>
        /// <returns>A list with all the releases</returns>
        Task<List<IRelease>> GetReleases();

        /// <summary>
        /// Get the latest release of the target
        /// </summary>
        /// <returns>The latest release</returns>
        Task<IRelease> GetLatestRelease();
    }
}
