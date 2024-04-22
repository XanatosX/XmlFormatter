namespace XmlFormatterOsIndependent.Services;

public class ResourceTranslationService
{
    public string? GetTranslation(string key)
    {
        return Properties.Resources.ResourceManager.GetString(key);
    }
}
