using System;

namespace XmlFormatter.src.EventMessages
{
    class BaseEventArgs : EventArgs
    {
        /// <summary>
        /// The title of the event
        /// </summary>
        private readonly string title;

        /// <summary>
        /// Readonly access to the event title
        /// </summary>
        public string Title => title;

        /// <summary>
        /// The message of the event
        /// </summary>
        private readonly string message;

        /// <summary>
        /// Readonly access to the event message
        /// </summary>
        public string Message => message;

        /// <summary>
        /// This will create a basic event data container
        /// </summary>
        /// <param name="title">The title of the event</param>
        /// <param name="message">The message of the event</param>
        public BaseEventArgs(string title, string message)
        {
            this.title = title;
            this.message = message;
        }
        
    }
}
