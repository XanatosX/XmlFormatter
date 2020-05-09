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
    public abstract class BaseUpdate : IUpdateStrategy
    {
        public PluginSettings Settings { get; private set; }

        /// <inheritdoc/>
        public PluginInformation Information { get; }

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
