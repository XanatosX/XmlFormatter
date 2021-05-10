using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
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
        /// The filter used for file selection
        /// </summary>
        protected readonly List<FileDialogFilter> dialogFilters;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parent">The parent window to use for binding the open file dialog to</param>
        public OpenFileCommand()
            : this(new List<FileDialogFilter>())
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parent">The parent window to use for binding the open file dialog to</param>
        /// <param name="dialogFilters">The filter to use for the open file dialog</param>
        public OpenFileCommand(List<FileDialogFilter> dialogFilters)
        {
            this.dialogFilters = dialogFilters;
        }

        /// <summary>
        /// Get the current main window
        /// </summary>
        /// <returns>Current main window</returns>
        protected Window GetMainWindow()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                return desktop.MainWindow;
            }
            return null;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return  (dialogFilters != null || parameter is List<FileDialogFilter>);
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter) || GetMainWindow() == null)
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

            Task<string[]> task = openFile.ShowAsync(GetMainWindow());
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
