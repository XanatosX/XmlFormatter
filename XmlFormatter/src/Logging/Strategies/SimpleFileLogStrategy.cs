﻿using System;
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
        StreamWriter streamWriter;

        public SimpleFileLogStrategy(string fileToWrite)
            : this(fileToWrite, true)
        {

        }

        public SimpleFileLogStrategy(string fileToWrite, bool append)
        {
            streamWriter = new StreamWriter(fileToWrite, append);
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
