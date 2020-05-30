using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterOsIndependent.DataSets
{
    public class FileDialogData
    {
        public Window View { get; }
        public List<FileDialogFilter> Filters { get; }

        public FileDialogData(Window view, List<FileDialogFilter> filter)
        {
            View = view;
            Filters = filter;
        }
    }
}
