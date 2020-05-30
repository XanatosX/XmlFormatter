using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XmlFormatterOsIndependent.Commands
{
    interface IDataCommand : ICommand
    {
        event EventHandler Executed;

        void CanExecute();
        void Execute();
        Task AsyncExecute();
        Task AsyncExecute(object parameter);
        bool IsExecuted();
        T GetData<T>();
    }
}
