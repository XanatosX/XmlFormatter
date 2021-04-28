using Avalonia.Controls;
using System;
using System.Runtime.CompilerServices;
using XmlFormatterOsIndependent.Views;

namespace XmlFormatterOsIndependent.Commands.Gui
{
    class OpenWindowCommand : BaseCommand
    {
        private readonly Type windowType;
        private Window windowToOpen;
        private readonly Window parentWindow;

        private bool windowClosed;

        public OpenWindowCommand(Type windowType, Window parentWindow)
        {
            if (windowType.IsSubclassOf(typeof(Window)))
            {
                this.windowType = windowType;
            }

            this.parentWindow = parentWindow;
        }

        public OpenWindowCommand(Window windowToOpen, Window parentWindow)
        {
            this.windowToOpen = windowToOpen;
            this.parentWindow = parentWindow;
        }

        public override bool CanExecute(object parameter)
        {
            return parentWindow != null && (windowType != null || windowToOpen != null);
        }

        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }
            
            try
            {
                windowToOpen = GetWindow();    
            }
            catch (Exception)
            {
            }

            if (windowToOpen == null)
            {
                return;
            }
            if (windowToOpen is IParentSetable parentSetable)
            {
                parentSetable.SetParent(parentWindow);
            }
            TaskAwaiter awaiter = windowToOpen.ShowDialog(parentWindow).GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                windowClosed = true;
            });

        }

        private Window GetWindow()
        {
            if (windowClosed)
            {
                windowClosed = false;
                Type typeToOpen = windowType;
                if (typeToOpen == null)
                {
                    typeToOpen = windowToOpen?.GetType();
                }
                return typeToOpen != null ? (Window)Activator.CreateInstance(typeToOpen) : null;
            }
            return windowToOpen ?? (Window)Activator.CreateInstance(windowType);
        }
    }
}
