using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace XmlFormatterOsIndependent.Helper
{
    /// <summary>
    /// This class will help you to open up urls
    /// </summary>
    public class UrlOpener
    {
        /// <summary>
        /// The url to open
        /// </summary>
        private readonly string url;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="url">The url which should be opened</param>
        public UrlOpener(string url)
        {
            this.url = url;
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="url">The url which should be openend</param>
        public UrlOpener(Uri url) : this(url.ToString())
        {

        }

        /// <summary>
        /// Open the given url
        /// </summary>
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
