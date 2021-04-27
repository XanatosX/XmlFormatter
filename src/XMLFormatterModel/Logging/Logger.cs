using System.Collections.Generic;
using XmlFormatterModel.Enums;

namespace XmlFormatterModel.Logging
{
    /// <summary>
    /// Strategy based logger instance
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// The strategy to use for logging
        /// </summary>
        private readonly ILoggingStrategy loggingStrategy;

        /// <summary>
        /// The strategy used for formating the message
        /// </summary>
        private readonly ILoggingFormatStrategy loggingFormatStrategy;

        /// <summary>
        /// A list with all the allowed scopes
        /// </summary>
        private readonly List<LogScopesEnum> allowedScopes;

        /// <summary>
        /// Is this a complete log which results in ignoring the scope list
        /// </summary>
        private readonly bool completeLog;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="loggingStrategy">The strategy used for logging</param>
        /// <param name="loggingFormatStrategy">The strategy used for formatting</param>
        public Logger(ILoggingStrategy loggingStrategy, ILoggingFormatStrategy loggingFormatStrategy)
            : this(loggingStrategy, loggingFormatStrategy, false)
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="loggingStrategy">The strategy used for logging</param>
        /// <param name="loggingFormatStrategy">The strategy used for formatting</param>
        /// <param name="completeLog">Is this a complete log</param>
        public Logger(
            ILoggingStrategy loggingStrategy,
            ILoggingFormatStrategy loggingFormatStrategy,
            bool completeLog
            )
        {
            this.loggingStrategy = loggingStrategy;
            this.loggingFormatStrategy = loggingFormatStrategy;
            this.completeLog = completeLog;
            allowedScopes = new List<LogScopesEnum>
            {
                LogScopesEnum.None
            };
        }

        /// <inheritdoc/>
        public void AddScope(LogScopesEnum logScopeEnum)
        {
            if (allowedScopes.Contains(logScopeEnum))
            {
                return;
            }
            allowedScopes.Add(logScopeEnum);
        }

        /// <inheritdoc/>
        public bool LogMessage(LoggingMessage message)
        {
            if (completeLog || allowedScopes.Contains(message.Scope))
            {
                string stringMessage = loggingFormatStrategy.FormatMessage(message);
                return loggingStrategy.LogMessage(stringMessage);
            }

            return false;
        }

        /// <summary>
        /// Dispose this logger
        /// </summary>
        public void Dispose()
        {
            loggingStrategy.Dispose();
        }
    }
}
