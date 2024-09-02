using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow() : this(null)
        {

        }

        public SettingsWindow(SettingsWindowViewModel? settingsWindowView)
        {
            this.InitializeComponent();
            DataContext = settingsWindowView;
        }
    }
}
