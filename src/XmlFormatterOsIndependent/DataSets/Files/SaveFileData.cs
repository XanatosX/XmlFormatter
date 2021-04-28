using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterOsIndependent.DataSets.Files
{
    class SaveFileData
    {
        public string FileName { get; }
        public List<FileDialogFilter> Filters { get; }

        public SaveFileData(string rootFolder, List<FileDialogFilter> filters)
        {
            FileName = rootFolder;
            Filters = filters;
        }
    }
}
