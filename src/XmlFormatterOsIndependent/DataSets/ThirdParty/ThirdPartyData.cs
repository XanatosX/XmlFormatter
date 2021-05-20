using System;
using System.Collections.Generic;

namespace XmlFormatterOsIndependent.DataSets.ThirdParty
{
    public class ThirdPartyData
    {
        public IReadOnlyList<ThirdClassLibraryData> LibraryData { get; }

        public ThirdPartyData(List<ThirdClassLibraryData> libraries)
        {
            LibraryData = libraries;
        }
    }
}
