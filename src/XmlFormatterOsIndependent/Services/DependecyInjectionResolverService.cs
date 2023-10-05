﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace XmlFormatterOsIndependent.Services;

/// <summary>
/// Service to resolve dependencies
/// </summary>
public class DependecyInjectionResolverService : IDependencyInjectionResolverService
{
    /// <summary>
    /// THe internal service provider 
    /// </summary>
    private readonly IServiceProvider provider;

    /// <summary>
    /// Create a new instance of the service
    /// </summary>
    /// <param name="provider">The internal provider used for the requests</param>
    public DependecyInjectionResolverService(IServiceProvider provider)
    {
        this.provider = provider;
    }

    /// <inheritdoc/>
    public T? GetService<T>() where T : class
    {
        return provider.GetService<T>();
    }

    /// <inheritdoc/>
    public IEnumerable<T> GetServices<T>() where T : class
    {
        return provider.GetServices<T>();
    }
}
