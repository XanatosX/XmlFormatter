using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using XmlFormatterOsIndependent.Manager;
using XmlFormatterOsIndependent.MVVM.ViewModels.Behaviors;

namespace XmlFormatterOsIndependent.MVVM.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ThemeManager.RegisterWindow(this);

            if (DataContext is IEventView eventView)
            {
                eventView.RegisterEvents(this);
            }

            
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
