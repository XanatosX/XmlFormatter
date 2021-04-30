using Avalonia.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.DataSets.Files;
using XmlFormatterOsIndependent.EventArg;

namespace XmlFormatterOsIndependent.Commands.SystemCommands
{
    /// <summary>
    /// Command to use for showing a save file dialog
    /// </summary>
    class SaveFileCommand : BaseTriggerCommand
    {

        /// <summary>
        /// The parent window to bind the save file dialog to
        /// </summary>
        protected readonly Window parent;

        /// <summary>
        /// The filters which allows you to select the file extension for saving
        /// </summary>
        protected readonly List<FileDialogFilter> dialogFilters;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parent">The class to bind the save file dialog to</param>
        public SaveFileCommand(Window parent)
            : this(parent, new List<FileDialogFilter>())
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="parent">The class to bind the save file dialog to</param>
        /// <param name="dialogFilters">The file filter to use for save file selection</param>
        public SaveFileCommand(Window parent, List<FileDialogFilter> dialogFilters)
        {
            this.parent = parent;
            this.dialogFilters = dialogFilters;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return parent != null && (dialogFilters != null || parameter is SaveFileData);
        }

        /// <inheritdoc/>
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
