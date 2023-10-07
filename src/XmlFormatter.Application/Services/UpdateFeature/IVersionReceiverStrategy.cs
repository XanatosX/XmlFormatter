using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XmlFormatter.Domain.Enums;
using XmlFormatter.Domain.PluginFeature;
using XmlFormatter.Domain.PluginFeature.UpdateStrategyFeature;

namespace XmlFormatterModel.Update.Strategies
{

    /// <summary>
    /// Interface to receive version from different sources
    /// </summary>
    public interface IVersionReceiverStrategy
    {
        /// <summary>
        /// Event if there was any error while receiving the version
        /// </summary>
        event EventHandler<BaseEventArgs> Error;

        /// <summary>
        /// The scope for the receiver strategy
        /// </summary>
        ScopeEnum Scope { get; }

        /// <summary>
        /// Get the version of the target
        /// </summary>
        /// <param name="convertStrategy">The strategy to use to build the version class from string</param>
        /// <returns>An async task which will get you the version</returns>
        Task<Version> GetVersionAsync(IVersionConvertStrategy convertStrategy);

        /// <summary>
        /// Get all the releases of the target
        /// </summary>
        /// <returns>A list with all the releases</returns>
        Task<List<IRelease>> GetReleasesAsync();

        /// <summary>
        /// Get the latest release of the target
        /// </summary>
        /// <returns>The latest release</returns>
        Task<IRelease> GetLatestReleaseAsync();
    }
}
