using System;

namespace PluginFramework.src.DataContainer
{
    public class PluginInformation
    {
        /// <summary>
        /// The name of the plugin
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The description of the plugin
        /// </summary>
        public readonly string Description;

        /// <summary>
        /// The author name of the plugin
        /// </summary>
        public readonly string Author;

        /// <summary>
        /// The version of the plugin
        /// </summary>
        public readonly Version Version;

        /// <summary>
        /// Create a new plugin information instance
        /// </summary>
        /// <param name="name">Name of the plugin</param>
        /// <param name="description">Description of the plugin</param>
        /// <param name="author">Author name of the plugin</param>
        /// <param name="version">The version of the plugin</param>
        public PluginInformation(string name, string description, string author, Version version)
        {
            Name = name;
            Author = author;
            Description = description;
            Version = version;
        }
    }
}
