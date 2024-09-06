using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using XmlFormatterOsIndependent.Model.Messages;

namespace XmlFormatterOsIndependent.ViewModels;

/// <summary>
/// Window bar for a dialog window
/// </summary>
internal partial class DialogWindowBarViewModel : ObservableObject, IWindowBar
{
    /// <inheritdoc/>
    public int WindowId {get; }

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

    public DialogWindowBarViewModel(string? iconPath, string? windowTitle, int windowId)
    {
        this.WindowId = windowId;
        this.WindowName = windowTitle ?? string.Empty;
        if (iconPath is not null)
        {
            SetWindowIcon(iconPath);
        }
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
}