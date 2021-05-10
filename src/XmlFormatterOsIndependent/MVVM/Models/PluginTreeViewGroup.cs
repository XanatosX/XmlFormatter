using System;
using System.Collections.Generic;

namespace XmlFormatterOsIndependent.MVVM.Models
{
    /// <summary>
    /// Class to store information for the groups on the plugin tree
    /// </summary>
    class PluginTreeViewGroup
    {
        /// <summary>
        /// The name to display
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The subitems for this group
        /// </summary>
        public IReadOnlyList<PluginTreeViewItem> Items => items;

        /// <summary>
        /// The private access for the sub items
        /// </summary>
        private List<PluginTreeViewItem> items;

        /// <summary>
        /// Create a new instance for a tree group
        /// </summary>
        /// <param name="name">The name to display</param>
        public PluginTreeViewGroup(string name)
        {
            Name = name;
            items = new List<PluginTreeViewItem>();
        }

        /// <summary>
        /// Add a new subitem to this group
        /// </summary>
        /// <param name="item">The item to add</param>
        public void Add(PluginTreeViewItem item)
        {
            if (Contains(item))
            {
                return;
            }

            items.Add(item);
        }

        /// <summary>
        /// Add all items in a list to this group
        /// </summary>
        /// <param name="items">The list with all the items to add</param>
        public void Add(List<PluginTreeViewItem> items)
        {
            foreach(PluginTreeViewItem item in items)
            {
                this.items.Add(item);
            }
        }

        /// <summary>
        /// Check if a item is already in the group
        /// </summary>
        /// <param name="search">The plugin to search for</param>
        /// <returns>True if the item is included</returns>
        public bool Contains(PluginTreeViewItem search)
        {
            return Contains(item => item.Id == search.Id);

        }

        /// <summary>
        /// Check if a item is already in the group
        /// </summary>
        /// <param name="predicate">Predicate to check if a item is contained</param>
        /// <returns>True if the item is included</returns>
        public bool Contains(Predicate<PluginTreeViewItem> predicate)
        {
            return items.Find(predicate) != default;
        }


    }
}
