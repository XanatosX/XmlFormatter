using Avalonia.Controls;
using PluginFramework.DataContainer;
using PluginFramework.Enums;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Text;
using XmlFormatterOsIndependent.DataSets.Files;

namespace XmlFormatterOsIndependent.Commands.Gui
{
    class SelectSaveFileConversion : SaveFileCommand
    {
        private readonly Func<string, ModesEnum, string> fileConversionFunction;
        private readonly IPluginManager pluginManager;

        public SelectSaveFileConversion(Window parent, IPluginManager pluginManager, Func<string, ModesEnum, string> fileConversionFunction)
            : base(parent)
        {
            this.fileConversionFunction = fileConversionFunction;
            this.pluginManager = pluginManager;
        }

        public override bool CanExecute(object parameter)
        {
            return parent != null
                && pluginManager != null
                && fileConversionFunction != null
                && parameter != null
                && parameter is SaveFileConversionData;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            SaveFileConversionData dataSet = parameter as SaveFileConversionData;

            string fileName = fileConversionFunction(dataSet.InputFile,dataSet.Mode);
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

//CurrentFile
//ModesEnum
//List<FileDialogFilter> dialogFilters