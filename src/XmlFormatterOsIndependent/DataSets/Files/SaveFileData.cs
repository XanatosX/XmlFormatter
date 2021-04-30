using Avalonia.Controls;
using System.Collections.Generic;

namespace XmlFormatterOsIndependent.DataSets.Files
{
    /// <summary>
    /// Class to store the information to select a file to save to
    /// </summary>
    class SaveFileData
    {
        /// <summary>
        /// The file name used as a starting name for the save file selection dialog
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// The allowed file extensions for saving files
        /// </summary>
        public List<FileDialogFilter> Filters { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="fileName">The starting file name for the save dialog</param>
        /// <param name="filters">The filter used for allowed extensions</param>
        public SaveFileData(string fileName, List<FileDialogFilter> filters)
        {
            FileName = fileName;
            Filters = filters;
        }
    }
}
