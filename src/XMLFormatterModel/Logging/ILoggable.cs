namespace XmlFormatterModel.Logging
{
    /// <summary>
    /// Makes a class loggable
    /// </summary>
    public interface ILoggable
    {
        /// <summary>
        /// Set the logging manager for the class instance
        /// </summary>
        /// <param name="loggingManager">The logging manager to use</param>
        void SetLoggingManager(ILoggingManager loggingManager);
    }
}
