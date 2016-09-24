
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Microsoft.Orleans.Host.Abstractions
{
    public interface IStartup
    {
        IEnumerable<Type> Members { get; } // not sure I understand why this?
        IServiceProvider ConfigureServices(IServiceCollection services);
    }
}