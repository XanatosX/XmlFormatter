using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XmlFormatterOsIndependent.Commands
{
    class AsyncExecutionCommand : BaseCommand, IDataCommand
    {
        public event EventHandler Executed;

        private readonly BaseDataCommand asyncExecutionCommand;

        public AsyncExecutionCommand(BaseDataCommand asyncExecutionCommand)
        {
            this.asyncExecutionCommand = asyncExecutionCommand;
        }

        public Task AsyncExecute()
        {
            return AsyncExecute(null);
        }

        public Task AsyncExecute(object parameter)
        {
            return asyncExecutionCommand.AsyncExecute(parameter);
        }

        public override bool CanExecute(object parameter)
        {
            return asyncExecutionCommand != null && asyncExecutionCommand.CanExecute(parameter);
        }

        public bool CanExecute()
        {
            return CanExecute(null);
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            //asyncExecutionCommand.AsyncExecute
        }

        public void Execute()
        {
            
        }

        public T GetData<T>()
        {
            return asyncExecutionCommand.GetData<T>();
        }

        public bool IsExecuted()
        {
            return asyncExecutionCommand.IsExecuted();
        }
    }
}
