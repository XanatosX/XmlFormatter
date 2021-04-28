using PluginFramework.DataContainer;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.Models
{
    public class PluginTreeViewItem : PluginMetaData
    {
        public PluginType PluginType { get; }

        public PluginTreeViewItem(PluginMetaData pluginMetaData, PluginType pluginType) : base(pluginMetaData.Id, pluginMetaData.Information, pluginMetaData.Type)
        {
            PluginType = pluginType;
        }


    }
}
