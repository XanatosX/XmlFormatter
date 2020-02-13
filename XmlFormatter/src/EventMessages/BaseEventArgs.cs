using System;

namespace XmlFormatter.src.EventMessages
{
    class BaseEventArgs : EventArgs
    {
        private readonly string title;
        public string Title => title;

        private readonly string message;
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
