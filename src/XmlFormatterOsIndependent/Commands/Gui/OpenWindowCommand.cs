using Avalonia.Controls;
using System;
using System.Runtime.CompilerServices;
using XmlFormatterOsIndependent.Views;

namespace XmlFormatterOsIndependent.Commands.Gui
{
    /// <summary>
    /// Command to open a new custom window
    /// </summary>
    class OpenWindowCommand : BaseTriggerCommand
    {
        /// <summary>
        /// The window type to get
        /// </summary>
        private readonly Type windowType;

        /// <summary>
        /// The window to open
        /// </summary>
        private Window windowToOpen;

        /// <summary>
        /// The parent window to bind the newly opened window to
        /// </summary>
        private readonly Window parentWindow;

        /// <summary>
        /// Was the window closed and we need to create a new instance the next time
        /// </summary>
        private bool windowClosed;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="windowType">The window type to create</param>
        /// <param name="parentWindow">The parent window to bind the new window to</param>
        public OpenWindowCommand(Type windowType, Window parentWindow)
        {
            if (windowType.IsSubclassOf(typeof(Window)))
            {
                this.windowType = windowType;
            }

            this.parentWindow = parentWindow;
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="windowToOpen">The window instance to open</param>
        /// <param name="parentWindow">The parent window to bind the new window to</param>
        public OpenWindowCommand(Window windowToOpen, Window parentWindow)
        {
            this.windowToOpen = windowToOpen;
            this.parentWindow = parentWindow;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return parentWindow != null && (windowType != null || windowToOpen != null);
        }

        /// <inheritdoc/>
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
                CommandExecuted(null);
            });

        }

        /// <summary>
        /// Get the window which should be shown
        /// </summary>
        /// <returns>The usable window instance</returns>
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
