using Avalonia.Controls;
using System;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.DataSets;

namespace XmlFormatterOsIndependent.Commands
{
    /// <summary>
    /// Open the save file fialot
    /// </summary>
    public class OpenSaveFileDialogCommand : BaseDataCommand
    {
        /// <summary>
        /// The data of the file dialog
        /// </summary>
        private string data;

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            if (parameter is FileDialogData)
            {
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public async override Task AsyncExecute(object parameter)
        {
            if (parameter is FileDialogData data)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filters = data.Filters;
                saveFileDialog.InitialFileName = data.DefaultFileName;
                this.data = await saveFileDialog.ShowAsync(data.View);
                ExecutionDone();
            }
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            AsyncExecute();
        }

        /// <inheritdoc/>
        public override T GetData<T>()
        {
            if (!IsExecuted())
            {
                return default;
            }
            Type type = typeof(T);
            return type == typeof(string) ? (T)Convert.ChangeType(data, typeof(T)) : default;
        }

        /// <inheritdoc/>
        public override bool IsExecuted()
        {
            return !string.IsNullOrWhiteSpace(data);
        }
    }
}
