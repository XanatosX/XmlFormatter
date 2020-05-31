using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XmlFormatterOsIndependent.DataSets;

namespace XmlFormatterOsIndependent.Commands
{
    internal class CloseWindowCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (parameter is CloseWindowData)
            {
                return true;
            }
            return false;
        }

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

        private void CloseWithoutAsking(Window window)
        {
            window.Close();
        }
    }
}
