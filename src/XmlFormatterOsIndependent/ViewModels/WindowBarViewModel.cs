using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using XmlFormatterOsIndependent.Services;

namespace XmlFormatterOsIndependent.ViewModels;
public partial class WindowBarViewModel : ObservableObject, IWindowWithId
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

    public void ChangeWindowHeadline(string headline)
    {
        WindowName = headline;
    }

    [RelayCommand]
    private void CloseWindow()
    {
        applicationService.CloseActiveWindow();
    }


    [RelayCommand(CanExecute = nameof(CanMinimizeWindow))]
    private void MinimizeWindow()
    {
        if (applicationService.GetTopMostWindow() is null)
        {
            return;
        }
        applicationService!.GetTopMostWindow()!.WindowState = Avalonia.Controls.WindowState.Minimized;
    }

    private bool CanMinimizeWindow()
    {
        return applicationService is not null;
    }

}
