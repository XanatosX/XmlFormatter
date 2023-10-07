using System;
using XmlFormatterModel.Update;
using XmlFormatterModel.Update.Strategies;

namespace XmlFormatterOsIndependent.Update
{
    /// <summary>
    /// This class will create you a valid update manager
    /// </summary>
    [Obsolete]
    public class UpdateManagerFactory : IVersionManagerFactory
    {
        /// <summary>
        /// The version manager
        /// </summary>
        private IVersionManager manager;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public UpdateManagerFactory()
        {
            manager = null;
        }

        /// <summary>
        /// Get the version manager to use
        /// </summary>
        /// <returns>The correct version manager</returns>
        public IVersionManager GetVersionManager()
        {
            if (manager == null)
            {
                manager = new VersionManager(
                new DefaultStringConvertStrategy(),
                new LocalVersionReceiverStrategy(),
                new GitHubVersionReceiverStrategy());
            }
            return manager;
        }
    }
}
