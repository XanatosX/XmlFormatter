using System.Text;
using XmlFormatter.src.DataContainer.Logging;
using XmlFormatter.src.Interfaces.Logging;

namespace XmlFormatter.src.Logging.FormatStrategies
{
    /// <summary>
    /// Simple class to format log messages for file logging
    /// </summary>
    class SimpleFileFormatStrategy : ILoggingFormatStrategy
    {
        /// <summary>
        /// The lenght of the sender filed
        /// </summary>
        private readonly int senderLength;

        /// <summary>
        /// Create a new instace of this class
        /// </summary>
        public SimpleFileFormatStrategy()
             : this(70)
        {
            
        }

        /// <summary>
        /// Create a new instace of this class
        /// </summary>
        /// <param name="senderLength">The length for the sender field</param>
        public SimpleFileFormatStrategy(int senderLength)
        {
            this.senderLength = senderLength;
        }

        /// <inheritdoc/>
        public string FormatMessage(LoggingMessage message)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(ExpandString(message.Sender, " ", senderLength));
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

        /// <summary>
        /// Extend a string so that it reaches a upper limit
        /// </summary>
        /// <param name="inputString">The string to extend</param>
        /// <param name="fillChar">The character used for filling</param>
        /// <param name="length">The target lenght of the string</param>
        /// <returns></returns>
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
