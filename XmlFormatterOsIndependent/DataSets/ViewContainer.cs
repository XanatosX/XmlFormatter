using Avalonia.Controls;

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

        public Window GetParent()
        {
            return parent;
        }
    }
}
