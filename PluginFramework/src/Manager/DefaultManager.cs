using Octokit;
using PluginFramework.src.DataContainer;
using PluginFramework.src.Interfaces.Manager;
using PluginFramework.src.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PluginFramework.src.Manager
{

    public class DefaultManager : IPluginManager
    {
        private readonly List<PluginMetaData> plugins;
        private readonly List<Type> loadedTypes;
        private IPluginLoadStrategy defaultStrategy;
        private int nextId;

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
            return LoadPlugin<T>(metaData.Id);
        }

        /// <inheritdoc/>
        public T LoadPlugin<T>(int id) where T : IPluginOverhead
        {
            PluginMetaData metaData = plugins.Find(plugin => plugin.Id == id);
            return metaData == null ? default :(T)Activator.CreateInstance(plugins[id].Type);
        }

        /// <inheritdoc/>
        public T LoadPlugin<T>(string type) where T : IPluginOverhead
        {
            if (!loadedTypes.Contains(typeof(T)))
            {
                LoadPluginsOfType<T>(defaultStrategy, false);
            }
            PluginMetaData metaData = plugins.Find(plugin => plugin.Type.ToString() == type);
            return metaData != null ? LoadPlugin<T>(metaData) : default;
        }
    }
}
