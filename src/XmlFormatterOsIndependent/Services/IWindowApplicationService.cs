using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlFormatterOsIndependent.Services;
public interface IWindowApplicationService
{
    void CloseAplication();
    void CloseActiveWindow();

    Task<string?> OpenFileAsync(List<FileDialogFilter> fileFilters);

    Task<IEnumerable<string>> OpenMultipleFilesAsync(List<FileDialogFilter> fileFilters);
}
