using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterOsIndependent.MVVM.ViewModels.Behaviors
{
    interface IEventView
    {
        void RegisterEvents(Window currentWindow);
        void UnregisterEvents(Window currentWindow);
    }
}
