namespace XmlFormatter.Application.Services.UpdateFeature
{
    /// <summary>
    /// Create a new version manager
    /// </summary>
    public interface IVersionManagerFactory
    {
        /// <summary>
        /// Get a working version manager
        /// </summary>
        /// <returns>A working version manager</returns>
        IVersionManager GetVersionManager();
    }
}
