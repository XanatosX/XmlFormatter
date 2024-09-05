using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.Services;

namespace XmlFormatterOsIndependent.ViewModels;

/// <summary>
/// A view model for the custom window bar
/// </summary>
public partial class WindowBarViewModel : ObservableObject, IWindowBar
{
    /// <inheritdoc/>
    public int WindowId {get; }

    /// <summary>
    /// Service used for the everything related to windows
    /// </summary>
    private readonly IWindowApplicationService applicationService;

    /// <summary>
    /// The display name of the window
    /// </summary>
    [ObservableProperty]
    private string windowName;

    /// <summary>
    /// The icon of the window
    /// </summary>
    [ObservableProperty]
    private Bitmap? windowIcon;

    private bool allowMinimize;

    /// <summary>
    /// Create a new instance of this custom window
    /// </summary>
    /// <param name="applicationService">The application service to use</param>
    /// <param name="windowIconPath">The window icon path to use</param>
    /// <param name="windowTitle">The title of the window</param>
    /// <param name="windowId">The id of this window</param>
    public WindowBarViewModel(IWindowApplicationService applicationService, string windowIconPath, string windowTitle, int windowId) : this(applicationService, windowIconPath, windowTitle, true, windowId)
    {
    }

    
    /// <summary>
    /// Create a new instance of this custom window
    /// </summary>
    /// <param name="applicationService">The application service to use</param>
    /// <param name="windowIconPath">The window icon path to use</param>
    /// <param name="windowTitle">The title of the window</param>
    /// <param name="windowId">The id of this window</param>
    public WindowBarViewModel(IWindowApplicationService applicationService, string windowIconPath, string windowTitle, bool allowMinimize, int windowId)
    {
        this.applicationService = applicationService;
        SetWindowIcon(windowIconPath);
        WindowName = windowTitle;
        this.WindowId = windowId;
        this.allowMinimize = allowMinimize;
    }

    /// <summary>
    /// Set the icon of the window based on a given resource path
    /// </summary>
    /// <param name="path">The resource path to use</param>
    private void SetWindowIcon(string path)
    {
        WindowIcon = new Bitmap(AssetLoader.Open(new Uri($"avares://{path}")));
    }

    /// <summary>
    /// Send a close command to the host window
    /// </summary>
    [RelayCommand]
    private void CloseWindow()
    {
        WeakReferenceMessenger.Default.Send(new CloseWindowMessage(WindowId));
    }


    /// <summary>
    /// Send a command to minimize the host window
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanMinimizeWindow))]
    private void MinimizeWindow()
    {
        WeakReferenceMessenger.Default.Send(new ChangeWindowState(WindowId, Avalonia.Controls.WindowState.Minimized));
    }

    /// <summary>
    /// Is it possible to minimize this window?
    /// </summary>
    /// <returns>True if minimize is allowed</returns>
    private bool CanMinimizeWindow()
    {
        return applicationService is not null && allowMinimize;
    }

}
