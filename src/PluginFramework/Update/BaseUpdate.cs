using PluginFramework.DataContainer;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.IO;
using System.Reflection;
using XmlFormatterModel.Update;

namespace PluginFramework.Update
{
    /// <summary>
    /// Basic class for a new update plugin
    /// </summary>
    public abstract class BaseUpdate : IUpdateStrategy
    {
        /// <summary>
        /// Settings of the update plugin
        /// </summary>
        public PluginSettings Settings { get; private set; }

        /// <inheritdoc/>
        public PluginInformation Information { get; protected set; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public BaseUpdate()
            : this(new PluginInformation(string.Empty, string.Empty, string.Empty, new Version(0, 0)))
        {

        }

        /// <summary>
        /// Create a new instance of this base plugin class
        /// </summary>
        /// <param name="information">The plugin information to use</param>
        public BaseUpdate(PluginInformation information)
        {
            Information = information;
        }

        /// <inheritdoc/>
        public void ChangeSettings(PluginSettings settings)
        {
            Settings = settings;
        }

        /// <inheritdoc/>
        public abstract bool Update(VersionCompare versionInformation, Predicate<IReleaseAsset> assetFilter);

        /// <summary>
        /// Method to load information from an resource embedded into the plugin
        /// </summary>
        /// <param name="resourcePath">The path to the embedded resource</param>
        /// <returns>The content of the resource or an empty string if nothing was found</returns>
        protected string LoadFromEmbeddedResource(string resourcePath)
        {
            string returnData = string.Empty;
            Assembly assembly = Assembly.GetCallingAssembly();
            if (assembly is null || string.IsNullOrEmpty(resourcePath))
            {
                return returnData;
            }
            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        returnData = reader.ReadToEnd();
                    }
                }
            }


            return returnData;
        }
    }
}
