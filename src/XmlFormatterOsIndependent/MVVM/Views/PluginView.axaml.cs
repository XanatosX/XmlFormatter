using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace XmlFormatterOsIndependent.MVVM.Views
{
    public partial class PluginView : UserControl
    {
        public PluginView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
