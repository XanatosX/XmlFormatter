using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace XmlFormatterOsIndependent.MVVM.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
