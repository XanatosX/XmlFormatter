using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using XmlFormatterOsIndependent.Enums;
using XmlFormatterOsIndependent.Model.Messages;

namespace XmlFormatterOsIndependent.ViewModels;

public partial class YesNoDialogViewModel : ObservableObject, IDialogWindow
{
    private DialogButtonResponses dialogButtonResponses;
    public DialogButtonResponses DialogButtonResponses => dialogButtonResponses;

    public string Identifier { get; }

    [ObservableProperty]
    private string dialogText;

    public YesNoDialogViewModel(string markdownText)
    {
        DialogText = markdownText;
        Identifier = Guid.NewGuid().ToString();
    }

    [RelayCommand]
    public void Yes()
    {
        dialogButtonResponses = DialogButtonResponses.Yes;
        Close();
    }

    [RelayCommand]
    public void No()
    {
        dialogButtonResponses = DialogButtonResponses.No;
        Close();
    }

    public void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseDialogMessage(Identifier));
    }
}
