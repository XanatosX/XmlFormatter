using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatterOsIndependent.DataSets;
using XmlFormatterOsIndependent.Services;

namespace XmlFormatterOsIndependent.ViewModels;
internal partial class WindowBarViewModel : ObservableObject
{
    /// <summary>
    /// Service used for the everything related to windows
    /// </summary>
    private readonly IWindowApplicationService applicationService;

    [ObservableProperty]
    private string windowName;

    [ObservableProperty]
    private Bitmap? windowIcon;

    public WindowBarViewModel(IWindowApplicationService applicationService)
    {
        this.applicationService = applicationService;
        SetWindowIcon(Properties.Properties.Default_Window_Icon);
        WindowName = Properties.Properties.Application_Name;
    }

    public void SetWindowIcon(string path)
    {
        //TODO: Create service
        WindowIcon = new Bitmap(AssetLoader.Open(new Uri($"avares://{path}")));
    }

    public void ChangeWindowHeadline(string headline)
    {
        WindowName = headline;
    }

    [RelayCommand]
    private void CloseWindow()
    {
        applicationService.CloseActiveWindow();
    }


    [RelayCommand]
    private void MinimizeWindow()
    {
        applicationService.GetTopMostWindow()?.Hide();
    }

}
