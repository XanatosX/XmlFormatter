using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.Model.Messages;

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

            WeakReferenceMessenger.Default.Register<RequestMainWindowMessage>(this, async (_, e) =>
            {
                e.Reply(this);
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
