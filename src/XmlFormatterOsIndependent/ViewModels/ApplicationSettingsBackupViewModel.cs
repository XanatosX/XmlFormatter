using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Generic;
using XmlFormatterOsIndependent.Model.Messages;
using XmlFormatterOsIndependent.Services;

namespace XmlFormatterOsIndependent.ViewModels;

internal partial class ApplicationSettingsBackupViewModel : ObservableObject
{
    /// <summary>
    /// The current message for the backup view
    /// </summary>
    [ObservableProperty]
    private string currentMessage;

    /// <summary>
    /// Service used to load and save settings
    /// </summary>
    private readonly SettingFacadeService settingFacade;

    /// <summary>
    /// Service used to open save file or open file dialogs
    /// </summary>
    private readonly IWindowApplicationService windowService;

    public ApplicationSettingsBackupViewModel(
        SettingFacadeService settingFacadeService,
        IWindowApplicationService windowService)
    {
        CurrentMessage = string.Empty;
        settingFacade = settingFacadeService;
        this.windowService = windowService;
    }

    /// <summary>
    /// Method to export the settings from the application to the disc
    /// </summary>
    /// <returns>Nothing</returns>
    [RelayCommand]
    public async void ExportSettings()
    {
        var file = await windowService.SaveFileAsync(new List<FileDialogFilter>{
            new() { Name = "Xml", Extensions = new List<string>{ "xml" } }
        });
        var settings = settingFacade.GetSettings();
        if (string.IsNullOrEmpty(file) || settings is null)
        {
            CurrentMessage = Properties.Resources.BackupSettingsView_ExportFailed;
            return;
        }

        if (settingFacade.SaveSettings(settings, file) is not null)
        {
            CurrentMessage = Properties.Resources.BackupSettingsView_ExportingSettingWasSuccessful;
        }
    }

    /// <summary>
    /// Method to import a given setting file into the application
    /// </summary>
    /// <returns>Nothing</returns>
    [RelayCommand]
    public async void ImportSettings()
    {
        var file = await windowService.OpenFileAsync(new List<FileDialogFilter>{
            new() { Name = "Xml", Extensions = new List<string>{ "xml" } }
        });
        if (file is null)
        {
            //@Todo popup would be nice
            return;
        }
        var loadedSettings = settingFacade.GetSettings(file);
        if (loadedSettings is null)
        {
            CurrentMessage = Properties.Resources.BackupSettingsView_ImportedSettingNotLoaded;
            //@Todo popup would be nice
            return;
        }
        if (settingFacade.SaveSettings(loadedSettings) is not null)
        {
            CurrentMessage = Properties.Resources.BackupSettingsView_ImportingSettingWasSuccessful;
            WeakReferenceMessenger.Default.Send(new SettingsImportedMessage(loadedSettings));
        }

    }
}
