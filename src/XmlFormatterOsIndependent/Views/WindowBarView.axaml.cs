using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using CommunityToolkit.Mvvm.Messaging;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Views;
public partial class WindowBarView : UserControl
{
    //private int windowId;

    private bool mouseDownForWindow;
    private PointerPoint originalPoint;


    public WindowBarView()
    {
        InitializeComponent();
        mouseDownForWindow = false;
    }

    //Moving windows based on https://github.com/AvaloniaUI/Avalonia/discussions/8441

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        mouseDownForWindow = true;
        originalPoint = e.GetCurrentPoint(this);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        mouseDownForWindow = false;
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);

        if (!mouseDownForWindow)
        {
            return;
        }

        var currentPoint = e.GetCurrentPoint(this);
        var deltaPosition = new PixelPoint((int)(currentPoint.Position.X - originalPoint.Position.X), (int)(currentPoint.Position.Y - originalPoint.Position.Y));
        if (DataContext is WindowBarViewModel barViewModel)
        {
            WeakReferenceMessenger.Default.Send(new WindowDeltaDragEvent(deltaPosition, barViewModel.WindowId));
        }
    }
}
