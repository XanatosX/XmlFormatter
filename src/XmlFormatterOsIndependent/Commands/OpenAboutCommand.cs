using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Views;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// Open the about view
    /// </summary>
    internal class OpenAboutCommand : BaseDataCommand
    {
        /// <inheritdoc/>
        public async override Task AsyncExecute(object parameter)
        {
            Execute(parameter);
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return parameter is ViewContainer;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (parameter is ViewContainer data)
            {
                AboutWindow aboutWindow = new AboutWindow();
                TaskAwaiter awaiter = aboutWindow.ShowDialog(data.GetWindow()).GetAwaiter();
                awaiter.OnCompleted(() => ExecutionDone());
            }

        }

        /// <inheritdoc/>
        public override T GetData<T>()
        {
            return default;
        }

        /// <inheritdoc/>
        public override bool IsExecuted()
        {
            return false;
        }
    }
}
