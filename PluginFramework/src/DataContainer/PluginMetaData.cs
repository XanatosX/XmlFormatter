using System;

namespace PluginFramework.src.DataContainer
{
    public class PluginMetaData
    {
        /// <summary>
        /// The plugin information provided by the author
        /// </summary>
        public PluginInformation Information => information;

        /// <summary>
        /// Reafonly plugin information provided by author
        /// </summary>
        private readonly PluginInformation information;

        /// <summary>
        /// The unique id of the plugin
        /// </summary>
        public int Id => id;

        /// <summary>
        /// Readonly id of the plugin
        /// </summary>
        private readonly int id;

        /// <summary>
        /// The type of the plugin
        /// </summary>
        public Type Type => type;

        /// <summary>
        /// Readonly type of the plugin
        /// </summary>
        private readonly Type type;

        /// <summary>
        /// Create a new instance of this meta dataset
        /// </summary>
        /// <param name="id">The id of the plugin</param>
        /// <param name="information">The author information of the plugin</param>
        /// <param name="type">The plugin type</param>
        public PluginMetaData(int id, PluginInformation information, Type type)
        {
            this.id = id;
            this.information = information;
            this.type = type;
        }


    }
}
