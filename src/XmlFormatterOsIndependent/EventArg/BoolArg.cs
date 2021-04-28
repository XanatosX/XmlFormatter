using System;

namespace XmlFormatterOsIndependent.EventArg
{
    class BoolArg : EventArgs
    {
        public bool Result { get; }

        public BoolArg(bool result)
        {
            Result = result;
        }

        
    }
}
