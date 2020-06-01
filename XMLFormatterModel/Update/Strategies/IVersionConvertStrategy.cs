using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterModel.Update
{
    public interface IVersionConvertStrategy
    {
        string GetStringVersion(Version version);
        Version ConvertStringToVersion(string version);
    }
}
