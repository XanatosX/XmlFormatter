using Avalonia.Controls;
using System;
using System.Windows.Input;
using XmlFormatterOsIndependent.DataSets;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// This command will close the current window
    /// </summary>
    internal class CloseWindowCommand : BaseCommand
    {
        private readonly Window targetWindow;

        public CloseWindowCommand(Window targetWindow)
        {
            this.targetWindow = targetWindow;
        }

        public override bool CanExecute(object parameter)
        {
            return targetWindow != null;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            targetWindow.Close();
        }
    }
}
