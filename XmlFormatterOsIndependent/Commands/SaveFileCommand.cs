using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.DataSets;

namespace XmlFormatterOsIndependent.Commands
{
    public class SaveFileCommand : BaseDataCommand
    {
        private string data;

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
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filters = data.Filters;
                this.data = await saveFileDialog.ShowAsync(data.View);
                ExecutionDone();
            }
        }


        public override void Execute(object parameter)
        {
            AsyncExecute();
        }

        public override T GetData<T>()
        {
            if (!IsExecuted())
            {
                return default;
            }
            Type type = typeof(T);
            return type == typeof(string) ? (T)Convert.ChangeType(data, typeof(T)) : default;
        }

        public override bool IsExecuted()
        {
            return !string.IsNullOrWhiteSpace(data);
        }
    }
}
