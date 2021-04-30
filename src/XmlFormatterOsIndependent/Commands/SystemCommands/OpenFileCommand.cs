using Avalonia.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.EventArg;

namespace XmlFormatterOsIndependent.Commands.SystemCommands
{
    /// <summary>
    /// Command to use to show a open file dialog
    /// </summary>
    class OpenFileCommand : BaseTriggerCommand
    {
        /// <summary>
        /// The paren to use for binding the open file window to
        /// </summary>
        protected readonly Window parent;

        /// <summary>
        /// The filter used for file selection
        /// </summary>
        protected readonly List<FileDialogFilter> dialogFilters;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parent">The parent window to use for binding the open file dialog to</param>
        public OpenFileCommand(Window parent)
            : this(parent, new List<FileDialogFilter>())
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parent">The parent window to use for binding the open file dialog to</param>
        /// <param name="dialogFilters">The filter to use for the open file dialog</param>
        public OpenFileCommand(Window parent, List<FileDialogFilter> dialogFilters)
        {
            this.parent = parent;
            this.dialogFilters = dialogFilters;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return parent != null && (dialogFilters != null || parameter is List<FileDialogFilter>);
        }

        /// <inheritdoc/>
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
