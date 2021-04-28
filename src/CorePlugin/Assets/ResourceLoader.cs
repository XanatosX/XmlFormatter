using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CorePlugin.Assets
{
    class ResourceLoader
    {
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
