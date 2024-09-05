using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.Services;

namespace XmlFormatterOsIndependent.ViewModels;
public partial class WindowBarViewModel : ObservableObject, IWindowBar
{
    public int WindowId {get; }

    /// <summary>
    /// Service used for the everything related to windows
    /// </summary>
    private readonly IWindowApplicationService applicationService;

    [ObservableProperty]
    private string windowName;

    [ObservableProperty]
    private Bitmap? windowIcon;

    public WindowBarViewModel(IWindowApplicationService applicationService, string windowIconPath, string windowName, int windowId)
    {
        this.applicationService = applicationService;
        SetWindowIcon(windowIconPath);
        WindowName = windowName;
        this.WindowId = windowId;
    }

    private void SetWindowIcon(string path)
    {
        WindowIcon = new Bitmap(AssetLoader.Open(new Uri($"avares://{path}")));
    }

    [RelayCommand]
    private void CloseWindow()
    {
        WeakReferenceMessenger.Default.Send(new CloseWindowMessage(WindowId));
    }


    [RelayCommand(CanExecute = nameof(CanMinimizeWindow))]
    private void MinimizeWindow()
    {
        WeakReferenceMessenger.Default.Send(new ChangeWindowState(WindowId, Avalonia.Controls.WindowState.Minimized));
    }

    private bool CanMinimizeWindow()
    {
        return applicationService is not null;
    }

}
