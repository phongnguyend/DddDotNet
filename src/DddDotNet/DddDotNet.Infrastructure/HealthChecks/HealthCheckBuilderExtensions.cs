using DddDotNet.Infrastructure.HealthChecks;
using DddDotNet.Infrastructure.MessageBrokers.AzureQueue;
using DddDotNet.Infrastructure.MessageBrokers.RabbitMQ;
using DddDotNet.Infrastructure.Notification.Email.Smtp;
using DddDotNet.Infrastructure.Storages.Amazon;
using DddDotNet.Infrastructure.Storages.Azure;
using DddDotNet.Infrastructure.Storages.Local;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HealthCheckBuilderExtensions
    {
        public static IHealthChecksBuilder AddFilePathWrite(this IHealthChecksBuilder builder, string path, string name, HealthStatus failureStatus, IEnumerable<string> tags = default)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return builder.Add(new HealthCheckRegistration(
                name,
                new FilePathWriteHealthCheck(path),
                failureStatus,
                tags));
        }

        public static IHealthChecksBuilder AddSqlServer(
            this IHealthChecksBuilder builder,
            string connectionString,
            string healthQuery = default,
            string name = default,
            HealthStatus? failureStatus = default,
            IEnumerable<string> tags = default,
            TimeSpan? timeout = default)
        {
            return builder.Add(new HealthCheckRegistration(
                name,
                new SqlServerHealthCheck(connectionString, healthQuery),
                failureStatus,
                tags,
                timeout));
        }

        public static IHealthChecksBuilder AddAmazonS3(
            this IHealthChecksBuilder builder,
            AmazonOptions amazonOptions,
            string name = default,
            HealthStatus? failureStatus = default,
            IEnumerable<string> tags = default,
            TimeSpan? timeout = default)
        {
            return builder.Add(new HealthCheckRegistration(
                name,
                new AmazonS3HealthCheck(amazonOptions),
                failureStatus,
                tags,
                timeout));
        }

        public static IHealthChecksBuilder AddAzureBlobStorage(
            this IHealthChecksBuilder builder,
            AzureBlobOption azureBlobOptions,
            string name = default,
            HealthStatus? failureStatus = default,
            IEnumerable<string> tags = default,
            TimeSpan? timeout = default)
        {
            return builder.Add(new HealthCheckRegistration(
                name,
                new AzureBlobStorageHealthCheck(azureBlobOptions),
                failureStatus,
                tags,
                timeout));
        }

        public static IHealthChecksBuilder AddAzureQueueStorage(
            this IHealthChecksBuilder builder,
            string connectionString,
            string queueName,
            string name = default,
            HealthStatus? failureStatus = default,
            IEnumerable<string> tags = default,
            TimeSpan? timeout = default)
        {
            return builder.Add(new HealthCheckRegistration(
                name,
                new AzureQueueStorageHealthCheck(
                    connectionString: connectionString,
                    queueName: queueName),
                failureStatus,
                tags,
                timeout));
        }

        public static IHealthChecksBuilder AddRabbitMQ(
            this IHealthChecksBuilder builder,
            RabbitMQHealthCheckOptions options,
            string name = default,
            HealthStatus? failureStatus = default,
            IEnumerable<string> tags = default,
            TimeSpan? timeout = default)
        {
            return builder.Add(new HealthCheckRegistration(
                name,
                new RabbitMQHealthCheck(options),
                failureStatus,
                tags,
                timeout));
        }

        public static IHealthChecksBuilder AddSmtp(
            this IHealthChecksBuilder builder,
            SmtpHealthCheckOptions options,
            string name = default,
            HealthStatus? failureStatus = default,
            IEnumerable<string> tags = default,
            TimeSpan? timeout = default)
        {
            return builder.Add(new HealthCheckRegistration(
                name,
                new SmtpHealthCheck(options),
                failureStatus,
                tags,
                timeout));
        }

        public static IHealthChecksBuilder AddLocalFile(
            this IHealthChecksBuilder builder,
            LocalFileHealthCheckOptions options,
            string name = default,
            HealthStatus? failureStatus = default,
            IEnumerable<string> tags = default,
            TimeSpan? timeout = default)
        {
            return builder.Add(new HealthCheckRegistration(
                name,
                new LocalFileHealthCheck(options),
                failureStatus,
                tags,
                timeout));
        }
    }
}