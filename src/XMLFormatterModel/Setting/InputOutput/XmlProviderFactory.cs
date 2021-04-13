using XmlFormatterModel.Setting;

namespace XMLFormatterModel.Setting.InputOutput
{
    /// <summary>
    /// Return classes for xml serialisation
    /// </summary>
    public class XmlProviderFactory : IPersistentFactory
    {
        /// <summary>
        /// Get the loading provider
        /// </summary>
        /// <returns>A valid loading provider</returns>
        public ISettingLoadProvider CreateLoader()
        {
            return new XmlLoaderProvider();
        }

        /// <summary>
        /// Get the saving provider
        /// </summary>
        /// <returns>A valid saving provider</returns>
        public ISettingSaveProvider CreateSaver()
        {
            return new XmlSaverProvider();
        }
    }
}
