using PluginFramework.src.DataContainer;
using System.Windows.Forms;

namespace PluginFramework.src.Interfaces.PluginTypes
{
    /// <summary>
    /// Represents the overhead of a plugin this interface needs to be extend for all managable plugins
    /// </summary>
    public interface IPluginOverhead
    {
        PluginSettings Settings { get; }

        /// <summary>
        /// The information for the plugin
        /// </summary>
        PluginInformation Information { get; }

        void ChangeSettings(PluginSettings settings);

        UserControl GetSettingsPage();
    }
}
