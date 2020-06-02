using PluginFramework.Interfaces.PluginTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using XmlFormatterOsIndependent.DataSets;

namespace XmlFormatterOsIndependent.Commands
{
    internal class UpdateApplicationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private IDataCommand getStrategyCommand;
        private ICommand executeUpdateStrategyCommand;

        public UpdateApplicationCommand()
        {
            getStrategyCommand = new GetUpdateStrategyCommand();
            executeUpdateStrategyCommand = new ExecuteUpdateStrategyCommand();
        }

        public bool CanExecute(object parameter)
        {
            return getStrategyCommand.CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (getStrategyCommand.CanExecute(parameter))
            {
                getStrategyCommand.Execute(parameter);
                IUpdateStrategy strategy = getStrategyCommand.GetData<IUpdateStrategy>();
                //UpdateApplicationData applicationData = new UpdateApplicationData()
                if (strategy != null)
                {
                    
                }
            }
        }
    }
}
