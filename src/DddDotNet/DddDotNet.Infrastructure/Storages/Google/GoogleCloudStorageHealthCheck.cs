using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Storages.Google
{
    public class GoogleCloudStorageHealthCheck : IHealthCheck
    {
        private readonly GoogleCloudStorageOptions _options;

        public GoogleCloudStorageHealthCheck(GoogleCloudStorageOptions options)
        {
            _options = options;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                GoogleCredential credential = null;
                using (var jsonStream = new FileStream(_options.CredentialFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    credential = GoogleCredential.FromStream(jsonStream);
                }

                var storageClient = StorageClient.Create(credential);

                var fileName = _options.Path + $"HealthCheck/{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")}-{Guid.NewGuid()}.txt";

                using var stream = new MemoryStream(Encoding.UTF8.GetBytes($"HealthCheck {DateTime.Now}"));
                var response = await storageClient.UploadObjectAsync(_options.BucketName, fileName, null, stream);
                await storageClient.DeleteObjectAsync(_options.BucketName, fileName);

                return HealthCheckResult.Healthy($"BucketName: {_options.BucketName}");
            }
            catch (Exception exception)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, null, exception);
            }
        }
    }
}
