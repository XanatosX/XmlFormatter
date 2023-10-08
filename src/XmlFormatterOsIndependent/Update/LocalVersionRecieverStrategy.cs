using System.Reflection;
using XmlFormatterModel.Update.Adapter;

namespace XmlFormatterOsIndependent.Update
{
    /// <summary>
    /// Strategy to use to get the local version
    /// </summary>
    internal class LocalVersionReceiverStrategy : LocalResourceVersionReceiver
    { 
        /// <inheritdoc/>
        protected override Assembly GetAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }

        /// <inheritdoc/>
        protected override string GetResourcePath()
        {
            return "XmlFormatterOsIndependent.Version.txt";
        }
    }
}
