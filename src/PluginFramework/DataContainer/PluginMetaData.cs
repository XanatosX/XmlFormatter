using System;

namespace PluginFramework.DataContainer
{
    /// <summary>
    /// A class representing the meta data of the plugin
    /// </summary>
    public class PluginMetaData
    {
        /// <summary>
        /// The plugin information provided by the author
        /// </summary>
        public PluginInformation Information { get; }

        /// <summary>
        /// The unique id of the plugin
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The type of the plugin
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Create a new instance of this meta dataset
        /// </summary>
        /// <param name="id">The id of the plugin</param>
        /// <param name="information">The author information of the plugin</param>
        /// <param name="type">The plugin type</param>
        public PluginMetaData(int id, PluginInformation information, Type type)
        {
            Id = id;
            Information = information;
            Type = type;
        }


    }
}
