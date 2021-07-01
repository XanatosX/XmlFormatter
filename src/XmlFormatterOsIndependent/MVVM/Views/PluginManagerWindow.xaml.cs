using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.MVVM.ViewModels;
using XmlFormatterOsIndependent.MVVM.ViewModels.Main;

namespace XmlFormatterOsIndependent.MVVM.Views
{
    /// <summary>
    /// Window class for the plugin manager
    /// </summary>
    [Obsolete]
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

            DataContext = new PluginManagerViewModel(new ViewContainer(this, this));
        }
    }
}
