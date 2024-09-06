using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using CommunityToolkit.Mvvm.Messaging;
using XmlFormatterOsIndependent.Model.Messages;

namespace XmlFormatterOsIndependent.Views;

public abstract partial class WindowBar : UserControl
{
    /// <summary>
    /// Is the mouse currently down for dragging?
    /// </summary>
    private bool mouseDownForWindow;

    /// <summary>
    /// The original point of the mouse before dragging begins
    /// </summary>
    private PointerPoint originalPoint;

    public WindowBar()
    {
        mouseDownForWindow = false;
    }
    
    //Moving windows based on https://github.com/AvaloniaUI/Avalonia/discussions/8441
    /// <summary>
    /// Event handler for when the mouse is pressed
    /// </summary>
    /// <param name="e">The mouse pointer event data</param>
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        mouseDownForWindow = true;
        originalPoint = e.GetCurrentPoint(this);
    }

    /// <summary>
    /// Event handler for when the mouse is released
    /// </summary>
    /// <param name="e">The mouse pointer event handler</param>
    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        mouseDownForWindow = false;
    }

    /// <summary>
    /// Event handler for when the mouse is moved
    /// </summary>
    /// <param name="e">The pointer data after moving</param>
    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);

        if (!mouseDownForWindow)
        {
            return;
        }

        var currentPoint = e.GetCurrentPoint(this);
        var deltaPosition = new PixelPoint((int)(currentPoint.Position.X - originalPoint.Position.X), (int)(currentPoint.Position.Y - originalPoint.Position.Y));
        if (DataContext is IWindowBar barViewModel)
        {
            WeakReferenceMessenger.Default.Send(new WindowDragEventMessage(deltaPosition, barViewModel.WindowId));
        }
    }
}