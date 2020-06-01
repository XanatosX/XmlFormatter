using XmlFormatterModel.Update;
using XmlFormatterModel.Update.Strategies;

namespace XmlFormatter.src.Update
{
    class VersionManagerFactory : IVersionManagerFactory
    {
        private IVersionManager manager;

        public VersionManagerFactory()
        {
            manager = null;
        }

        public IVersionManager GetVersionManager()
        {
            if (manager == null)
            {
                manager = new VersionManager(
                new DefaultStringConvertStrategy(),
                new LocalVersionReciever(),
                new GitHubVersionRecieverStrategy());
            }
            return manager;
        }
    }
}
