using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.MVVM.ViewModels;

namespace XmlFormatterOsIndependent.MVVM.Views
{
    [Obsolete]
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            DataContext = new MainWindowViewModel(new ViewContainer(this, this), DefaultManagerFactory.GetSettingsManager(), DefaultManagerFactory.GetPluginManager());
        }
    }
}
