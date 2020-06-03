using XmlFormatterModel.Update;
using XmlFormatterModel.Update.Strategies;

namespace XmlFormatterOsIndependent.Update
{
    class UpdateManagerFactory : IVersionManagerFactory
    {
        private IVersionManager manager;

        public UpdateManagerFactory()
        {
            manager = null;
        }

        public IVersionManager GetVersionManager()
        {
            if (manager == null)
            {
                manager = new VersionManager(
                new DefaultStringConvertStrategy(),
                new LocalVersionRecieverStrategy(),
                new GitHubVersionRecieverStrategy());
            }
            return manager;
        }
    }
}
