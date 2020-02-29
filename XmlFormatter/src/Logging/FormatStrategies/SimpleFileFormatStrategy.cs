using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.DataContainer.Logging;
using XmlFormatter.src.Interfaces.Logging;

namespace XmlFormatter.src.Logging.FormatStrategies
{
    class SimpleFileFormatStrategy : ILoggingFormatStrategy
    {
        private readonly int callerLength;

        public SimpleFileFormatStrategy()
             : this(70)
        {
            
        }

        public SimpleFileFormatStrategy(int callerLength)
        {
            this.callerLength = callerLength;
        }

        public string FormatMessage(LoggingMessage message)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(ExpandString(message.Sender, " ", callerLength));
            stringBuilder.Append(" -> ");
            stringBuilder.Append(message.TimeStamp);
            stringBuilder.Append(": ");
            stringBuilder.Append(message.Message);
            if (message.ExceptionThrown != null)
            {
                stringBuilder.Append("Exception: ");
                stringBuilder.Append(message.ExceptionThrown.Message);
            }

            return stringBuilder.ToString();
        }

        private string ExpandString(string inputString, string fillChar, int length)
        {
            if (length <= inputString.Length)
            {
                return inputString;
            }

            string appendString = fillChar;
            int lengthToAdd = length - inputString.Length;
            for (int i = 0; i < lengthToAdd; i++)
            {
                appendString += appendString;
                if (appendString.Length > lengthToAdd)
                {
                    break;
                }
            }

            if (appendString.Length > length)
            {
                appendString = appendString.Substring(0, lengthToAdd);
            }

            return inputString + appendString;
        }
    }
}
