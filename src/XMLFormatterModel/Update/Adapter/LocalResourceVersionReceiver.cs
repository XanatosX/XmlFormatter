using PluginFramework.EventMessages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using XmlFormatterModel.Enums;
using XmlFormatterModel.Update.Strategies;

namespace XmlFormatterModel.Update.Adapter
{
    /// <summary>
    /// Implementation of the version reciever strategy to get the local version of the application
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
            string versionString = "0.0.0";
            using (Stream stream = assembly.GetManifestResourceStream(GetResourcePath()))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    versionString = reader.ReadLine();
                }
            }

            return Task.Run(() => convertStrategy.ConvertStringToVersion(versionString));
        }

        /// <summary>
        /// Get the path to the resource to load
        /// </summary>
        /// <returns>A useable string path to the matching resource</returns>
        protected abstract string GetResourcePath();

        /// <summary>
        /// Get the correct assemly to load the resource from
        /// </summary>
        /// <returns>The correct assembly</returns>
        protected abstract Assembly GetAssembly();
    }
}
