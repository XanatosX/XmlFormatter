using System;
using System.IO;
using System.Reflection;

namespace CorePlugin.Assets
{
    /// <summary>
    /// Class to use for load embedded resources
    /// </summary>
    [Obsolete]
    internal class ResourceLoader
    {
        /// <summary>
        /// Load the resource by name
        /// </summary>
        /// <param name="resourceName">The name of the resource to load</param>
        /// <returns>Return the data string from the loaded resource</returns>
        public string LoadResource(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string name = "CorePlugin.Assets." + resourceName;
            string returnData = string.Empty;

            try
            {
                using (Stream stream = assembly.GetManifestResourceStream(name))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        returnData = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                return returnData;
            }

            return returnData;
        }
    }
}
