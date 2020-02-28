using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.DataContainer.Logging;
using XmlFormatter.src.Enums;

namespace XmlFormatter.src.Interfaces.Logging
{
    interface ILogger
    {
        void AddScope(LogScopesEnum logScopeEnum);
        bool LogMessage(LoggingMessage message);
    }
}
