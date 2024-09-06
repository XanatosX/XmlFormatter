using CommunityToolkit.Mvvm.Messaging.Messages;

namespace XmlFormatterOsIndependent.Model.Messages;

/// <summary>
/// Message to close a given dialog based on there identifier
/// </summary>
public class CloseDialogMessage : RequestMessage<bool>
{
    /// <summary>
    /// The identifier of the dialog to close
    /// </summary>
    public string Identifier { get; }

    /// <summary>
    /// Create a new instance of this dialog
    /// </summary>
    /// <param name="identifier">The identifier of the dialog to close</param>
    public CloseDialogMessage(string identifier)
    {
        Identifier = identifier;
    }

}