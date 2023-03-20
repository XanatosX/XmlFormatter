using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlFormatterOsIndependent.Services;
public class DefaultInteractionService : IIOInteractionService
{
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
