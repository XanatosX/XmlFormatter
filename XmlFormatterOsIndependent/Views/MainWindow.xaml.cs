using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PluginFramework.Interfaces.Manager;
using PluginFramework.LoadStrategies;
using PluginFramework.Manager;
using System;
using System.Reflection;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            IPluginManager manager = new DefaultManager();
            string folder = Assembly.GetExecutingAssembly().Location;
            //folder += Environment.
            manager.SetDefaultLoadStrategy(new PluginFolder());
            DataContext = new MainWindowViewModel(new ViewContainer(this, this));
        }
    }
}
