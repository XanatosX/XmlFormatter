namespace XmlFormatterModel.Logging
{
    /// <summary>
    /// The format strategy to log a message
    /// </summary>
    public interface ILoggingFormatStrategy
    {
        /// <summary>
        /// Format the message to a specific format
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <returns>the formatted string to use</returns>
        string FormatMessage(LoggingMessage message);
    }
}
