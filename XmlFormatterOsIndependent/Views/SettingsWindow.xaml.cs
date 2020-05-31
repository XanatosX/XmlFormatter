using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PluginFramework.Interfaces.Manager;
using PluginFramework.LoadStrategies;
using PluginFramework.Manager;
using System.IO;
using System.Reflection;
using System.Text;
using XmlFormatterModel.Setting;
using XMLFormatterModel.Setting.InputOutput;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    public class SettingsWindow : Window
    {
        private Window parent;

        public SettingsWindow()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
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
