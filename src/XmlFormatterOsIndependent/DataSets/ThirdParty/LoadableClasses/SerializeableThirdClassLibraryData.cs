namespace XmlFormatterOsIndependent.DataSets.ThirdParty.LoadableClasses
{
    public class SerializeableThirdClassLibraryData
    {
        public string Name;
        public string Author;
        public string Url;

        public SerializeableThirdClassLibraryData()
        {

        }
        public ThirdClassLibraryData GetImmutableClass()
        {
            return new ThirdClassLibraryData(Name, Author, Url, "GUI Application");
        }
    }
}
