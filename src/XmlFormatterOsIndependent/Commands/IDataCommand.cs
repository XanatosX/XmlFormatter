using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// Command wich will return you some data
    /// </summary>
    [Obsolete]
    public interface IDataCommand : ICommand
    {
        /// <summary>
        /// Was the command already executed
        /// </summary>
        event EventHandler Executed;

        /// <summary>
        /// Can you execute this command without parameter
        /// </summary>
        /// <returns>True if the command can be executed</returns>
        bool CanExecute();

        /// <summary>
        /// Execute this command without parameter
        /// </summary>
        void Execute();

        /// <summary>
        /// Execute this command async
        /// </summary>
        /// <returns></returns>
        Task AsyncExecute();

        /// <summary>
        /// Execute this command async
        /// </summary>
        /// <param name="parameter">The parameter for the command</param>
        /// <returns></returns>
        Task AsyncExecute(object parameter);

        /// <summary>
        /// Was the command already executed
        /// </summary>
        /// <returns></returns>
        bool IsExecuted();

        /// <summary>
        /// Get the data after the execution
        /// </summary>
        /// <typeparam name="T">The type to return</typeparam>
        /// <returns>The data casted to the given type</returns>
        T GetData<T>();
    }
}
