using System;

namespace PluginFramework.src.EventMessages
{
    /// <summary>
    /// This class is a simple event data container
    /// </summary>
    public class BaseEventArgs : EventArgs
    {
        /// <summary>
        /// Readonly access to the event title
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Readonly access to the event message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// This will create a basic event data container
        /// </summary>
        /// <param name="title">The title of the event</param>
        /// <param name="message">The message of the event</param>
        public BaseEventArgs(string title, string message)
        {
            this.Title = title;
            this.Message = message;
        }
    }
}
