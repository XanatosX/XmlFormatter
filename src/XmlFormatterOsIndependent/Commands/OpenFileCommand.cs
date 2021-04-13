using Avalonia.Controls;
using System;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.DataSets;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// Open the open file dialog
    /// </summary>
    internal class OpenFileCommand : BaseDataCommand
    {
        /// <summary>
        /// The file selected in the dialog
        /// </summary>
        private string[] data;

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return parameter is FileDialogData;
        }

        /// <inheritdoc/>
        public async override Task AsyncExecute(object parameter)
        {
            if (parameter is FileDialogData data)
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.AllowMultiple = false;
                openFile.Filters = data.Filters;
                this.data = await openFile.ShowAsync(data.View);
                ExecutionDone();
            }
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            AsyncExecute(parameter);
        }

        /// <inheritdoc/>
        public override T GetData<T>()
        {
            if (!IsExecuted())
            {
                return default;
            }

            Type type = typeof(T);
            return type == typeof(string) ? (T)Convert.ChangeType(data[0], typeof(T)) : default;
        }

        /// <inheritdoc/>
        public override bool IsExecuted()
        {
            return data != null && data.Length > 0;
        }
    }
}
