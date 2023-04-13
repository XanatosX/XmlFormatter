using System;
using System.Diagnostics;

namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Default implementation of the <see cref="IIOInteractionService"/>
/// </summary>
public class DefaultInteractionService : IIOInteractionService
{
    /// <inheritdoc/>
    public bool OpenWebsite(string url)
    {
        try
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = url
            };
            _ = Process.Start(processStartInfo);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
}
