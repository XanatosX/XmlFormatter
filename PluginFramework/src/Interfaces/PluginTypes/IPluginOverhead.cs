using PluginFramework.src.DataContainer;

namespace PluginFramework.src.Interfaces.PluginTypes
{
    /// <summary>
    /// Represents the overhead of a plugin this interface needs to be extend for all managable plugins
    /// </summary>
    public interface IPluginOverhead
    {
        /// <summary>
        /// The information for the plugin
        /// </summary>
        PluginInformation Information { get; }
    }
}
