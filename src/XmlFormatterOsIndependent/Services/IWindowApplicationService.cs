using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace XmlFormatterOsIndependent.Services;
public interface IWindowApplicationService
{
    void CloseApplication();
    bool CloseActiveWindow();

    IEnumerable<Window> GetAllWindows();

    Window? GetTopMostWindow();

    Task<Unit> OpenNewWindow(Window window);

    Task<Unit> OpenNewWindow<T>() where T : Window;

    Task<string?> OpenFileAsync(List<FileDialogFilter> fileFilters);

    Task<IEnumerable<string>> OpenMultipleFilesAsync(List<FileDialogFilter> fileFilters);

    Task<string?> SaveFileAsync(List<FileDialogFilter> fileFilters);
}
