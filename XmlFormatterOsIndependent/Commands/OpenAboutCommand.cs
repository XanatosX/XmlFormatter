using Avalonia.Controls;
using System;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.Views;

namespace XmlFormatterOsIndependent.Commands
{
    public class OpenAboutCommand : BaseDataCommand
    {
        public async override Task AsyncExecute(object parameter)
        {
            Execute(parameter);
        }

        public override bool CanExecute(object parameter)
        {
            if (parameter is Window)
            {
                return true;
            }
            return false;
        }

        public override void Execute(object parameter)
        {
            if (parameter is Window data)
            {
                AboutWindow aboutWindow = new AboutWindow();
                aboutWindow.ShowDialog(data);
            }

        }

        public override T GetData<T>()
        {
            return default;
        }

        public override bool IsExecuted()
        {
            return false;
        }
    }
}
