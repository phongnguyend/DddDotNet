using Google.Cloud.SecretManager.V1;
using Microsoft.Extensions.Configuration;
using System;

namespace DddDotNet.Infrastructure.Configuration;

internal class GoogleCloudSecretManagerConfigurationProvider : ConfigurationProvider
{
    private readonly GoogleCloudSecretManagerOptions _options;

    public GoogleCloudSecretManagerConfigurationProvider(GoogleCloudSecretManagerOptions options)
    {
        _options = options;
    }

    public override void Load()
    {
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", _options.CredentialFilePath);
        var client = SecretManagerServiceClient.Create();

        var secrets = client.ListSecrets(_options.Parent);

        foreach (var secret in secrets)
        {
            var secretVersionName = new SecretVersionName(secret.SecretName.ProjectId, secret.SecretName.SecretId, "latest");
            var secretVersion = client.AccessSecretVersion(secretVersionName);

            var key = secret.SecretName.SecretId.Replace("__", ":");
            var value = secretVersion.Payload.Data.ToStringUtf8();

            Set(key, value);
        }
    }
}

public class GoogleCloudSecretManagerOptions
{
    public string CredentialFilePath { get; set; }

    public string Parent { get; set; }
}

public class GoogleCloudSecretManagerConfigurationSource : IConfigurationSource
{
    private readonly GoogleCloudSecretManagerOptions _options;

    public GoogleCloudSecretManagerConfigurationSource(GoogleCloudSecretManagerOptions options)
    {
        _options = options;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new GoogleCloudSecretManagerConfigurationProvider(_options);
    }
}

public static class GoogleCloudSecretManagerConfigurationExtensions
{
    public static IConfigurationBuilder AddGoogleCloudSecretManager(this IConfigurationBuilder builder, GoogleCloudSecretManagerOptions options)
    {
        return builder.Add(new GoogleCloudSecretManagerConfigurationSource(options));
    }
}
