using Avalonia.Controls;
using System;
using System.Windows.Input;
using XmlFormatterOsIndependent.DataSets;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// This command will close the current window
    /// </summary>
    internal class CloseWindowCommand : ICommand
    {
        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            if (parameter is CloseWindowData)
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            if (parameter is CloseWindowData data)
            {
                if (data.AskBeforeClosing)
                {
                    return;
                }
                CloseWithoutAsking(data.Window);
            }
        }

        /// <inheritdoc/>
        private void CloseWithoutAsking(Window window)
        {
            window.Close();
        }
    }
}
