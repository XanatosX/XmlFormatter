using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Octokit;
using PluginFramework.Interfaces.Manager;
using PluginFramework.LoadStrategies;
using PluginFramework.Manager;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Factories;
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
            DefaultManagerFactory managerFactory = new DefaultManagerFactory();

            DataContext = new MainWindowViewModel(new ViewContainer(this, this), managerFactory.GetSettingsManager(), managerFactory.GetPluginManager());
        }
    }
}
