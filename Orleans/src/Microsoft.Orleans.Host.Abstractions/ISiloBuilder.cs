using System;
using System.Collections.Generic;

namespace Microsoft.Orleans.Host.Abstractions
{
    public interface ISiloBuilder
    {
        /// <summary>
        /// Gets or sets the System.IServiceProvider that provides access to the application's
        ///    service container.
        /// </summary>
        IServiceProvider ApplicationServices { get; set; }
        
        /// <summary>
        /// Gets a key/value collection that can be used to share data between middleware.
        /// </summary>
        IDictionary<string, object> Properties { get; }


        //IFeatureCollection ServerFeatures { get; }
    }
}