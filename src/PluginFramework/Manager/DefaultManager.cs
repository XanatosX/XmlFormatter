using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PluginFramework.Manager
{
    /// <summary>
    /// Default plugin manager
    /// </summary>
    public class DefaultManager : IPluginManager
    {
        /// <summary>
        /// A list with all the plugins
        /// </summary>
        private readonly List<PluginMetaData> plugins;

        /// <summary>
        /// A list with all the plugin types which got loaded already
        /// </summary>
        private readonly List<Type> loadedTypes;

        /// <summary>
        /// Default loading strategy to use
        /// </summary>
        private IPluginLoadStrategy defaultStrategy;

        /// <summary>
        /// The next id of the plugin
        /// </summary>
        private int nextId;

        /// <summary>
        /// Create a new instance of this plugin manager
        /// </summary>
        public DefaultManager()
        {
            plugins = new List<PluginMetaData>();
            loadedTypes = new List<Type>();
            nextId = 0;
        }

        /// <inheritdoc/>
        public void SetDefaultLoadStrategy(IPluginLoadStrategy loadStrategy)
        {
            defaultStrategy = loadStrategy ?? defaultStrategy;
        }

        /// <inheritdoc/>
        public List<PluginMetaData> ListPlugins<T>() where T : IPluginOverhead
        {
            return ListPlugins<T>(false);
        }

        /// <inheritdoc/>
        public List<PluginMetaData> ListPlugins<T>(bool reload) where T : IPluginOverhead
        {
            return ListPlugins<T>(defaultStrategy, false);
        }

        /// <inheritdoc/>
        public List<PluginMetaData> ListPlugins<T>(IPluginLoadStrategy loadStrategy) where T : IPluginOverhead
        {
            return ListPlugins<T>(loadStrategy, false);
        }

        /// <inheritdoc/>
        public List<PluginMetaData> ListPlugins<T>(IPluginLoadStrategy loadStrategy, bool reload) where T : IPluginOverhead
        {
            if (loadStrategy == null)
            {
                return null;
            }

            LoadPluginsOfType<T>(loadStrategy, reload);
            return plugins.FindAll(obj => obj.Type.GetInterfaces().Contains(typeof(T)));
        }

        /// <summary>
        /// Load all plugins of a given type
        /// </summary>
        /// <typeparam name="T">The type to load the plugin for</typeparam>
        /// <param name="loadStrategy">The strategy to load the plugins</param>
        /// <param name="reload">Should we delete all plugins with the given type</param>
        private void LoadPluginsOfType<T>(IPluginLoadStrategy loadStrategy, bool reload) where T : IPluginOverhead
        {
            if (reload)
            {
                plugins.RemoveAll(plugin => plugin.Type.GetInterfaces().Contains(typeof(T)));
                loadedTypes.Remove(typeof(T));
            }

            if (!loadedTypes.Contains(typeof(T)))
            {
                foreach (T plugin in loadStrategy.LoadPlugins<T>())
                {
                    plugins.Add(new PluginMetaData(nextId, plugin.Information, plugin.GetType()));
                    nextId++;
                }
                loadedTypes.Add(typeof(T));
            }
        }

        /// <inheritdoc/>
        public T LoadPlugin<T>(PluginMetaData metaData) where T : IPluginOverhead
        {
            return LoadPlugin<T>(metaData, null);
        }

        /// <inheritdoc/>
        public T LoadPlugin<T>(PluginMetaData metaData, PluginSettings settings) where T : IPluginOverhead
        {
            return LoadPlugin<T>(metaData.Id, settings);
        }

        /// <inheritdoc/>
        public T LoadPlugin<T>(int id) where T : IPluginOverhead
        {
            return LoadPlugin<T>(id, null);
        }

        /// <inheritdoc/>
        public T LoadPlugin<T>(int id, PluginSettings settings) where T : IPluginOverhead
        {
            PluginMetaData metaData = plugins.Find(plugin => plugin.Id == id);
            T pluginInstance = metaData == null ? default : (T)Activator.CreateInstance(plugins[id].Type);
            if (pluginInstance != null && settings != null)
            {
                pluginInstance.ChangeSettings(settings);
            }
            return pluginInstance;
        }

        /// <inheritdoc/>
        public T LoadPlugin<T>(string type) where T : IPluginOverhead
        {
            return LoadPlugin<T>(type, null);
        }

        /// <inheritdoc/>
        public T LoadPlugin<T>(string type, PluginSettings settings) where T : IPluginOverhead
        {
            if (!loadedTypes.Contains(typeof(T)))
            {
                LoadPluginsOfType<T>(defaultStrategy, false);
            }
            PluginMetaData metaData = plugins.Find(plugin => plugin.Type.ToString() == type);
            return metaData != null ? LoadPlugin<T>(metaData.Id) : default;
        }
    }
}
