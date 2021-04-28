using Avalonia.Controls;
using Avalonia.Threading;
using PluginFramework.EventMessages;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.IO;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.Commands.Gui;
using XmlFormatterOsIndependent.DataSets.Files;
using XmlFormatterOsIndependent.EventArg;

namespace XmlFormatterOsIndependent.Commands.Conversion
{
    class ConvertFileCommand : BaseTriggerCommand
    {
        private readonly Window parent;
        private readonly IPluginManager pluginManager;
        private readonly EventHandler<BaseEventArgs> statusChanged;
        private SaveFileCommand saveCommand;
        private bool executionLocked;

        public ConvertFileCommand(Window parent, IPluginManager pluginManager, EventHandler<BaseEventArgs> statusChanged)
        {
            this.parent = parent;
            this.pluginManager = pluginManager;
            this.statusChanged = statusChanged;
            executionLocked = false;
        }
        public override bool CanExecute(object parameter)
        {
            return pluginManager != null
                && parameter is SaveFileConversionData
                && !executionLocked
                && statusChanged != null;
        }

        public override void Execute(object parameter)
        {
            executionLocked = true;
            TriggerChangedEvent();
            SaveFileConversionData conversionData = parameter as SaveFileConversionData;
            SaveFileCommand saveCommand = GetSaveFileCommand();

            

            saveCommand.ContinueWith += (sender, data) =>
            {
                if (data is FileSelectedArg saveFile)
                {
                    if (saveFile == null
                    || saveFile.SelectedFile == null
                    || saveFile.SelectedFile == string.Empty)
                    {
                        executionLocked = false;
                        TriggerChangedEvent();
                        CommandExecuted(null);
                        return;
                    }
                    IFormatter converter = GetFormatter(conversionData);
                    converter.StatusChanged += statusChanged;
                    Task<bool> conversionTask = Task.Run(() => converter.ConvertToFormat(conversionData.InputFile, saveFile.SelectedFile, conversionData.Mode));
                    conversionTask.ContinueWith((result) =>
                    {
                    executionLocked = false;
                    Dispatcher.UIThread.InvokeAsync(() => {
                            TriggerChangedEvent();
                            CommandExecuted(new BoolArg(result.Result));
                        });
                        
                    });
                }
            };
            saveCommand.Execute(conversionData);
        }

        private SaveFileCommand GetSaveFileCommand()
        {
            if (saveCommand != null)
            {
                return saveCommand;
            }
            saveCommand =  new SelectSaveFileConversion(parent, pluginManager, (file, mode) =>
            {
                FileInfo fileInfo = new FileInfo(file);
                string fileName = fileInfo.Name;
                fileName = fileName.Replace(fileInfo.Extension, "");
                fileName += "_" + mode.ToString();
                fileName += fileInfo.Extension;
                return fileName;
            });
            return GetSaveFileCommand();
        }

        private IFormatter GetFormatter(SaveFileConversionData data)
        {
            return pluginManager.LoadPlugin<IFormatter>(data.PluginMeta);
        }
    }
}
