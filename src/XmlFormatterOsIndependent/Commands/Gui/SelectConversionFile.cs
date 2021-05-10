using Avalonia.Controls;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System.Collections.Generic;
using XmlFormatterOsIndependent.Commands.SystemCommands;

namespace XmlFormatterOsIndependent.Commands.Gui
{
    /// <summary>
    /// Load a file for conversion
    /// </summary>
    class SelectConversionFile : OpenFileCommand
    {
        /// <summary>
        /// The plugin manager to use for loading
        /// </summary>
        private readonly IPluginManager pluginManager;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="pluginManager"></param>
        public SelectConversionFile(IPluginManager pluginManager)
        {
            this.pluginManager = pluginManager;
        
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter)
                && pluginManager != null
                && parameter != null;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            if (parameter is PluginMetaData data)
            {
                List<FileDialogFilter> filters = GetCurrentFilter(parameter as PluginMetaData);
                base.Execute(filters);
            }
        }

        /// <summary>
        /// Get the current filter for file open or save dialog
        /// </summary>
        /// <returns></returns>
        private List<FileDialogFilter> GetCurrentFilter(PluginMetaData currentPlugin)
        {
            

            IFormatter formatter = pluginManager.LoadPlugin<IFormatter>(currentPlugin);
            if (formatter == null)
            {
                return new List<FileDialogFilter>();
            }

            List<FileDialogFilter> filters = new List<FileDialogFilter>();
            List<string> extensions = new List<string>();
            extensions.Add(formatter.Extension.ToLower());
            filters.Add(new FileDialogFilter()
            {
                Name = formatter.Extension.ToUpper() + "-File",
                Extensions = extensions
            });

            return filters;
        }
    }
}
