using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.Model.Messages;

namespace XmlFormatterOsIndependent.Services;
public class WindowApplicationService : IWindowApplicationService
{
    public void CloseActiveWindow()
    {
        throw new NotImplementedException();
    }

    public void CloseAplication()
    {
        WeakReferenceMessenger.Default.Send(new CloseApplicationMessage());
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
}
