using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Windows.Input;
using XmlFormatterOsIndependent.DataSets;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// This class will check for new updates and try to update the application
    /// </summary>
    internal class UpdateApplicationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private IDataCommand getStrategyCommand;
        private ICommand executeUpdateStrategyCommand;

        /// <inheritdoc/>
        public UpdateApplicationCommand()
        {
            getStrategyCommand = new GetUpdateStrategyCommand();
            executeUpdateStrategyCommand = new ExecuteUpdateStrategyCommand();
        }

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            if (parameter is UpdateApplicationData data)
            {
                return getStrategyCommand.CanExecute(data.PluginManagmentData);
            }
            return false;
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            if (parameter is UpdateApplicationData data)
            {
                getStrategyCommand.Execute(data.PluginManagmentData);
                IUpdateStrategy strategy = getStrategyCommand.GetData<IUpdateStrategy>();
                if (strategy != null)
                {
                    ExecuteApplicationUpdateData executionData = new ExecuteApplicationUpdateData(strategy, data.VersionCompare);
                    if (executeUpdateStrategyCommand.CanExecute(executionData))
                    {
                        executeUpdateStrategyCommand.Execute(executionData);
                    }

                }
            }
        }
    }
}
