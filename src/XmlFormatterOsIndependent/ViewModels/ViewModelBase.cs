using PluginFramework.Interfaces.Manager;
using ReactiveUI;
using System;
using System.Windows.Input;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.DataSets;

namespace XmlFormatterOsIndependent.ViewModels
{
    /// <summary>
    /// Base view model class
    /// </summary>
    public class ViewModelBase : ReactiveObject
    {
        /// <summary>
        /// The current view of this model
        /// </summary>
        [Obsolete]
        protected readonly ViewContainer view;

        /// <summary>
        /// The settings manager to use
        /// </summary>
        protected readonly ISettingsManager settingsManager;

        /// <summary>
        /// The plugin manager to use
        /// </summary>
        protected readonly IPluginManager pluginManager;

        /// <summary>
        /// The path to the settings file
        /// </summary>
        protected readonly string settingsPath;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        [Obsolete]
        public ViewModelBase() : this(null, null)
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="view">The view for this model</param>
        /// <param name="settingsManager">The settings manager for this model</param>
        /// <param name="pluginManager">The plugin manager for this model</param>
        public ViewModelBase(ISettingsManager settingsManager, IPluginManager pluginManager) //ViewContainer view, 
        {
            this.view = view;
            this.settingsManager = settingsManager;
            this.pluginManager = pluginManager;
            settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            settingsPath += System.IO.Path.DirectorySeparatorChar + "XmlFormatter";
            settingsPath += System.IO.Path.DirectorySeparatorChar + "settings.set";

            settingsManager.Load(settingsPath);
        }

        /// <summary>
        /// Execute a given command if possible
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <param name="parameter">The parameter to use</param>
        [Obsolete]
        protected void ExecuteCommand(ICommand command, object parameter)
        {
            if (command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }
        }
    }
}
