using System;
using System.Windows.Input;
using PluginFramework.Interfaces.Manager;
using ReactiveUI;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Commands;

namespace XmlFormatterOsIndependent.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        protected readonly ISettingsManager settingsManager;
        protected readonly IPluginManager pluginManager;
        protected readonly string settingsPath;

        public ViewModelBase() : this(null, null)
        {

        }

        public ViewModelBase(ISettingsManager settingsManager, IPluginManager pluginManager)
        {
            this.settingsManager = settingsManager;
            this.pluginManager = pluginManager;
            settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            settingsPath += System.IO.Path.DirectorySeparatorChar + "XmlFormatter";
            settingsPath += System.IO.Path.DirectorySeparatorChar + "settings.set";
        }

        protected void ExecuteCommand(ICommand command, object parameter)
        {
            if (command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }
        }

        protected void ExecuteAsyncCommand(IDataCommand command, object parameter)
        {
            if (command.CanExecute(parameter))
            {
                command.AsyncExecute(parameter);
            }
        }
    }
}
