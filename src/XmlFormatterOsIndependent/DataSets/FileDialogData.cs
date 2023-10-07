using Avalonia.Controls;
using System.Collections.Generic;

namespace XmlFormatterOsIndependent.DataSets
{
    /// <summary>
    /// Data class for file dialogs
    /// </summary>
    public class FileDialogData
    {
        /// <summary>
        /// The window this dialog is parent of
        /// </summary>
        public Window View { get; }

        /// <summary>
        /// The search filter to use
        /// </summary>
        public List<FileDialogFilter> Filters { get; }

        /// <summary>
        /// The default name of the file to save to
        /// </summary>
        public string DefaultFileName { get; }

        /// <summary>
        /// Create a new instance this this class
        /// </summary>
        /// <param name="view">The parent window of the new dialog</param>
        /// <param name="filter">The filter to use for the dialog</param>
        public FileDialogData(Window view, List<FileDialogFilter> filter) : this(view, filter, string.Empty)
        {
        }

        /// <summary>
        /// Create a new instance this this class
        /// </summary>
        /// <param name="view">The parent window of the new dialog</param>
        /// <param name="filter">The filter to use for the dialog</param>
        /// <param name="defaultFile">The default name of the file to save to</param>
        public FileDialogData(Window view, List<FileDialogFilter> filter, string defaultFile)
        {
            View = view;
            Filters = filter;
            DefaultFileName = defaultFile;
        }
    }
}
