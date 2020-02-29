using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.DataContainer.Logging;
using XmlFormatter.src.Enums;
using XmlFormatter.src.Interfaces.Logging;

namespace XmlFormatter.src.Logging
{
    class Logger : ILogger
    {
        private readonly ILoggingStrategy loggingStrategy;
        private readonly ILoggingFormatStrategy loggingFormatStrategy;
        private readonly List<LogScopesEnum> allowedScopes;

        private readonly bool completeLog;

        public Logger(ILoggingStrategy loggingStrategy, ILoggingFormatStrategy loggingFormatStrategy)
            : this(loggingStrategy, loggingFormatStrategy, false)
        {

        }

        public Logger(
            ILoggingStrategy loggingStrategy,
            ILoggingFormatStrategy loggingFormatStrategy,
            bool completeLog
            )
        {
            this.loggingStrategy = loggingStrategy;
            this.loggingFormatStrategy = loggingFormatStrategy;
            this.completeLog = completeLog;
            allowedScopes = new List<LogScopesEnum>();
            allowedScopes.Add(LogScopesEnum.None);
        }

        public void AddScope(LogScopesEnum logScopeEnum)
        {
            if (allowedScopes.Contains(logScopeEnum))
            {
                return;
            }
            allowedScopes.Add(logScopeEnum);
        }

        public bool LogMessage(LoggingMessage message)
        {
            if (completeLog || allowedScopes.Contains(message.Scope))
            {
                string stringMessage = loggingFormatStrategy.FormatMessage(message);
                return loggingStrategy.LogMessage(stringMessage);
            }

            return false;
        }
    }
}
