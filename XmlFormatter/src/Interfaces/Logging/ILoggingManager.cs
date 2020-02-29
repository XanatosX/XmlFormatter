using System;
using XmlFormatter.src.DataContainer.Logging;

namespace XmlFormatter.src.Interfaces.Logging
{
    /// <summary>
    /// This interface defines a logging manager
    /// </summary>
    interface ILoggingManager : IDisposable
    {
        /// <summary>
        /// Add a new logger to the manager
        /// </summary>
        /// <param name="logger">The logger to add</param>
        /// <returns>True if adding was successful</returns>
        bool AddLogger(ILogger logger);

        /// <summary>
        /// Remove a logger from the manager
        /// </summary>
        /// <param name="logger">The logger to remove</param>
        /// <returns>True if removing was successful</returns>
        bool RemoveLogger(ILogger logger);

        /// <summary>
        /// Remove all the loggers from the manager
        /// </summary>
        void RemoveLoggers();

        /// <summary>
        /// Log a message to all the loggers
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <returns>True if all the loggers did log successful</returns>
        bool LogMessage(LoggingMessage message);
    }
}
