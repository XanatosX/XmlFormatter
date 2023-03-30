using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// Base command for the ICommand interface
    /// </summary>
    [Obsolete]
    abstract class BaseCommand : ICommand
    {
        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public abstract bool CanExecute(object parameter);

        /// <inheritdoc/>
        public abstract void Execute(object parameter);

        protected void TriggerChangedEvent()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
