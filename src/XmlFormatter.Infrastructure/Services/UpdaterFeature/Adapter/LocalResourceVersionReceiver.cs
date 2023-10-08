using System.Diagnostics;
using System.Reflection;
using XmlFormatter.Application.Services.UpdateFeature;
using XmlFormatter.Domain.Enums;
using XmlFormatter.Domain.PluginFeature;
using XmlFormatter.Domain.PluginFeature.UpdateStrategyFeature;

namespace XmlFormatterModel.Update.Adapter
{
    /// <summary>
    /// Implementation of the version receiver strategy to get the local version of the application
    /// </summary>
    public abstract class LocalResourceVersionReceiver : IVersionReceiverStrategy
    {
        /// <inheritdoc/>
        public ScopeEnum Scope => ScopeEnum.Local;

        /// <inheritdoc/>
        public event EventHandler<BaseEventArgs> Error;

        /// <inheritdoc/>
        public Task<IRelease> GetLatestReleaseAsync()
        {
            return default;
        }

        /// <inheritdoc/>
        public Task<List<IRelease>> GetReleasesAsync()
        {
            return default;
        }

        /// <inheritdoc/>
        public Task<Version> GetVersionAsync(IVersionConvertStrategy convertStrategy)
        {
            Assembly assembly = GetAssembly();
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;

            return Task.Run(() => convertStrategy.ConvertStringToVersion(versionInfo ?? "0.0.0"));
        }

        /// <summary>
        /// Get the path to the resource to load
        /// </summary>
        /// <returns>A useable string path to the matching resource</returns>
        protected abstract string GetResourcePath();

        /// <summary>
        /// Get the correct assembly to load the resource from
        /// </summary>
        /// <returns>The correct assembly</returns>
        protected abstract Assembly GetAssembly();
    }
}
