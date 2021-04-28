using System;
using System.Collections.Generic;

namespace XmlFormatterOsIndependent.Models
{
    class PluginTreeViewGroup
    {
        public string Name { get; }

        public IReadOnlyList<PluginTreeViewItem> Items => items;
        private List<PluginTreeViewItem> items;

        public PluginTreeViewGroup(string name)
        {
            Name = name;
            items = new List<PluginTreeViewItem>();
        }

        public void Add(PluginTreeViewItem item)
        {
            if (Contains(item))
            {
                return;
            }

            items.Add(item);
        }

        public bool Contains(PluginTreeViewItem search)
        {
            return Contains(item => item.Id == search.Id);

        }

        public bool Contains(Predicate<PluginTreeViewItem> predicate)
        {
            return items.Find(predicate) != default;
        }


    }
}
