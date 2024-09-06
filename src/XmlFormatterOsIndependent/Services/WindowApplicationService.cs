using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using XmlFormatter.Application.Services;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.ViewModels;
using XmlFormatterOsIndependent.Views;

namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Default implementation of the <see cref="IWindowApplicationService"/>
/// </summary>
public class WindowApplicationService : IWindowApplicationService
{
    /// <summary>
    /// The current window to use for the next window with an id
    /// </summary>
    private int currentWindowId;

    /// <summary>
    /// The resolver service used to load the windows
    /// </summary>
    private readonly IDependencyInjectionResolverService injectionResolverService;

    /// <summary>
    /// The theme service to use
    /// </summary>
    private readonly IThemeService themeService;

    /// <summary>
    /// Create a new instance of the service
    /// </summary>
    /// <param name="injectionResolverService">The resolver service used to load the windows</param>
    public WindowApplicationService(IDependencyInjectionResolverService injectionResolverService,
                                    IThemeService themeService)
    {
        this.injectionResolverService = injectionResolverService;
        this.themeService = themeService;
        currentWindowId = 0;
    }

    /// <inheritdoc/>
    public Window? GetMainWindow()
    {
        return WeakReferenceMessenger.Default.Send(new RequestMainWindowMessage()).Response.Result;
    }

    /// <inheritdoc/>
    public bool CloseActiveWindow()
    {
        var topMost = GetTopMostWindow();
        topMost?.Close();
        return topMost?.IsActive ?? false;
    }

    /// <inheritdoc/>
    public void CloseApplication()
    {
        WeakReferenceMessenger.Default.Send(new CloseApplicationMessage());
    }

    /// <inheritdoc/>
    public IEnumerable<Window> GetAllWindows()
    {
        IEnumerable<Window> windows = Enumerable.Empty<Window>();
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopApp)
        {
            windows = desktopApp.Windows;
        }

        return windows;
    }

    /// <inheritdoc/>
    public Window? GetTopMostWindow()
    {
        return GetAllWindows().Where(window => window.IsActive && window.IsEnabled)
                              .SortByZIndex()
                              .Select(data => data as Window)
                              .OfType<Window>()
                              .FirstOrDefault();
    }

    /// <inheritdoc/>
    public Task<string?> OpenFileAsync(List<FilePickerFileType> fileFilters)
    {
        return Task.Run(async () =>
        {
            var response = await OpenMultipleFilesAsync(fileFilters);
            return response?.FirstOrDefault();
        });
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<string>> OpenMultipleFilesAsync(List<FilePickerFileType> fileFilters)
    {
        var mainWindow = await WeakReferenceMessenger.Default.Send(new RequestMainWindowMessage());
        if (mainWindow is null)
        {
            return Enumerable.Empty<string>();
        }
        var selectedFiles = await mainWindow.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions {
            FileTypeFilter = fileFilters,
        });

        return selectedFiles.Select(file => file.Path.AbsolutePath);
    }

    /// <inheritdoc/>
    public async Task<Unit> OpenNewWindow(Window window)
    {
        var mainWindow = await WeakReferenceMessenger.Default.Send(new RequestMainWindowMessage());
        if (mainWindow is null)
        {
            return Unit.Default;
        }
        await window.ShowDialog(mainWindow);
        return Unit.Default;
    }

    /// <inheritdoc/>
    public async Task<Unit> OpenNewWindow<T>() where T : Window
    {
        var window = injectionResolverService.GetService<T>() as Window;
        if (window is null)
        {
            return Unit.Default;
        }
        return await OpenNewWindow(window);

    }

    /// <inheritdoc/>
    public async Task<string?> SaveFileAsync(List<FilePickerFileType> fileFilters)
    {
        var mainWindow = await WeakReferenceMessenger.Default.Send(new RequestMainWindowMessage());
        if (mainWindow is null)
        {
            return null;
        }
        var pickedFile = await mainWindow.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions 
        {
            FileTypeChoices = fileFilters
        });

        return pickedFile?.Path.AbsolutePath ?? null;
    }

    /// <inheritdoc/>
    public IWindowBar GetWindowBar()
    {
        return GetWindowBar(Properties.Properties.Default_Window_Icon, Properties.Properties.Application_Name);
    }

    /// <inheritdoc/>
    public IWindowBar GetWindowBar(string windowIconPath, string windowTitle)
    {
        return new WindowBarViewModel(windowIconPath, windowTitle, currentWindowId++);
    }

    /// <inheritdoc/>
    public IWindowBar GetWindowBar(string windowIconPath, string windowTitle, bool allowMinimize)
    {
        return new WindowBarViewModel(windowIconPath, windowTitle, allowMinimize, currentWindowId++);
    }

    /// <inheritdoc/>
    public IWindowBar GetDialogWindowBar(string? windowIconPath, string? windowTitle)
    {
        return new DialogWindowBarViewModel(windowIconPath, windowTitle, currentWindowId++);
    }

    /// <inheritdoc/>
    public async Task<DialogButtonResponses> OpenDialogBoxAsync(string? windowIconPath, string? windowTitle, IDialogWindow content)
    {
        var mainWindow = GetMainWindow();
        if (mainWindow is null)
        {
            return DialogButtonResponses.None;
        }

        var dialogWindow = new DialogWindow();
        var dialogWindowViewModel = new DialogWindowViewModel(this, content, themeService, windowIconPath, windowTitle);
        dialogWindow.DataContext = dialogWindowViewModel;

        await dialogWindow.ShowDialog(mainWindow);

        return dialogWindowViewModel.DialogButtonResponses;
    }

}
