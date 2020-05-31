﻿using System;

namespace XmlFormatterModel.Logging
{
    /// <summary>
    /// This interfaces defines a strategy on how to log a message
    /// </summary>
    public interface ILoggingStrategy : IDisposable
    {
        /// <summary>
        /// Log the given message to the output channel
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <returns>True if logging was successful</returns>
        bool LogMessage(string message);
    }
}