using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace XmlFormatterOsIndependent.DataSets.ThirdParty
{
    class ThirdPartyDataComparer : IEqualityComparer<ThirdClassLibraryData>
    {
        public bool Equals([AllowNull] ThirdClassLibraryData x, [AllowNull] ThirdClassLibraryData y)
        {
            if (Object.ReferenceEquals(x, y))
            {
                return true;
            }
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
            {
                return false;
            }

            return x.Name.ToLower() == y.Name.ToLower() && x.Url.ToLower() == y.Url.ToLower();
        }

        public int GetHashCode([DisallowNull] ThirdClassLibraryData classLibrary)
        {
            if (Object.ReferenceEquals(classLibrary, null))
            {
                return 0;
            }

            int hashName = classLibrary.Name.ToLower() == null ? 0 : classLibrary.Name.ToLower().GetHashCode();
            int hashUrl = classLibrary.Url.ToLower() == null ? 0 : classLibrary.Url.ToLower().GetHashCode();

            return hashName ^ hashUrl;
        }
    }
}
