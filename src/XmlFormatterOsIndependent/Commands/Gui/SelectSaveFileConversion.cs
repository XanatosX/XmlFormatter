using Avalonia.Controls;
using PluginFramework.DataContainer;
using PluginFramework.Enums;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using XmlFormatterOsIndependent.Commands.SystemCommands;
using XmlFormatterOsIndependent.DataSets.Files;

namespace XmlFormatterOsIndependent.Commands.Gui
{
    /// <summary>
    /// Command to convert the input file to the defined format
    /// </summary>
    class SelectSaveFileConversion : SaveFileCommand
    {
        /// <summary>
        /// The function used to convert the input file name to the output file name
        /// </summary>
        private readonly Func<string, ModesEnum, string> fileConversionFunction;

        /// <summary>
        /// The plugin manager to use for loading plugins
        /// </summary>
        private readonly IPluginManager pluginManager;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parent">The parent window to bind the save dialog window to</param>
        /// <param name="pluginManager">The plugin manager ussed for loading the plugin for conversion</param>
        /// <param name="fileConversionFunction">The function to use for converting the input file to the output file name</param>
        public SelectSaveFileConversion(Window parent, IPluginManager pluginManager, Func<string, ModesEnum, string> fileConversionFunction)
            : base(parent)
        {
            this.fileConversionFunction = fileConversionFunction;
            this.pluginManager = pluginManager;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return parent != null
                && pluginManager != null
                && fileConversionFunction != null
                && parameter != null
                && parameter is SaveFileConversionData;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            SaveFileConversionData dataSet = parameter as SaveFileConversionData;

            string fileName = fileConversionFunction(dataSet.InputFile, dataSet.Mode);
            base.Execute(new SaveFileData(fileName, GetCurrentFilter(dataSet.PluginMeta)));

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