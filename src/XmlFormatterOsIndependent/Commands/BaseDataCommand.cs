using System;
using System.Threading.Tasks;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// This class is a base class for any data driven command
    /// </summary>
    [Obsolete]
    public abstract class BaseDataCommand : IDataCommand
    {
        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public event EventHandler Executed;

        /// <inheritdoc/>
        public virtual bool CanExecute()
        {
            return CanExecute(null);
        }

        /// <inheritdoc/>
        public virtual void Execute()
        {
            Execute(null);
        }

        /// <inheritdoc/>
        public abstract bool CanExecute(object parameter);

        /// <inheritdoc/>
        public abstract void Execute(object parameter);

        /// <inheritdoc/>
        public abstract T GetData<T>();

        /// <inheritdoc/>
        public abstract bool IsExecuted();

        /// <inheritdoc/>
        public Task AsyncExecute()
        {
            return AsyncExecute(null);
        }

        /// <inheritdoc/>
        public abstract Task AsyncExecute(object parameter);

        /// <summary>
        /// Trigger the event that the execution was done
        /// </summary>
        protected void ExecutionDone()
        {
            EventHandler handler = Executed;
            handler?.Invoke(this, null);
        }
    }
}
