using Avalonia.Controls;
using System.Collections.Generic;

namespace XmlFormatterOsIndependent.DataSets
{
    public class FileDialogData
    {
        public Window View { get; }
        public List<FileDialogFilter> Filters { get; }
        public string DefaultFileName { get; }

        public FileDialogData(Window view, List<FileDialogFilter> filter) : this(view, filter, string.Empty)
        {
        }

        public FileDialogData(Window view, List<FileDialogFilter> filter, string defaultFile)
        {
            View = view;
            Filters = filter;
            DefaultFileName = defaultFile;
        }
    }
}
