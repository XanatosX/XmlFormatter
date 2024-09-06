using System;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.Services;

namespace XmlFormatterOsIndependent.ViewModels;

/// <summary>
/// Model view for the about window
/// </summary>
internal partial class DialogWindowViewModel : ObservableObject, IWindowWithId, IDialogWindow, IDisposable
{
    /// <inheritdoc/>
    public int WindowId => WindowBar?.WindowId ?? -1;

    /// <inheritdoc/>
    public DialogButtonResponses DialogButtonResponses => WindowContent?.DialogButtonResponses ?? DialogButtonResponses.None;


    /// <inheritdoc/>
    public string Identifier => WindowContent?.Identifier ?? string.Empty;

    /// <summary>
    /// Is this dialog window already disposed?
    /// </summary>
    private bool isDisposed;

    /// <summary>
    /// The window bar to use 
    /// </summary>
    [ObservableProperty]
    private IWindowBar? windowBar;

    /// <summary>
    /// The content of the dialog window
    /// </summary>
    [ObservableProperty]
    private IDialogWindow windowContent;

    /// <summary>
    /// The theme color to use based on the theme variant
    /// </summary>
    [ObservableProperty]
    private Color themeColor;

    
    /// <summary>
    /// Create a new instance of the dialog window class
    /// </summary>
    /// <param name="applicationService">The application service to use</param>
    /// <param name="dialogWindowContent">The current dialog window content</param>
    /// <param name="themeService">The theme service to use</param>
    /// <param name="imagePath">The image path for the dialog window</param>
    /// <param name="dialogTitle">The dialog title for the window</param>
    public DialogWindowViewModel(
            IWindowApplicationService applicationService,
            IDialogWindow dialogWindowContent,
            IThemeService themeService,
            string? imagePath,
            string? dialogTitle
    )
    {
        isDisposed = false;
        windowBar = applicationService.GetDialogWindowBar(imagePath, dialogTitle);
        WindowContent = dialogWindowContent;

        var themeVariant = themeService.GetCurrentThemeVariant();
        ThemeColor = themeService.GetColorForTheme(themeVariant);

        if (dialogWindowContent.Identifier is null)
        { 
            CloseDialog();
        }

        WeakReferenceMessenger.Default.Register<CloseDialogMessage>(this, (_, m) => {
            if (m.Identifier == Identifier)
            {
                 CloseDialog();
                 m.Reply(true);
            }
        });
    }

    /// <summary>
    /// Method to close the dialog window
    /// </summary>
    private void CloseDialog()
    {
        WeakReferenceMessenger.Default.Send(new CloseWindowMessage(WindowId));
        Dispose();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (isDisposed)
        {
            return;
        }
        isDisposed = true;
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    /// <summary>
    /// Deconstructor to ensure the object is disposed
    /// This is required to clear all the registered message subjects
    /// </summary>
    ~DialogWindowViewModel()
    {
        Dispose();
    }
} 