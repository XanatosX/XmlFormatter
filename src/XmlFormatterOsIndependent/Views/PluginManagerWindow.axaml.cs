using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views
{
    /// <summary>
    /// Window class for the plugin manager
    /// </summary>
    internal partial class PluginManagerWindow : CustomWindowBarWindow
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
    }
}
