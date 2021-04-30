using System;

namespace XmlFormatterOsIndependent.EventArg
{
    /// <summary>
    /// Event arg class for returning a single boolean
    /// </summary>
    class BoolArg : EventArgs
    {
        /// <summary>
        /// The result of the previous action
        /// </summary>
        public bool Result { get; }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="result">The result to save in the class</param>
        public BoolArg(bool result)
        {
            Result = result;
        }

        
    }
}
