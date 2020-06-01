using System;
using System.Windows.Input;
using PluginFramework.Interfaces.Manager;
using ReactiveUI;
using XmlFormatterModel.Setting;
using XmlFormatterOsIndependent.Commands;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        protected readonly ViewContainer view;
        protected readonly ISettingsManager settingsManager;
        protected readonly IPluginManager pluginManager;
        protected readonly string settingsPath;

        public ViewModelBase() : this(null, null, null)
        {

        }

        public ViewModelBase(ViewContainer view, ISettingsManager settingsManager, IPluginManager pluginManager)
        {
            this.view = view;
            this.settingsManager = settingsManager;
            this.pluginManager = pluginManager;
            settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            settingsPath += System.IO.Path.DirectorySeparatorChar + "XmlFormatter";
            settingsPath += System.IO.Path.DirectorySeparatorChar + "settings.set";

            settingsManager.Load(settingsPath);
            ChangeTheme();
        }

        protected void ChangeTheme()
        {
            IDataCommand themeCommand = new GetThemeCommand();
            ExecuteCommand(themeCommand, this.settingsManager);
            ICommand themeSwitchCommand = new SwitchStyleCommand();
            ExecuteCommand(themeSwitchCommand, new ThemeSwitchData(view, themeCommand.GetData<ThemeEnum>()));
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
