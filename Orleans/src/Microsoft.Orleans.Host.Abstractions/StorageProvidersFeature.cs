using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Orleans.Host.Abstractions
{
    // this would go into the (optional) storage providers DLL.
    public interface IStorageProvidersBuilder
    {
        ISiloBuilder Silo { get; }
        void AddProvider(IStorageProvider provider);
    }

    public static class SiloStorageProviderExtensions
    {
        public static void AddStorageProviders(this IServiceCollection services)
        {
            services.AddSingleton<IStorageProvidersBuilder, StorageProvidersBuilder>();
        }
        public static void ConfigureStorageProviders(this ISiloBuilder silo, Action<IStorageProvidersBuilder> configureMethod)
        {
            var factory = silo.ApplicationServices.GetRequiredService<IStorageProvidersBuilder>();
            configureMethod(factory);
        }

    }

    public class StorageProvidersBuilder : IStorageProvidersBuilder
    {
        // these would then be passed on to the provider manager so it can manage their lifetime
        private Dictionary<string, IStorageProvider> providers = new Dictionary<string, IStorageProvider>();
        public StorageProvidersBuilder(ISiloBuilder silo)
        {
            Silo = silo;
        }
        public ISiloBuilder Silo { get; }

        public void AddProvider(IStorageProvider provider)
        {
            this.providers.Add(provider.Name, provider);
        }
    }

    public interface IStorageProvider
    {
        string Name { get; }
        Task Start();
        Task Stop();
    }
}