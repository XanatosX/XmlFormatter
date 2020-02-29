using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlFormatter.src.DataContainer.Logging;
using XmlFormatter.src.Interfaces.Logging;

namespace XmlFormatter.src.Logging.Strategies
{
    class SimpleFileLogStrategy : ILoggingStrategy
    {
        private readonly StreamWriter streamWriter;

        public SimpleFileLogStrategy(string fileToWrite)
            : this(fileToWrite, true)
        {

        }

        public SimpleFileLogStrategy(string fileToWrite, bool append)
        {
            FileMode mode = FileMode.OpenOrCreate;
            if (append)
            {
                mode = FileMode.Append;
            }
            FileStream stream = new FileStream(fileToWrite, mode, FileAccess.Write, FileShare.Read);
            streamWriter = new StreamWriter(stream);
            streamWriter.AutoFlush = true;
        }

        public bool LogMessage(string message)
        {
            try
            {
                streamWriter.WriteLine(message);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        ~SimpleFileLogStrategy()
        {
            try
            {
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (Exception)
            {
            }
            
        }
    }
}
