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
    public int WindowId => WindowBar?.WindowId ?? -1;

    public DialogButtonResponses DialogButtonResponses => windowContent?.DialogButtonResponses ?? DialogButtonResponses.None;

    public string Identifier => windowContent?.Identifier ?? string.Empty;

    private bool isDisposed;


    [ObservableProperty]
    private IWindowBar? windowBar;

    [ObservableProperty]
    private IDialogWindow windowContent;

    [ObservableProperty]
    private Color themeColor;

    private string? contentIdentifier;

    public DialogWindowViewModel(
            IWindowApplicationService applicationService,
            IDialogWindow dialogWindow,
            IThemeService themeService,
            string? imagePath,
            string? dialogTitle
    )
    {
        isDisposed = false;
        windowBar = applicationService.GetDialogWindowBar(imagePath, dialogTitle);
        WindowContent = dialogWindow;
        contentIdentifier = dialogWindow.Identifier;

        var themeVariant = themeService.GetCurrentThemeVariant();
        ThemeColor = themeService.GetColorForTheme(themeVariant);

        if (contentIdentifier is null)
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

    private void CloseDialog()
    {
        WeakReferenceMessenger.Default.Send(new CloseWindowMessage(WindowId));
        Dispose();
    }

    public void Dispose()
    {
        if (isDisposed)
        {
            return;
        }
        isDisposed = true;
        WeakReferenceMessenger.Default.UnregisterAll(this);
    }

    ~DialogWindowViewModel()
    {
        Dispose();
    }
} 