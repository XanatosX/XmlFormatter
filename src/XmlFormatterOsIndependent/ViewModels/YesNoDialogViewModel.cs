using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.Model.Messages;

namespace XmlFormatterOsIndependent.ViewModels;


/// <summary>
/// Dialog content for a simple yes no quest
/// </summary>
public partial class YesNoDialogViewModel : ObservableObject, IDialogWindow
{
    /// <summary>
    /// The result of the dialog
    /// </summary>
    private DialogButtonResponses dialogButtonResponses;

    /// <inheritdoc/>
    public DialogButtonResponses DialogButtonResponses => dialogButtonResponses;

    /// <summary>
    /// The identifier of the dialog
    /// </summary>
    public string Identifier { get; }

    /// <summary>
    /// The text of the dialog as markdown
    /// </summary>
    [ObservableProperty]
    private string dialogText;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="markdownText">The markdown text to use</param>
    public YesNoDialogViewModel(string markdownText)
    {
        DialogText = markdownText;
        Identifier = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Command if the dialog was confirmed
    /// </summary>
    [RelayCommand]
    public void Yes()
    {
        dialogButtonResponses = DialogButtonResponses.Yes;
        Close();
    }

    /// <summary>
    /// Command if the dialog was not confirmed
    /// </summary>
    [RelayCommand]
    public void No()
    {
        dialogButtonResponses = DialogButtonResponses.No;
        Close();
    }

    /// <summary>
    /// Method to close the current dialog
    /// </summary>
    public void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseDialogMessage(Identifier));
    }
}
