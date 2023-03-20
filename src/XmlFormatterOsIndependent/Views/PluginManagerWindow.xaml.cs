using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Factories;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    /// <summary>
    /// Window class for the plugin manager
    /// </summary>
    public class PluginManagerWindow : Window
    {
        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public PluginManagerWindow()
        {
            InitializeComponent();
        }

        /// <inheritdoc/>
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            //DefaultManagerFactory managerFactory = new DefaultManagerFactory();

            //DataContext = new PluginManagerViewModel(new ViewContainer(this, this), managerFactory);
        }
    }
}
