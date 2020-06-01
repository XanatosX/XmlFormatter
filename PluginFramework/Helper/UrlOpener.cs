using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace XmlFormatterOsIndependent.Helper
{
    public class UrlOpener
    {
        private readonly string url;

        public UrlOpener(string url)
        {
            this.url = url;
        }

        public UrlOpener(Uri url) : this(url.ToString())
        {

        }

        public void OpenUrl()
        {
            string realUrl = url;
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    realUrl = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {realUrl}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", realUrl);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", realUrl);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
