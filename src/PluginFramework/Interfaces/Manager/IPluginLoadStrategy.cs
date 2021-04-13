using PluginFramework.Interfaces.PluginTypes;
using System.Collections.Generic;

namespace PluginFramework.Interfaces.Manager
{
    /// <summary>
    /// Strategy to load a plugin
    /// </summary>
    public interface IPluginLoadStrategy
    {
        /// <summary>
        /// Load all the plugins with this strategy
        /// </summary>
        /// <typeparam name="T">Type of the plugin to load</typeparam>
        /// <returns>A list with all the loaded plugins</returns>
        List<T> LoadPlugins<T>() where T : IPluginOverhead;
    }
}
