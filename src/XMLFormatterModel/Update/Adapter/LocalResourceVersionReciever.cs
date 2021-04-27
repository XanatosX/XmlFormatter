﻿using PluginFramework.EventMessages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using XmlFormatterModel.Update.Strategies;

namespace XmlFormatterModel.Update.Adapter
{
    public abstract class LocalResourceVersionReciever : IVersionRecieverStrategy
    {
        /// <inheritdoc/>
        public event EventHandler<BaseEventArgs> Error;

        /// <inheritdoc/>
        public Task<IRelease> GetLatestRelease()
        {
            return default;
        }

        /// <inheritdoc/>
        public Task<List<IRelease>> GetReleases()
        {
            return default;
        }

        /// <inheritdoc/>
        public Task<Version> GetVersion(IVersionConvertStrategy convertStrategy)
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
