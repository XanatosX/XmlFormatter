using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace XmlFormatterOsIndependent.DataSets
{
    public class ViewContainer
    {
        private readonly Window current;
        private readonly Window parent;

        public ViewContainer(Window current, Window parent)
        {
            this.current = current;
            this.parent = parent;
        }

        public Window GetWindow()
        {
            return current;
        }
        Window GetParent()
        {
            return parent;
        }
    }
}
