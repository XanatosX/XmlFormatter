using Avalonia.Threading;
using System;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// Abstract class for wratting the base trigger command
    /// </summary>
    abstract class BaseTriggerCommand : BaseCommand, ITriggerCommand
    {
        /// <inheritdoc/>
        public event EventHandler ContinueWith;

        /// <inheritdoc/>
        public void DataHasChanged()
        {
            TriggerChangedEvent();
        }

        /// <inheritdoc/>
        protected void CommandExecuted(EventArgs eventArgs)
        {
            Dispatcher.UIThread.InvokeAsync(() => ContinueWith?.Invoke(this, eventArgs));
        }
    }
}
