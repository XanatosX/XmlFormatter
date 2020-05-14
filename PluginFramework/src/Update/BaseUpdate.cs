using PluginFramework.src.DataContainer;
using PluginFramework.src.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluginFramework.src.Update
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
        public PluginInformation Information { get; }

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
            this.Settings = settings;
        }

        /// <inheritdoc/>
        public virtual UserControl GetSettingsPage()
        {
            return null;
        }

        /// <inheritdoc/>
        public abstract bool Update(VersionCompare versionInformation);
    }
}
