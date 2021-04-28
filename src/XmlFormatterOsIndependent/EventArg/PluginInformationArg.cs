using PluginFramework.DataContainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace XmlFormatterOsIndependent.EventArg
{
    class PluginInformationArg : EventArgs
    {
        public PluginInformation Information { get; }

        public PluginInformationArg(PluginInformation information)
        {
            Information = information;
        }
    }
}
