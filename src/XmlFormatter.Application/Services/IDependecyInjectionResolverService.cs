namespace XmlFormatterOsIndependent.Services
{
    public interface IDependencyInjectionResolverService
    {
        /// <summary>
        /// Get a service from the service provider
        /// </summary>
        /// <typeparam name="T">The type of class instance to get</typeparam>
        /// <returns>An instance of the class</returns>
        T? GetService<T>() where T : class;

        /// <summary>
        /// Get all services from the service provider for a given type
        /// </summary>
        /// <typeparam name="T">The type of classes to get instances from</typeparam>
        /// <returns>A list with all the instances</returns>
        IEnumerable<T> GetServices<T>() where T : class;
    }

}