using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using XmlFormatterOsIndependent.MVVM.ViewModels.Popups;

namespace XmlFormatterOsIndependent.MVVM.ViewModels.Main
{
    public class PopupWindowViewModel : ReactiveObject
    {
        public object WindowContent
        {
            get => windowContent;
            private set => this.RaiseAndSetIfChanged(ref windowContent, value);
        }

        private object windowContent;

        public void SetContent(object content)
        {
            WindowContent = content;
        }

        public void SetWindow(Window popupHost)
        {
            if (WindowContent is UserControl control && control.DataContext is IPopupContent popupContent)
            {
                popupContent.SetHostWindow(popupHost);
            }
        }
    }
}
