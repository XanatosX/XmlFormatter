using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterOsIndependent.Commands
{
    abstract class BaseTriggerCommand : BaseCommand, ITriggerCommand
    {
        public event EventHandler ContinueWith;

        public void DataHasChanged()
        {
            TriggerChangedEvent();
        }

        protected void CommandExecuted(EventArgs eventArgs)
        {
            Dispatcher.UIThread.InvokeAsync(() => ContinueWith?.Invoke(this, eventArgs));
        }
    }
}
