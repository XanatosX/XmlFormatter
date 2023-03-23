using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    /// <summary>
    /// Window class for the plugin manager
    /// </summary>
    public class PluginManagerWindow : Window
    {
        public PluginManagerWindow() : this(null)
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public PluginManagerWindow(PluginManagerViewModel? viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        /// <inheritdoc/>
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
