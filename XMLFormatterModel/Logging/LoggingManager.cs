using System;
using System.Collections.Generic;

namespace XmlFormatterModel.Logging
{
    /// <summary>
    /// This class is a simple logging manager
    /// </summary>
    public class LoggingManager : ILoggingManager, IDisposable
    {
        /// <summary>
        /// A list with all the loggers in the manager
        /// </summary>
        private readonly List<ILogger> loggers;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public LoggingManager()
        {
            loggers = new List<ILogger>();
        }

        /// <inheritdoc/>
        public bool AddLogger(ILogger logger)
        {
            loggers.Add(logger);
            return true;
        }

        /// <inheritdoc/>
        public bool RemoveLogger(ILogger logger)
        {
            return loggers.Remove(logger);
        }

        /// <inheritdoc/>
        public void RemoveLoggers()
        {
            foreach (ILogger logger in loggers)
            {
                logger.Dispose();
            }
            loggers.Clear();
        }

        /// <inheritdoc/>
        public bool LogMessage(LoggingMessage message)
        {
            bool status = true;
            foreach (ILogger logger in loggers)
            {
                status &= logger.LogMessage(message);
            }

            return status;
        }

        /// <summary>
        /// Dispose the manager
        /// </summary>
        public void Dispose()
        {
            RemoveLoggers();
        }
    }
}
