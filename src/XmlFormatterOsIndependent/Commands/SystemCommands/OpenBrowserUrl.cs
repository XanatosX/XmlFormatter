using PluginFramework.Utils;
using System;

namespace XmlFormatterOsIndependent.Commands.SystemCommands
{
    /// <summary>
    /// This command will allow you to open up a url in the system browser
    /// </summary>
    class OpenBrowserUrl : BaseCommand
    {
        /// <summary>
        /// The url to open
        /// </summary>
        private readonly string url;

        /// <summary>
        /// The class used to open up the url
        /// </summary>
        private UrlOpener urlOpener;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public OpenBrowserUrl()
        {
            url = null;
        }


        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="url">The url to use for open up in the browser</param>
        public OpenBrowserUrl(Uri url)
            : this(url.ToString())
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="url">The url to use for open up in the browser</param>
        public OpenBrowserUrl(string url)
        {
            this.url = url;
        }
        
        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            return url != null || parameter is string;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            urlOpener = urlOpener ?? new UrlOpener();
            string urlToOpen = parameter is string ? parameter as string : url;
            urlOpener?.OpenUrl(urlToOpen);
        }
    }
}
