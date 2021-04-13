namespace XmlFormatterModel.Update
{
    /// <summary>
    /// This interface represents a release asset
    /// </summary>
    public interface IReleaseAsset
    {
        /// <summary>
        /// Url to the asset for downloading
        /// </summary>
        string Url { get; }

        /// <summary>
        /// The unique id of the asset
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The name of the asset
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The label of the asset
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Size of the asset in bytes
        /// </summary>
        int Size { get; }
    }
}
