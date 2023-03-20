using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            WeakReferenceMessenger.Default.Register<CloseApplicationMessage>(this, (_, _) =>
            {
                Close();
            });
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            DefaultManagerFactory managerFactory = new DefaultManagerFactory();

            //DataContext = new MainWindowViewModel(new ViewContainer(this, this), managerFactory.GetSettingsManager(), managerFactory.GetPluginManager());
        }
    }
}
