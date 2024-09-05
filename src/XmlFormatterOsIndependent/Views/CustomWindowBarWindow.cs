using System;
using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.ViewModels;

/// <summary>
/// Abstract class for a window with a custom window bar
/// </summary>
public abstract partial class CustomWindowBarWindow : Window, IDisposable
{
    /// <summary>
    /// Was this already disposed
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    public CustomWindowBarWindow()
    {
        isDisposed = false;
        WeakReferenceMessenger.Default.Register<WindowDragEventMessage>(this, (_, e) =>
        {
            if (e.WindowId == GetWindowId())
            {
                MoveWindowPosition(e);
            }
        });

        WeakReferenceMessenger.Default.Register<CloseWindowMessage>(this, (_, e) =>
        {
            if (e.WindowId == GetWindowId())
            {
                CloseWindow();
            }
        });

        WeakReferenceMessenger.Default.Register<ChangeWindowState>(this, (_, e) =>
        {
            if (e.WindowId == GetWindowId())
            {
                ChangeState(e.NewState);
            }
        });
    }

    protected virtual void CloseWindow()
    {
        Dispose();
        Close();
    }

    protected virtual void ChangeState(WindowState newState)
    {
        WindowState = newState;
    }

    protected virtual void MoveWindowPosition(WindowDragEventMessage deltaPosition)
    {
        Position = new PixelPoint(Position.X + deltaPosition.Value.X, Position.Y + deltaPosition.Value.Y);
    }

    /// <summary>
    /// Get the id of the window
    /// </summary>
    /// <returns>The window id or -1 if noting was found</returns>
    protected virtual int? GetWindowId()
    {
        return DataContext is IWindowWithId windowWithId ? windowWithId.WindowId : -1;
    }

    
    /// <inheritdoc/>
    public virtual void Dispose()
    {
        if (isDisposed)
        {
            return;
        }
        isDisposed = true;
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    /// <summary>
    /// Deconstructor for this class
    /// </summary>
    ~CustomWindowBarWindow()
    {
        Dispose();
    }

}
