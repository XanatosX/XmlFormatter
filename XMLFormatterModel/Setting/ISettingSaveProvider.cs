namespace XmlFormatterModel.Setting
{
    /// <summary>
    /// This interface is representing a save provider for the settings manager
    /// </summary>
    public interface ISettingSaveProvider
    {
        /// <summary>
        /// Save all the scope which are in the manager
        /// </summary>
        /// <param name="settingsManager">The manager to save</param>
        /// <param name="filePath">The file path to save the data to</param>
        /// <returns>True if saving was a success</returns>
        bool SaveSettings(ISettingsManager settingsManager, string filePath);
    }
}
