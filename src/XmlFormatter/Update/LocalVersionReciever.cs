using System.Reflection;
using XmlFormatterModel.Update.Adapter;

namespace XmlFormatter.Update
{
    /// <summary>
    /// Get the local version
    /// </summary>
    class LocalVersionReciever : LocalResourceVersionReciever
    {
        /// <inheritdoc/>
        protected override Assembly GetAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }

        /// <inheritdoc/>
        protected override string GetResourcePath()
        {
            return "XmlFormatter.Version.txt";
        }
    }
}
