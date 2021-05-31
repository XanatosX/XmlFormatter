using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterOsIndependent.MVVM.ViewModels.Popups
{
    interface IPopupContent
    {
        object GetData();

        void Clear();

        void SetHostWindow(Window hostWindow);
    }
}
