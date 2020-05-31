using Avalonia.Controls;
using System;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.DataSets;

namespace XmlFormatterOsIndependent.Commands
{
    internal class OpenFileCommand : BaseDataCommand
    {
        private string[] data;

        public override bool CanExecute(object parameter)
        {
            if (parameter is FileDialogData)
            {
                return true;
            }
            return false;
        }

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

        public override void Execute(object parameter)
        {
            AsyncExecute(parameter);
        }

        public override T GetData<T>()
        {
            if (!IsExecuted())
            {
                return default;
            }

            Type type = typeof(T);
            return type == typeof(string) ? (T)Convert.ChangeType(data[0], typeof(T)) : default;
        }

        public override bool IsExecuted()
        {
            return data != null && data.Length > 0;
        }
    }
}
