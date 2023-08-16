using Microsoft.Extensions.Configuration;

namespace DddDotNet.Infrastructure.Configuration;

public static class SqlConfigurationExtensions
{
    public static IConfigurationBuilder AddSqlServer(this IConfigurationBuilder builder, SqlServerOptions options)
    {
        return builder.Add(new SqlConfigurationSource(options));
    }
}
