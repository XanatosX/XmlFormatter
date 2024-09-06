using System.Globalization;
using System.IO;
using System.Reflection;

namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Service to load resource files from the assembly
/// </summary>
public class ResourceLoaderService
{
    /// <summary>
    /// The current assembly
    /// </summary>
    private Assembly assembly;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    public ResourceLoaderService()
    {
        assembly = Assembly.GetExecutingAssembly();
    }

    /// <summary>
    /// Get the resource from a given resource path, if nothing was found return null
    /// </summary>
    /// <param name="resourcePath">The resource path to request</param>
    /// <returns>The content of the resource or null</returns>
    public string? GetResource(string resourcePath)
    {
        string? returnString = null;
        try
        {
            using (Stream? stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream is not null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        returnString = reader.ReadToEnd();
                    }
                }
            }
        }
        catch (System.Exception)
        {
            // The request failed but this is properly okay, I guess
        }

        return returnString;
    }

    /// <summary>
    /// Get the resource from a given resource path, if nothing was found return null
    /// This method will try to request a localized version of the resource if it exists,
    /// Otherwise it will try to return the resource in the default language
    /// </summary>
    /// <param name="resourcePath">The resource path to request</param>
    /// <returns>The content of the resource or null</returns>
    public string? GetLocalizedString(string resourcePath)
    {
        var name = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        FileInfo info = new FileInfo(resourcePath);
        string fileName = resourcePath.Replace(info.Extension, $".{name}{info.Extension}");
        string? returnString = GetResource(fileName);
        return string.IsNullOrEmpty(returnString) ? GetResource(resourcePath) : returnString;
    }

}