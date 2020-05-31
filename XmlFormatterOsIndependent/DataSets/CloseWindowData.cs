using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterOsIndependent.DataSets
{
    internal class CloseWindowData
    {
        public Window Window { get; }
        public bool AskBeforeClosing { get; }

        public CloseWindowData(Window view) : this(view, false)
        {

        }

        public CloseWindowData(Window view, bool askBeforeClose)
        {
            Window = view;
            AskBeforeClosing = askBeforeClose;
        }
    }
}
