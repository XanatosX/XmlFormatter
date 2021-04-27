using Avalonia.Controls;
using System;

namespace XmlFormatterOsIndependent.DataSets
{
    /// <summary>
    /// Data container with all the view information
    /// </summary>
    public class ViewContainer
    {
        /// <summary>
        /// The current window
        /// </summary>
        public Window Current { get; }

        /// <summary>
        /// The parent window
        /// </summary>
        public Window Parent { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="current">The current window</param>
        /// <param name="parent">The parent window</param>
        public ViewContainer(Window current, Window parent)
        {
            Current = current;
            Parent = parent;
        }

        /// <summary>
        /// Get the current window
        /// </summary>
        /// <returns>The current window</returns>
        [Obsolete]
        public Window GetWindow()
        {
            return Current;
        }

        /// <summary>
        /// Get the parent window
        /// </summary>
        /// <returns>The parent window</returns>
        [Obsolete]
        public Window GetParent()
        {
            return Parent;
        }
    }
}
