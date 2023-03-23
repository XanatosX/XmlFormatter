using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    public class SettingsWindow : Window
    {
        public SettingsWindow() : this(null)
        {

        }

        public SettingsWindow(SettingsWindowViewModel? settingsWindowView)
        {
            this.InitializeComponent();
            DataContext = settingsWindowView;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
