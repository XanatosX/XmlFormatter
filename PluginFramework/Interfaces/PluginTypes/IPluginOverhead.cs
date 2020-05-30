using PluginFramework.DataContainer;
//using System.Windows.Forms;

namespace PluginFramework.Interfaces.PluginTypes
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

        /// <summary>
        /// This method will allow the host application to change the plugin settings
        /// </summary>
        /// <param name="settings"></param>
        void ChangeSettings(PluginSettings settings);

        /// <summary>
        /// This method will return the settings view for the plugin
        /// </summary>
        /// <returns>A useable control to implement into the host application</returns>
        //UserControl GetSettingsPage();
    }
}
