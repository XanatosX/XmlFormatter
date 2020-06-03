using XmlFormatterModel.Update;
using XmlFormatterModel.Update.Strategies;

namespace XmlFormatter.src.Update
{
    /// <summary>
    /// Create a new version manager to use
    /// </summary>
    internal class VersionManagerFactory : IVersionManagerFactory
    {
        /// <summary>
        /// The version manager to use
        /// </summary>
        private IVersionManager manager;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public VersionManagerFactory()
        {
            manager = null;
        }

        /// <inheritdoc/>
        public IVersionManager GetVersionManager()
        {
            manager = manager ?? new VersionManager(
                new DefaultStringConvertStrategy(),
                new LocalVersionReciever(),
                new GitHubVersionRecieverStrategy());
            
            return manager;
        }
    }
}
