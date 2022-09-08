using FluentFTP;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Storages.Ftp
{
    public class FtpHealthCheck : IHealthCheck
    {
        private readonly FtpOptions _options;

        public FtpHealthCheck(FtpOptions options)
        {
            _options = options;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var fileName = $"HealthCheck/{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")}-{Guid.NewGuid()}.txt";
                using var stream = new MemoryStream(Encoding.UTF8.GetBytes($"HealthCheck {DateTime.Now}"));
                await CreateAsync(fileName, stream, cancellationToken);
                await DeleteAsync(fileName);
                return HealthCheckResult.Healthy($"Host: '{_options.Host}', Path: '{_options.Path}'");
            }
            catch (Exception exception)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, exception: exception);
            }
        }

        private async Task CreateAsync(string fileName, Stream stream, CancellationToken cancellationToken = default)
        {
            using var ftp = GetFtpClient();
            await ftp.Connect(cancellationToken);
            await ftp.UploadStream(stream, GetRemoteFilePath(fileName), FtpRemoteExists.Overwrite, true, token: cancellationToken);
        }

        private AsyncFtpClient GetFtpClient()
        {
            var ftpClient = new AsyncFtpClient(_options.Host, _options.UserName, _options.Password)
            {
                Config = new FtpConfig
                {
                    EncryptionMode = FtpEncryptionMode.Implicit,

                    // ValidateAnyCertificate = true,
                },
            };
            return ftpClient;
        }

        private string GetRemoteFilePath(string fileName)
        {
            return _options.Path + fileName;
        }

        public async Task DeleteAsync(string fileName, CancellationToken cancellationToken = default)
        {
            using var ftp = GetFtpClient();
            await ftp.Connect(cancellationToken);
            await ftp.DeleteFile(GetRemoteFilePath(fileName), cancellationToken);
        }
    }
}
