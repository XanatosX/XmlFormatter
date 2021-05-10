using PluginFramework.DataContainer;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.MVVM.Models
{
    /// <summary>
    /// This class represents a selectable item in the plugin tree view
    /// </summary>
    public class PluginTreeViewItem : PluginMetaData
    {
        /// <summary>
        /// The type of the plugin
        /// </summary>
        public PluginType PluginType { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="pluginMetaData">The meta data of the plugin</param>
        /// <param name="pluginType">The type of this plugin</param>
        public PluginTreeViewItem(PluginMetaData pluginMetaData, PluginType pluginType) : base(pluginMetaData.Id, pluginMetaData.Information, pluginMetaData.Type)
        {
            PluginType = pluginType;
        }


    }
}
