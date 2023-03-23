using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.Model.Messages;

namespace XmlFormatterOsIndependent.Services;
public class WindowApplicationService : IWindowApplicationService
{
    private readonly IDependecyInjectionResolverService injectionResolverService;

    public WindowApplicationService(IDependecyInjectionResolverService injectionResolverService)
    {
        this.injectionResolverService = injectionResolverService;
    }
    public Window? GetMainWindow()
    {
        return WeakReferenceMessenger.Default.Send(new RequestMainWindowMessage()).Response.Result;
    }

    public bool CloseActiveWindow()
    {
        var topMost = GetTopMostWindow();
        topMost?.Close();
        return topMost?.IsActive ?? false;
    }

    public void CloseApplication()
    {
        WeakReferenceMessenger.Default.Send(new CloseApplicationMessage());
    }

    public IEnumerable<Window> GetAllWindows()
    {
        IEnumerable<Window> windows = Enumerable.Empty<Window>();
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopApp)
        {
            windows = desktopApp.Windows;
        }

        return windows;
    }

    public Window? GetTopMostWindow()
    {
        return GetAllWindows().Where(window => window.IsActive && window.IsEnabled)
                              .SortByZIndex()
                              .Select(data => data as Window)
                              .OfType<Window>()
                              .FirstOrDefault();
    }

    public Task<string?> OpenFileAsync(List<FileDialogFilter> fileFilters)
    {
        return Task.Run(async () =>
        {
            var response = await OpenMultipleFilesAsync(fileFilters);
            return response?.FirstOrDefault();
        });
    }

    public async Task<IEnumerable<string>> OpenMultipleFilesAsync(List<FileDialogFilter> fileFilters)
    {
        OpenFileDialog openFile = new OpenFileDialog()
        {
            AllowMultiple = true,
            Filters = fileFilters
        };

        var mainWindow = await WeakReferenceMessenger.Default.Send(new RequestMainWindowMessage());
        if (mainWindow is null)
        {
            return Enumerable.Empty<string>();
        }
        string[] data = await openFile.ShowAsync(mainWindow) ?? Array.Empty<string>();
        return data;
    }

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

    public async Task<Unit> OpenNewWindow<T>() where T : Window
    {
        var window = injectionResolverService.GetService<T>() as Window;
        if (window is null)
        {
            return Unit.Default;
        }
        return await OpenNewWindow(window);

    }

    public async Task<string?> SaveFileAsync(List<FileDialogFilter> fileFilters)
    {
        SaveFileDialog saveFile = new SaveFileDialog()
        {
            Filters = fileFilters
        };

        var mainWindow = await WeakReferenceMessenger.Default.Send(new RequestMainWindowMessage());
        if (mainWindow is null)
        {
            return null;
        }
        return await saveFile.ShowAsync(mainWindow);
    }
}
