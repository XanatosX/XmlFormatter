using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.Enums;

namespace XmlFormatter.src.DataContainer.Logging
{
    class LoggingMessage
    {
        private readonly LogScopesEnum scope;
        public LogScopesEnum Scope => scope;

        private readonly string caller;
        public string Caller => caller;

        private readonly DateTime timeStamp;
        public DateTime TimeStamp => timeStamp;

        private readonly string message;
        public string Message => message;

        private readonly Exception exceptionThrown;
        public Exception ExceptionThrown => exceptionThrown;

        public LoggingMessage(object caller, string message)
            : this(LogScopesEnum.None, caller.GetType().FullName, message, null)
        {

        }

        public LoggingMessage(string caller, string message)
            : this(LogScopesEnum.None, caller, message, null)
        {

        }

        public LoggingMessage(LogScopesEnum scope, object caller, string message)
            : this(scope, caller.GetType().FullName, message, null)
        {

        }

        public LoggingMessage(LogScopesEnum scope, string caller, string message)
            : this(scope, caller, message, null)
        {

        }

        public LoggingMessage(LogScopesEnum scope, object caller, string message, Exception exception)
        : this (scope, caller.GetType().FullName, message, exception)
        {

        }

        public LoggingMessage(LogScopesEnum scope, string caller, string message, Exception exception)
        {
            this.scope = scope;
            this.caller = caller;
            this.message = message;
            this.exceptionThrown = exception;
            this.timeStamp = DateTime.Now;
        }


    }
}
