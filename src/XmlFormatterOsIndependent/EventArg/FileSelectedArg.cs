using System;

namespace XmlFormatterOsIndependent.EventArg
{
    class FileSelectedArg : EventArgs
    {
        public string SelectedFile { get; }

        public FileSelectedArg(string selectedFile)
        {
            SelectedFile = selectedFile;
        }

        
    }
}
