using System.Collections.Generic;
using System.Linq;

namespace XmlFormatterOsIndependent.DataSets.ThirdParty.LoadableClasses
{
    public class SerializeableThirdPartyData
    {
        public List<SerializeableThirdClassLibraryData> LibraryData;

        public SerializeableThirdPartyData()
        {
            LibraryData = new List<SerializeableThirdClassLibraryData>();
        }

        public ThirdPartyData GetImmutableClass()
        {
            List<ThirdClassLibraryData> data = LibraryData.Select(data => data.GetImmutableClass()).ToList();
            return new ThirdPartyData(data);
        }
    }
}
