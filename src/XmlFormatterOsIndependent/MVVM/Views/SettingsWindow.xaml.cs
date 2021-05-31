using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.Views;
using XmlFormatterOsIndependent.MVVM.ViewModels;

namespace XmlFormatterOsIndependent.MVVM.Views
{
    public class SettingsWindow : Window, IParentSetable
    {
        private Window parent;

        public SettingsWindow()
        {
            InitializeComponent();
        }

        public void SetParent(Window parent)
        {
            this.parent = parent;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = new SettingsWindowViewModel(new ViewContainer(this, parent), DefaultManagerFactory.GetSettingsManager(), DefaultManagerFactory.GetPluginManager());
        }
    }
}
