using XmlFormatterOsIndependent.Enums;

namespace XmlFormatterOsIndependent.ViewModels;

public interface IDialogWindow
{
    DialogButtonResponses DialogButtonResponses { get; }
    
    string Identifier { get; }
}