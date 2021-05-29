using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using PluginFramework.EventMessages;
using PluginFramework.Interfaces.Manager;
using PluginFramework.Interfaces.PluginTypes;
using System;
using System.IO;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.Commands.Gui;
using XmlFormatterOsIndependent.Commands.SystemCommands;
using XmlFormatterOsIndependent.DataSets.Files;
using XmlFormatterOsIndependent.EventArg;

namespace XmlFormatterOsIndependent.Commands.Conversion
{
    /// <summary>
    /// This command will convertion a input file to a new format based on the given plugin information
    /// </summary>
    class ConvertFileCommand : BaseTriggerCommand
    {
        /// <summary>
        /// The manager to use for loading the plugin instances
        /// </summary>
        private readonly IPluginManager pluginManager;

        /// <summary>
        /// Event if the status for conversion of the file did change
        /// </summary>
        private readonly EventHandler<BaseEventArgs> statusChanged;

        /// <summary>
        /// The save command to use for selection the save file
        /// </summary>
        private SaveFileCommand saveFileSelectionCommand;

        /// <summary>
        /// Is the execution locked right now
        /// </summary>
        private bool executionLocked;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parent">The parent window used for binding the save file dialog to</param>
        /// <param name="pluginManager">The plugin manager used for loading the plugins</param>
        /// <param name="statusChanged">The event which is getting called if the status did change</param>
        public ConvertFileCommand(IPluginManager pluginManager, EventHandler<BaseEventArgs> statusChanged)
        {
            this.pluginManager = pluginManager;
            this.statusChanged = statusChanged;
            executionLocked = false;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return pluginManager != null
                && parameter is SaveFileConversionData
                && !executionLocked
                && statusChanged != null;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (GetMainWindow() == null || !CanExecute(parameter))
            {
                return;
            }
            executionLocked = true;
            TriggerChangedEvent();
            SaveFileConversionData conversionData = parameter as SaveFileConversionData;
            SaveFileCommand saveCommand = GetSaveFileCommand();

            saveCommand.ContinueWith += (sender, data) =>
            {
                //@Todo: There is a bug which will prevent the conversion data to change! THIS MUST BE FIXED BEFORE RELEASING
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

        /// <summary>
        /// Get the current main window
        /// </summary>
        /// <returns>Current main window</returns>
        protected Window GetMainWindow()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                return desktop.MainWindow;
            }
            return null;
        }


        /// <summary>
        /// Get the command which should be used for saving
        /// </summary>
        /// <returns>A usable save command</returns>
        private SaveFileCommand GetSaveFileCommand()
        {
            if (saveFileSelectionCommand != null)
            {
                return saveFileSelectionCommand;
            }
            saveFileSelectionCommand =  new SelectSaveFileConversion(GetMainWindow(), pluginManager, (file, mode) =>
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

        /// <summary>
        /// Get the formatter to use for formatting
        /// </summary>
        /// <param name="data"></param>
        /// <returns>The ready to use formatter</returns>
        private IFormatter GetFormatter(SaveFileConversionData data)
        {
            return pluginManager.LoadPlugin<IFormatter>(data.PluginMeta);
        }
    }
}
