using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    internal class AboutWindow : Window
    {
        public AboutWindow() : this(null)
        {

        }

        public AboutWindow(AboutWindowViewModel? aboutWindowView)
        {
            this.InitializeComponent();
            DataContext = aboutWindowView;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
