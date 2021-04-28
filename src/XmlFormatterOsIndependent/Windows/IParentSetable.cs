using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterOsIndependent.Views
{
    /// <summary>
    /// Interface which allows you to set the parent
    /// </summary>
    public interface IParentSetable
    {
        /// <summary>
        /// Set the parent for this window
        /// </summary>
        /// <param name="parent">The parent to use</param>
        void SetParent(Window parent);
    }
}
