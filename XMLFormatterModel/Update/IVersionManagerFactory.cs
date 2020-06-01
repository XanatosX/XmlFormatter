using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterModel.Update
{
    public interface IVersionManagerFactory
    {
        IVersionManager GetVersionManager();
    }
}
