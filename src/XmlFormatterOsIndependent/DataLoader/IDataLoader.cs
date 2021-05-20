namespace XmlFormatterOsIndependent.DataLoader
{
    interface IDataLoader<T>
    {
        T Load(string path);
    }
}
