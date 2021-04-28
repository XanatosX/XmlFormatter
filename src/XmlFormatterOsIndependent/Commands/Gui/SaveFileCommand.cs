using Avalonia.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.DataSets.Files;
using XmlFormatterOsIndependent.EventArg;

namespace XmlFormatterOsIndependent.Commands.Gui
{
    class SaveFileCommand : BaseTriggerCommand
    {
        protected readonly Window parent;
        protected readonly List<FileDialogFilter> dialogFilters;

        public SaveFileCommand(Window parent)
            : this(parent, new List<FileDialogFilter>())
        {
        }

        public SaveFileCommand(Window parent, List<FileDialogFilter> dialogFilters)
        {
            this.parent = parent;
            this.dialogFilters = dialogFilters;
        }

        public override bool CanExecute(object parameter)
        {
            return parent != null && (dialogFilters != null || parameter is SaveFileData);
        }

        public override void Execute(object parameter)
        {
            string fileName = string.Empty;
            List<FileDialogFilter> filterToUse = dialogFilters;
            if (parameter is SaveFileData data)
            {
                filterToUse = data.Filters;
                fileName = data.FileName;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filters = filterToUse,
                InitialFileName = fileName
            };
            Task<string> task = saveFileDialog.ShowAsync(parent);
            task.ContinueWith((data) =>
            {
                if (data.Result == string.Empty)
                {
                    return;
                }

                CommandExecuted(new FileSelectedArg(data.Result));
            });
        }
    }
}
