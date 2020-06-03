using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Views;

namespace XmlFormatterOsIndependent.Commands
{
    internal class OpenSettingsCommand : BaseDataCommand
    {
        public async override Task AsyncExecute(object parameter)
        {
            Execute(parameter);
        }

        public override bool CanExecute(object parameter)
        {
            if (parameter is ViewContainer)
            {
                return true;
            }
            return false;
        }

        public override void Execute(object parameter)
        {
            if (parameter is ViewContainer data)
            {
                SettingsWindow settingsWindow = new SettingsWindow();
                settingsWindow.SetParent(data.GetParent());
                TaskAwaiter awaiter = settingsWindow.ShowDialog(data.GetWindow()).GetAwaiter();
                awaiter.OnCompleted(() => ExecutionDone());
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
