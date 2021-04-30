using System;
using System.Windows.Input;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// Interface used to define command which will talk back to the main thread
    /// </summary>
    public interface ITriggerCommand : ICommand
    {
        /// <summary>
        /// Action to continue with after command was completed
        /// </summary>
        event EventHandler ContinueWith;

        /// <summary>
        /// Method to tell the command that some values have changed and you need to recheck if it can be executed
        /// </summary>
        void DataHasChanged();
    }
}
