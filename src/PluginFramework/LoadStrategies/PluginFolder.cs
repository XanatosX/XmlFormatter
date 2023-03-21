using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PluginFramework.LoadStrategies
{
    /// <summary>
    /// Load strategy to load all plugins in a folder
    /// </summary>
    public class PluginFolder : IPluginLoadStrategy
    {
        /// <summary>
        /// The folder to search the plugins ins
        /// </summary>
        private readonly string folder;

        public PluginFolder() : this(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins"))
        {

        }

        /// <summary>
        /// Create a new instance of this load strategy
        /// </summary>
        /// <param name="folder">The folder to search for plugins</param>
        public PluginFolder(string folder)
        {
            this.folder = folder;
        }

        /// <inheritdoc/>
        public List<T> LoadPlugins<T>() where T : IPluginOverhead
        {
            List<T> returnList = new List<T>();
            if (!Directory.Exists(folder))
            {
                return returnList;
            }
            foreach (string file in Directory.GetFiles(folder))
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Extension != ".dll")
                {
                    continue;
                }

                Assembly assembly = Assembly.LoadFrom(file);
                try
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (type.GetInterfaces().Contains(typeof(T)))
                        {
                            T formatter = (T)Activator.CreateInstance(type);
                            returnList.Add(formatter);
                        }
                    }
                }
                catch (Exception)
                {
                    //We just try to instanciate the plugin otherwise we ignore it and don't add it to the list
                }
            }
            return returnList;
        }
    }
}
