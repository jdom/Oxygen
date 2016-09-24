using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Orleans.Host.Abstractions
{
    // this would go into the azure storage providers DLL
    public static class AzureStorageProviderExtensions
    {
        public static void AddAzureStorage(this IStorageProvidersFactory factory, string providerName, string connectionString)
        {
            var concreteFactory = ActivatorUtilities.CreateInstance<AzureStorageProviderFactory>(factory.Silo.ApplicationServices);
            var provider = concreteFactory.Create(providerName, connectionString);
            factory.AddProvider(providerName, provider);
        }

    }

    internal class AzureStorageProviderFactory
    {
        public AzureStorageProviderFactory(/* could be injected with service dependencies if needed */)
        {
        }

        public IStorageProvider Create(string name, string connectionString)
        {
            return new AzureStorageProvider(/*could pass along injected dependencies*/ name, connectionString);
        }
    }

    internal class AzureStorageProvider : IStorageProvider
    {
        private string connectionString;

        public AzureStorageProvider(string name, string connectionString)
        {
            this.Name = name;
            this.connectionString = connectionString;
        }

        public string Name { get; }

        public Task Start()
        {
            throw new NotImplementedException();
        }

        public Task Stop()
        {
            throw new NotImplementedException();
        }
    }
}
