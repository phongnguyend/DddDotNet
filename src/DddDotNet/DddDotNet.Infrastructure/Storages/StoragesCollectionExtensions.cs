using DddDotNet.Infrastructure.Storages;
using DddDotNet.Infrastructure.Storages.Amazon;
using DddDotNet.Infrastructure.Storages.Azure;
using DddDotNet.Infrastructure.Storages.Fake;
using DddDotNet.Infrastructure.Storages.Local;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StoragesCollectionExtensions
    {
        public static IServiceCollection AddLocalStorageManager(this IServiceCollection services, LocalOption options)
        {
            services.AddSingleton<IFileStorageManager>(new LocalFileStorageManager(options));

            return services;
        }

        public static IServiceCollection AddAzureBlobStorageManager(this IServiceCollection services, AzureBlobOption options)
        {
            services.AddSingleton<IFileStorageManager>(new AzureBlobStorageManager(options));

            return services;
        }

        public static IServiceCollection AddAmazonS3StorageManager(this IServiceCollection services, AmazonOptions options)
        {
            services.AddSingleton<IFileStorageManager>(new AmazonS3StorageManager(options));

            return services;
        }

        public static IServiceCollection AddFakeStorageManager(this IServiceCollection services)
        {
            services.AddSingleton<IFileStorageManager>(new FakeStorageManager());

            return services;
        }

        public static IServiceCollection AddStorageManager(this IServiceCollection services, StorageOptions options, IHealthChecksBuilder healthChecksBuilder = null)
        {
            if (options.UsedAzure())
            {
                services.AddAzureBlobStorageManager(options.Azure);

                if (healthChecksBuilder != null)
                {
                    healthChecksBuilder.AddAzureBlobStorage(
                        options.Azure.ConnectionString,
                        containerName: options.Azure.Container,
                        name: "Storage (Azure Blob)",
                        failureStatus: HealthStatus.Degraded);
                }
            }
            else if (options.UsedAmazon())
            {
                services.AddAmazonS3StorageManager(options.Amazon);

                if (healthChecksBuilder != null)
                {
                    healthChecksBuilder.AddAmazonS3(
                    options.Amazon,
                    name: "Storage (Amazon S3)",
                    failureStatus: HealthStatus.Degraded);
                }
            }
            else if (options.UsedLocal())
            {
                services.AddLocalStorageManager(options.Local);

                if (healthChecksBuilder != null)
                {
                    healthChecksBuilder.AddFilePathWrite(options.Local.Path,
                    name: "Storage (Local Directory)",
                    failureStatus: HealthStatus.Degraded);
                }
            }
            else
            {
                services.AddFakeStorageManager();
            }

            return services;
        }
    }
}
