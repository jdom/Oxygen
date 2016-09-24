using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Orleans.Host.Abstractions
{
    // this would go into the (optional) storage providers DLL.
    public interface IStorageProvidersFactory
    {
        ISiloBuilder Silo { get; }
        void AddProvider(string name, IStorageProvider provider);
    }

    public static class SiloStorageProviderExtensions
    {
        public static void AddStorageProviders(this IServiceCollection services)
        {
            services.AddSingleton<IStorageProvidersFactory, StorageProvidersFactory>();
        }
        public static void ConfigureStorageProviders(this ISiloBuilder silo, Action<IStorageProvidersFactory> configureMethod)
        {
            var factory = silo.ApplicationServices.GetRequiredService<IStorageProvidersFactory>();
            configureMethod(factory);
        }

    }

    public class StorageProvidersFactory : IStorageProvidersFactory
    {
        private Dictionary<string, IStorageProvider> providers = new Dictionary<string, IStorageProvider>();
        public StorageProvidersFactory(ISiloBuilder silo)
        {
            Silo = silo;
        }
        public ISiloBuilder Silo { get; }

        public void AddProvider(string name, IStorageProvider provider)
        {
            this.providers.Add(name, provider);
        }
    }

    public interface IStorageProvider
    {
        string Name { get; }
        Task Start();
        Task Stop();
    }
}