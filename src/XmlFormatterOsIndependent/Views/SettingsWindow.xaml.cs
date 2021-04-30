using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    public class SettingsWindow : Window, IParentSetable
    {
        private Window parent;

        public SettingsWindow()
        {
            this.InitializeComponent();
        }

        public void SetParent(Window parent)
        {
            this.parent = parent;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DefaultManagerFactory managerFactory = new DefaultManagerFactory();
            DataContext = new SettingsWindowViewModel(new ViewContainer(this, parent), managerFactory.GetSettingsManager(), managerFactory.GetPluginManager());
        }
    }
}
