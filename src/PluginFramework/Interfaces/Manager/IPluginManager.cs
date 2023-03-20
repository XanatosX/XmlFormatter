using PluginFramework.DataContainer;
using PluginFramework.Interfaces.PluginTypes;
using System.Collections.Generic;

namespace PluginFramework.Interfaces.Manager
{
    /// <summary>
    /// Plugin manager to list and load plugins
    /// </summary>
    public interface IPluginManager
    {
        /// <summary>
        /// Set a default load stratedy which is getting used if you do not provide one
        /// </summary>
        /// <param name="loadStrategy">The default load strategy to set</param>
        void SetDefaultLoadStrategy(IPluginLoadStrategy loadStrategy);

        /// <summary>
        /// List all the plugins of a given type by using the default strategy
        /// </summary>
        /// <typeparam name="T">The type of plugin to list</typeparam>
        /// <returns>A list with the plugin meta data</returns>
        IEnumerable<PluginMetaData> ListPlugins<T>() where T : IPluginOverhead;

        /// <summary>
        /// List all the plugins of a given type by using the default strategy
        /// </summary>
        /// <typeparam name="T">The type of plugin to list</typeparam>
        /// <param name="reload">Reload all the plugins</param>
        /// <returns>A list with the plugin meta data</returns>
        IEnumerable<PluginMetaData> ListPlugins<T>(bool reload) where T : IPluginOverhead;

        /// <summary>
        /// List all the plugins of a given type by
        /// </summary>
        /// <typeparam name="T">The type of plugin to list</typeparam>
        /// <param name="loadStrategy">The strategy to use for loading</param>
        /// <returns>A list with the plugin meta data</returns>
        IEnumerable<PluginMetaData> ListPlugins<T>(IPluginLoadStrategy loadStrategy) where T : IPluginOverhead;

        /// <summary>
        /// List all the plugins of a given type by
        /// </summary>
        /// <typeparam name="T">The type of plugin to list</typeparam>
        /// <param name="loadStrategy">The strategy to use for loading</param>
        /// <param name="reload">Reload all the plugins</param>
        /// <returns>A list with the plugin meta data</returns>
        IEnumerable<PluginMetaData> ListPlugins<T>(IPluginLoadStrategy loadStrategy, bool reload) where T : IPluginOverhead;

        /// <summary>
        /// Load a plugin by the meta data
        /// </summary>
        /// <typeparam name="T">The type of the plugin to load</typeparam>
        /// <param name="metaData">The meta data</param>
        /// <returns>A instance of the plugin</returns>
        T LoadPlugin<T>(PluginMetaData metaData) where T : IPluginOverhead;

        /// <summary>
        /// Load a plugin by the meta data
        /// </summary>
        /// <typeparam name="T">The type of the plugin to load</typeparam>
        /// <param name="metaData">The meta data</param>
        /// <param name="settings">Settings for the plugin</param>
        /// <returns>A instance of the plugin</returns>
        T LoadPlugin<T>(PluginMetaData metaData, PluginSettings settings) where T : IPluginOverhead;

        /// <summary>
        /// Load a plugin by the meta data
        /// </summary>
        /// <typeparam name="T">The type of the plugin to load</typeparam>
        /// <param name="id">The id of the plugin</param>
        /// <returns>A instance of the plugin</returns>
        T LoadPlugin<T>(int id) where T : IPluginOverhead;

        /// <summary>
        /// Load a plugin by the meta data
        /// </summary>
        /// <typeparam name="T">The type of the plugin to load</typeparam>
        /// <param name="id">The id of the plugin</param>
        /// <param name="settings">Settings for the plugin</param>
        /// <returns>A instance of the plugin</returns>
        T LoadPlugin<T>(int id, PluginSettings settings) where T : IPluginOverhead;

        /// <summary>
        /// Load a plugin by the meta data
        /// </summary>
        /// <typeparam name="T">The type of the plugin to load</typeparam>
        /// <param name="type">The type of the plugin</param>
        /// <returns>A instance of the plugin</returns>
        T LoadPlugin<T>(string type) where T : IPluginOverhead;

        /// <summary>
        /// Load a plugin by the meta data
        /// </summary>
        /// <typeparam name="T">The type of the plugin to load</typeparam>
        /// <param name="type">The type of the plugin</param>
        /// <param name="settings">Settings for the plugin</param>
        /// <returns>A instance of the plugin</returns>
        T LoadPlugin<T>(string type, PluginSettings settings) where T : IPluginOverhead;
    }
}