using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PluginFramework.Utils
{
    /// <summary>
    /// Util class to open links based on the operation system
    /// </summary>
    public class UrlOpener
    {
        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="url"></param>
        public void OpenUrl(Uri url)
        {
            OpenUrl(url.ToString());
        }

        /// <summary>
        /// Open the given url
        /// </summary>
        public void OpenUrl(string url)
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
