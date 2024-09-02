using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    internal partial class AboutWindow : Window
    {
        public AboutWindow() : this(null)
        {
        }

        public AboutWindow(AboutWindowViewModel? aboutWindowView)
        {
            this.InitializeComponent();
            DataContext = aboutWindowView;
        }
    }
}
