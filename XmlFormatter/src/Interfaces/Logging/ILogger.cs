using System;
using XmlFormatter.src.DataContainer.Logging;
using XmlFormatter.src.Enums;

namespace XmlFormatter.src.Interfaces.Logging
{
    /// <summary>
    /// A instance of a logger
    /// </summary>
    interface ILogger : IDisposable
    {
        /// <summary>
        /// Add a new scope the logger should log messages from
        /// </summary>
        /// <param name="logScopeEnum"></param>
        void AddScope(LogScopesEnum logScopeEnum);

        /// <summary>
        /// Log a new message to the logger
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <returns>True if logging was successful</returns>
        bool LogMessage(LoggingMessage message);
    }
}
