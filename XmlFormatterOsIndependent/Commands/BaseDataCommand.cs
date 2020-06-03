using System;
using System.Threading.Tasks;

namespace XmlFormatterOsIndependent.Commands
{
    public abstract class BaseDataCommand : IDataCommand
    {
        public event EventHandler CanExecuteChanged;
        public event EventHandler Executed;

        public virtual void CanExecute()
        {
            CanExecute(null);
        }

        public virtual void Execute()
        {
            Execute(null);
        }

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

        public abstract T GetData<T>();

        public abstract bool IsExecuted();

        public Task AsyncExecute()
        {
            return AsyncExecute(null);
        }

        public abstract Task AsyncExecute(object parameter);

        protected void ExecutionDone()
        {
            EventHandler handler = Executed;
            handler?.Invoke(this, null);
        }
    }
}
