using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using XmlFormatter.Application;
using XmlFormatterOsIndependent.Model;
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

    private readonly ISettingsRepository<ApplicationSettings> settingsRepository;

    /// <summary>
    /// Service used to open save file or open file dialogs
    /// </summary>
    private readonly IWindowApplicationService windowService;

    public ApplicationSettingsBackupViewModel(
        ISettingsRepository<ApplicationSettings> settingsRepository,
        IWindowApplicationService windowService)
    {
        CurrentMessage = string.Empty;
        this.settingsRepository = settingsRepository;
        this.windowService = windowService;
    }

    /// <summary>
    /// Method to export the settings from the application to the disc
    /// </summary>
    /// <returns>Nothing</returns>
    [RelayCommand]
    public async Task ExportSettings()
    {
        var file = await windowService.SaveFileAsync(new List<FilePickerFileType>{
            new FilePickerFileType("Json") {
                Patterns = new List<string>{ "*.json" }
            },
        });
        var settings = settingsRepository.CreateOrLoad();
        if (string.IsNullOrEmpty(file) || settings is null)
        {
            CurrentMessage = Properties.Resources.BackupSettingsView_ExportFailed;
            return;
        }
        bool savingSuccessful = true;
        try
        {
            using (FileStream fileStream = new(file, FileMode.Create, FileAccess.Write))
            {
                JsonSerializer.Serialize(fileStream, settings);
            }
        }
        catch (System.Exception)
        {
            savingSuccessful = false;
        }

        if (savingSuccessful)
        {
            CurrentMessage = Properties.Resources.BackupSettingsView_ExportingSettingWasSuccessful;
        }
    }

    /// <summary>
    /// Method to import a given setting file into the application
    /// </summary>
    /// <returns>Nothing</returns>
    [RelayCommand]
    public async Task ImportSettings()
    {
        var file = await windowService.OpenFileAsync(new List<FilePickerFileType>{
            new("Json") { Patterns = new List<string>{ "json" } }
        });
        if (file is null)
        {
            //@Todo popup would be nice
            return;
        }
        ApplicationSettings? loadedSettings = null;
        try
        {
            using (FileStream fileStream = new(file, FileMode.Open, FileAccess.Read))
            {
                loadedSettings = JsonSerializer.Deserialize<ApplicationSettings>(fileStream);
            }
        }
        catch (System.Exception)
        {
        }

        
        if (loadedSettings is null)
        {
            CurrentMessage = Properties.Resources.BackupSettingsView_ImportedSettingNotLoaded;
            //@Todo popup would be nice
            return;
        }
        if (settingsRepository.Update(loadedSettings) is not null)
        {
            CurrentMessage = Properties.Resources.BackupSettingsView_ImportingSettingWasSuccessful;
            WeakReferenceMessenger.Default.Send(new SettingsImportedMessage(loadedSettings));
        }

    }
}
