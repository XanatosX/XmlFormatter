using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.ViewModels;

namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Service to handle the application window's
/// </summary>
public interface IWindowApplicationService
{
    /// <summary>
    /// Get the main window of the application
    /// </summary>
    /// <returns>The applications main window</returns>
    Window? GetMainWindow();

    /// <summary>
    /// Close the application
    /// </summary>
    void CloseApplication();

    /// <summary>
    /// Close the currently active window
    /// </summary>
    /// <returns>True if closing was succesful</returns>
    bool CloseActiveWindow();

    /// <summary>
    /// Get all windows of the application
    /// </summary>
    /// <returns>A enumerable with all the windows which are currenty open</returns>
    IEnumerable<Window> GetAllWindows();

    /// <summary>
    /// Get the top most window of the application
    /// </summary>
    /// <returns>The topmost window or null if nothing was found</returns>
    Window? GetTopMostWindow();

    /// <summary>
    /// Open a new window
    /// </summary>
    /// <param name="window">The new window to open</param>
    /// <returns>A awaitable task if the window was opened</returns>
    Task<Unit> OpenNewWindow(Window window);

    /// <summary>
    /// Open a new window of a given type
    /// </summary>
    /// <typeparam name="T">The type of the window to open</typeparam>
    /// <returns>A awaitable task if the window was opened</returns>
    Task<Unit> OpenNewWindow<T>() where T : Window;

    /// <summary>
    /// Open a dialog to select a file for opening
    /// </summary>
    /// <param name="fileFilters">The filters used to define which files can be opened</param>
    /// <returns>A awaitable task containing a string with the selected file</returns>
    Task<string?> OpenFileAsync(List<FilePickerFileType> fileFilters);

    /// <summary>
    /// Open a dialog to select multiple files for opening
    /// </summary>
    /// <param name="fileFilters">The filters used to define which files can be opened</param>
    /// <returns>A enumerable with all the files which where selected for opening</returns>
    Task<IEnumerable<string>> OpenMultipleFilesAsync(List<FilePickerFileType> fileFilters);

    /// <summary>
    /// Method to open a dialog to select a file for saving
    /// </summary>
    /// <param name="fileFilters">The filters used to define which type of files will be saved</param>
    /// <returns>A single string for the path to the file where the data should be saved in</returns>
    Task<string?> SaveFileAsync(List<FilePickerFileType> fileFilters);
    
    /// <summary>
    /// Method to get the window bar of the application
    /// </summary>
    /// <returns>Returns the window bar of the application</returns>
    IWindowBar GetWindowBar();

    /// <summary>
    /// Get the window bar with some custom attributes
    /// </summary>
    /// <param name="windowIconPath">The path to a png resource icon to use as a window icon</param>
    /// <param name="windowName">The name of the window</param>
    /// <returns>A custom window bar</returns>
    IWindowBar GetWindowBar(string windowIconPath, string windowName);

    /// <summary>
    /// Get the window bar with some custom attributes
    /// </summary>
    /// <param name="windowIconPath">The path to a png resource icon to use as a window icon</param>
    /// <param name="windowName">The name of the window</param>
    /// <param name="allowMinimize">Is it allowed to minimize this window?</param>
    /// <returns>A custom window bar</returns>
    IWindowBar GetWindowBar(string windowIconPath, string windowName, bool allowMinimize);
}
