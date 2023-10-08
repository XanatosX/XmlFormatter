using System;

namespace PluginFramework.Utils
{
    /// <summary>
    /// Open a predefined url
    /// </summary>
    public class StaticUrlOpener
    {
        /// <summary>
        /// The url opener to use
        /// </summary>
        private UrlOpener urlOpener;

        /// <summary>
        /// The url to open
        /// </summary>
        private readonly string url;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="url">The url which should be opened</param>
        public StaticUrlOpener(string url)
        {
            this.url = url;
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="url">The url which should be opened</param>
        public StaticUrlOpener(Uri url) : this(url.ToString())
        {
        }

        /// <summary>
        /// Open the given url in the system browser
        /// </summary>
        public void OpenUrl()
        {
            urlOpener = urlOpener ?? new UrlOpener();
            urlOpener?.OpenUrl(url);
        }
    }
}
