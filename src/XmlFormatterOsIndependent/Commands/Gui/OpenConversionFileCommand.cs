﻿using Avalonia.Controls;
using PluginFramework.DataContainer;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System.Collections.Generic;
using XmlFormatterOsIndependent.Commands.SystemCommands;

namespace XmlFormatterOsIndependent.Commands.Gui
{
    /// <summary>
    /// Open the open file dialog
    /// </summary>
    internal class OpenConversionFileCommand : OpenFileCommand
    {
        /// <summary>
        /// The plugin manager to use
        /// </summary>
        private readonly IPluginManager pluginManager;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parent">The parent window to use for binding</param>
        /// <param name="pluginManager">The plugin manager used for loading the plugins</param>
        public OpenConversionFileCommand(Window parent, IPluginManager pluginManager)
            : base()
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

            List<FileDialogFilter> filters = GetCurrentFilter(parameter as PluginMetaData);
            base.Execute(filters);
            
        }

        /// <summary>
        /// Get the current filter for file open or save dialog
        /// </summary>
        /// <returns></returns>
        private List<FileDialogFilter> GetCurrentFilter(PluginMetaData currentPlugin)
        {
            List<FileDialogFilter> filters = new List<FileDialogFilter>();

            IFormatter formatter = pluginManager.LoadPlugin<IFormatter>(currentPlugin);
            if (formatter == null)
            {
                return filters;
            }
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