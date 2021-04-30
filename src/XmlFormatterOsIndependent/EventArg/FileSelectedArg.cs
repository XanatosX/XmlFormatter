using System;

namespace XmlFormatterOsIndependent.EventArg
{
    /// <summary>
    /// Event args for file selection
    /// </summary>
    class FileSelectedArg : EventArgs
    {
        /// <summary>
        /// The selected file to load or save to
        /// </summary>
        public string SelectedFile { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="selectedFile">The selected file from the box</param>
        public FileSelectedArg(string selectedFile)
        {
            SelectedFile = selectedFile;
        }

        
    }
}
