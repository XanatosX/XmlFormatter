using System.Reflection;
using XmlFormatterModel.Update.Adapter;

namespace XmlFormatter.Update
{
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
