using Avalonia.Controls;

namespace XmlFormatterOsIndependent.DataSets
{
    /// <summary>
    /// Data class to close a window
    /// </summary>
    internal class CloseWindowData
    {
        /// <summary>
        /// The window to close
        /// </summary>
        public Window Window { get; }

        /// <summary>
        /// Should we ask before closing
        /// </summary>
        public bool AskBeforeClosing { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="view">The window to close</param>
        public CloseWindowData(Window view) : this(view, false)
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="view">The window to close</param>
        /// <param name="askBeforeClose">As before closind</param>
        public CloseWindowData(Window view, bool askBeforeClose)
        {
            Window = view;
            AskBeforeClosing = askBeforeClose;
        }
    }
}
