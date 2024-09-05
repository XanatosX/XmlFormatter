﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using XmlFormatter.Application.Services;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Default implementation of the <see cref="IWindowApplicationService"/>
/// </summary>
public class WindowApplicationService : IWindowApplicationService
{
    private int currentWindowId;

    /// <summary>
    /// The resolver service used to load the windows
    /// </summary>
    private readonly IDependencyInjectionResolverService injectionResolverService;

    /// <summary>
    /// Create a new instance of the service
    /// </summary>
    /// <param name="injectionResolverService">The resolver service used to load the windows</param>
    public WindowApplicationService(IDependencyInjectionResolverService injectionResolverService)
    {
        this.injectionResolverService = injectionResolverService;
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

    public IWindowBar GetWindowBar()
    {
        return GetWindowBar(Properties.Properties.Default_Window_Icon, Properties.Properties.Application_Name);
    }

    
    public IWindowBar GetWindowBar(string windowIconPath, string windowName)
    {
        return new WindowBarViewModel(this, windowIconPath, windowName, currentWindowId++);
    }
}
