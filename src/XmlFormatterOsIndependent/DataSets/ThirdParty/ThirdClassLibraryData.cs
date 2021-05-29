using PluginFramework.DataContainer;

namespace XmlFormatterOsIndependent.DataSets.ThirdParty
{
    public class ThirdClassLibraryData : ThirdPartyLibrary
    {

        public string Scope { get; }

        public ThirdClassLibraryData(string name, string author, string url, string scope)
            : base(name, author, url)
        {
            Scope = scope.Trim();
        }
    }
}
