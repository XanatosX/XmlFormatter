using Octokit;
using PluginFramework.src.DataContainer;
using System;

namespace XmlFormatter.src.DataContainer
{
    /// <summary>
    /// A class to use with the combobox
    /// </summary>
    public class ComboboxPluginItem
    {
        /// <summary>
        /// The plugin information to display
        /// </summary>
        public PluginInformation Information => metaData.Information;

        /// <summary>
        /// The id of the plugin
        /// </summary>
        public int Id => metaData.Id;

        /// <summary>
        /// The type of the plugin
        /// </summary>
        public Type Type => metaData.Type;

        /// <summary>
        /// The meta data of the plugin
        /// </summary>
        private readonly PluginMetaData metaData;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="metaData">The meta data to use</param>
        public ComboboxPluginItem(PluginMetaData metaData)
        {
            this.metaData = metaData;
        }

        /// <summary>
        /// Overriding the to string method
        /// </summary>
        /// <returns>The name of the plugin</returns>
        public override string ToString()
        {
            return Information.Name; 
        }
    }
}
