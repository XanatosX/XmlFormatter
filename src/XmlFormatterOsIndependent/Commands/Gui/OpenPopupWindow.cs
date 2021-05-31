using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Collections.Generic;
using System.Text;
using XmlFormatterOsIndependent.MVVM.ViewModels.Popups;
using XmlFormatterOsIndependent.MVVM.Windows;

namespace XmlFormatterOsIndependent.Commands.Gui
{
    class OpenPopupWindow : BaseTriggerCommand
    {
        private readonly UserControl popupContent;

        public OpenPopupWindow(UserControl popupContent)
        {
            this.popupContent = popupContent;
        }
        public override bool CanExecute(object parameter)
        {
            return popupContent != null && popupContent.DataContext is IPopupContent;
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            IPopupContent currentContent = popupContent.DataContext as IPopupContent;
            currentContent.Clear();

            PopupWindow popupWindow = new PopupWindow();

            popupWindow.Closed += (sender, data) =>
            {
                IPopupContent content = popupContent.DataContext as IPopupContent;
                object returnData = content.GetData();
            };

            popupWindow.SetContent(popupContent);
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                popupWindow.ShowDialog(desktop.MainWindow);
            }
            
        }
    }
}
