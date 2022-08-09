using DddDotNet.Infrastructure.HealthChecks;
using DddDotNet.Infrastructure.MessageBrokers.RabbitMQ;
using DddDotNet.Infrastructure.Storages.Amazon;
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
    }
}