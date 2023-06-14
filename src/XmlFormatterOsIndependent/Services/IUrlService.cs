namespace XmlFormatterOsIndependent.Services;
public interface IUrlService
{
    bool IsValidUrl(string url);

    void OpenUrl(string url);
}
