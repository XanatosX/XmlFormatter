using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Views;

namespace XmlFormatterOsIndependent.Commands
{
    internal class OpenAboutCommand : BaseDataCommand
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
                AboutWindow aboutWindow = new AboutWindow();
                TaskAwaiter awaiter = aboutWindow.ShowDialog(data.GetWindow()).GetAwaiter();
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
