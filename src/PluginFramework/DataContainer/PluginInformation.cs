using System;
using System.Collections.Generic;

namespace PluginFramework.DataContainer
{
    /// <summary>
    /// This class represents all the information of the plugin
    /// </summary>
    public class PluginInformation
    {
        /// <summary>
        /// The name of the plugin
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The description of the plugin
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The author name of the plugin
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// The version of the plugin
        /// </summary>
        public Version Version { get; }

        /// <summary>
        /// All the used third party libraries for this extension
        /// </summary>
        public IReadOnlyList<ThirdPartyLibrary> Libraries;

        /// <summary>
        /// Create a new plugin information instance
        /// </summary>
        /// <param name="name">Name of the plugin</param>
        /// <param name="description">Description of the plugin</param>
        /// <param name="author">Author name of the plugin</param>
        /// <param name="version">The version of the plugin</param>
        public PluginInformation(
            string name,
            string description,
            string author,
            Version version
            )
            : this(name, description, author, version, new List<ThirdPartyLibrary>())
        {
        }

        /// <summary>
        /// Create a new plugin information instance
        /// </summary>
        /// <param name="name">Name of the plugin</param>
        /// <param name="description">Description of the plugin</param>
        /// <param name="author">Author name of the plugin</param>
        /// <param name="version">The version of the plugin</param>
        /// <param name="libraries">Used libraries for this extension</param>
        public PluginInformation(
            string name,
            string description,
            string author,
            Version version,
            List<ThirdPartyLibrary> libraries
            )
        {
            Name = name;
            Description = description;
            Author = author;
            Version = version;
            Libraries = libraries;
        }
    }
}
