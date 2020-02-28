using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.DataContainer.Logging;

namespace XmlFormatter.src.Interfaces.Logging
{
    interface ILoggingStrategy
    {
        bool LogMessage(string message);
    }
}
