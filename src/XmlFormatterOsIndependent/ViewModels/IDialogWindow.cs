using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.ViewModels;

/// <summary>
/// Interface to describe a dialog window
/// </summary>
public interface IDialogWindow
{
    /// <summary>
    /// The button response of the dialog window
    /// </summary>
    DialogButtonResponses DialogButtonResponses { get; }
    
    /// <summary>
    /// The identifier of the dialog window
    /// </summary>
    string Identifier { get; }
}