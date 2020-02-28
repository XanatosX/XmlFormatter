using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.DataContainer.Logging;

namespace XmlFormatter.src.Interfaces.Logging
{
    interface ILoggingManager
    {
        bool AddLogger(ILogger logger);

        bool RemoveLogger(ILogger logger);

        void RemoveLoggers();

        bool LogMessage(LoggingMessage message);
    }
}
