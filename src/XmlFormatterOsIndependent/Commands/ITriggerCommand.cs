using System;
using System.Windows.Input;

namespace XmlFormatterOsIndependent.Commands
{
    public interface ITriggerCommand : ICommand
    {
        event EventHandler ContinueWith;
        void DataHasChanged();
    }
}
