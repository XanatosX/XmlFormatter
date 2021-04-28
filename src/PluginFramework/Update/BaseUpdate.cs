using PluginFramework.DataContainer;
using PluginFramework.Interfaces.PluginTypes;
using System;
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

        public BaseUpdate()
            : this(new PluginInformation(string.Empty, string.Empty, string.Empty, new Version(0,0)))
        {

        }

        /// <summary>
        /// Create a new intsance of this base plugin calss
        /// </summary>
        /// <param name="information">The plugin information to use</param>
        protected BaseUpdate(PluginInformation information)
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
    }
}
