using System;
using XmlFormatterModel.Enums;

namespace XmlFormatterModel.Logging
{
    /// <summary>
    /// This class is a message container for logging
    /// </summary>
    public class LoggingMessage
    {
        /// <summary>
        /// The scope this message should be placed into
        /// </summary>
        private readonly LogScopesEnum scope;

        /// <summary>
        /// Scope of the message
        /// </summary>
        public LogScopesEnum Scope => scope;

        /// <summary>
        /// The sender of the log message
        /// </summary>
        private readonly string sender;

        /// <summary>
        /// Sender of the log message
        /// </summary>
        public string Sender => sender;

        /// <summary>
        /// The current time stamp the message was created
        /// </summary>
        private readonly DateTime timeStamp;

        /// <summary>
        /// Time stamp of the message
        /// </summary>
        public DateTime TimeStamp => timeStamp;

        /// <summary>
        /// The message body
        /// </summary>
        private readonly string message;

        /// <summary>
        /// The message body
        /// </summary>
        public string Message => message;

        /// <summary>
        /// Any exception which should be logged
        /// </summary>
        private readonly Exception exceptionThrown;

        /// <summary>
        /// Any exception which should be logged
        /// </summary>
        public Exception ExceptionThrown => exceptionThrown;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="sender">The sender of the message</param>
        /// <param name="message">The message itself</param>
        public LoggingMessage(object sender, string message)
            : this(LogScopesEnum.None, sender.GetType().FullName, message, null)
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="sender">The sender of the message</param>
        /// <param name="message">The message itself</param>
        public LoggingMessage(string sender, string message)
            : this(LogScopesEnum.None, sender, message, null)
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="scope">The scope of the message</param>
        /// <param name="sender">The sender of the message</param>
        /// <param name="message">The message itself</param>
        public LoggingMessage(LogScopesEnum scope, object sender, string message)
            : this(scope, sender.GetType().FullName, message, null)
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="scope">The scope of the message</param>
        /// <param name="sender">The sender of the message</param>
        /// <param name="message">The message itself</param>
        public LoggingMessage(LogScopesEnum scope, string sender, string message)
            : this(scope, sender, message, null)
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="scope">The scope of the message</param>
        /// <param name="sender">The sender of the message</param>
        /// <param name="message">The message itself</param>
        /// <param name="exception">A throwen exception</param>
        public LoggingMessage(LogScopesEnum scope, object sender, string message, Exception exception)
        : this(scope, sender.GetType().FullName, message, exception)
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="scope">The scope of the message</param>
        /// <param name="sender">The sender of the message</param>
        /// <param name="message">The message itself</param>
        /// <param name="exception">A throwen exception</param>
        public LoggingMessage(LogScopesEnum scope, string sender, string message, Exception exception)
        {
            this.scope = scope;
            this.sender = sender;
            this.message = message;
            this.exceptionThrown = exception;
            this.timeStamp = DateTime.Now;
        }


    }
}
