using Avalonia.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.EventArg;

namespace XmlFormatterOsIndependent.Commands.Gui
{
    class OpenFileCommand : BaseTriggerCommand
    {
        protected readonly Window parent;
        protected readonly List<FileDialogFilter> dialogFilters;

        public OpenFileCommand(Window parent)
            : this(parent, new List<FileDialogFilter>())
        {

        }
        public OpenFileCommand(Window parent, List<FileDialogFilter> dialogFilters)
        {
            this.parent = parent;
            this.dialogFilters = dialogFilters;
        }


        public override bool CanExecute(object parameter)
        {
            return parent != null && (dialogFilters != null || parameter is List<FileDialogFilter>);
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            List<FileDialogFilter> filterToUse = dialogFilters;
            if (parameter is List<FileDialogFilter> filters)
            {
                filterToUse = filters;
            }

            OpenFileDialog openFile = new OpenFileDialog()
            {
                AllowMultiple = false,
                Filters = filterToUse
            };

            Task<string[]> task = openFile.ShowAsync(parent);
            task.ContinueWith((data) =>
            {
                if (data.Result.Length == 0)
                {
                    return;
                }

                CommandExecuted(new FileSelectedArg(data.Result[0]));
            });
        }
    }
}
