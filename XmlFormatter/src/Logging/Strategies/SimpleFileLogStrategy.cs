using System;
using System.IO;
using XmlFormatter.src.Interfaces.Logging;

namespace XmlFormatter.src.Logging.Strategies
{
    /// <summary>
    /// This simple logger will write the data into a given file
    /// </summary>
    class SimpleFileLogStrategy : ILoggingStrategy
    {
        /// <summary>
        /// The write stream to use
        /// </summary>
        private readonly StreamWriter streamWriter;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="fileToWrite">The file to write to</param>
        public SimpleFileLogStrategy(string fileToWrite)
            : this(fileToWrite, true)
        {

        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="fileToWrite">The file to write to</param>
        /// <param name="append">Append the data or create a new file</param>
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

        /// <inheritdoc/>
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

        /// <summary>
        /// Descturor of this class instance
        /// </summary>
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
