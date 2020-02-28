using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.DataContainer.Logging;
using XmlFormatter.src.Interfaces.Logging;

namespace XmlFormatter.src.Logging
{
    class LoggingManager : ILoggingManager
    {
        private readonly List<ILogger> loggers;

        public LoggingManager()
        {
            loggers = new List<ILogger>();
        }

        public bool AddLogger(ILogger logger)
        {
            loggers.Add(logger);
            return true;
        }

        public bool RemoveLogger(ILogger logger)
        {
            return loggers.Remove(logger);
        }

        public void RemoveLoggers()
        {
            loggers.Clear();
        }

        public bool LogMessage(LoggingMessage message)
        {
            bool status = true;
            foreach (ILogger logger in loggers)
            {
                status &= logger.LogMessage(message);
            }

            return status;
        }
    }
}
