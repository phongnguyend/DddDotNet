using DddDotNet.Infrastructure.Storages.Ftp;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DddDotNet.IntegrationTests.Infrastructure.Storages
{
    public class FptStorageManagerTests
    {
        FtpOptions _options = new FtpOptions();

        public FptStorageManagerTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("09f024f8-e8d1-4b78-9ddd-da941692e8fa")
                .Build();

            config.GetSection("Storage:Ftp").Bind(_options);
        }

        [Fact]
        public async Task CreateAsync_Success()
        {
            using FtpStorageManager ftpStorageManager = new FtpStorageManager(_options);

            var fileEntry = new FileEntry
            {
                FileLocation = DateTime.Now.ToString("yyyy/MM/dd") + "/" + Guid.NewGuid()
            };

            var fileStream = new MemoryStream(Encoding.UTF8.GetBytes("Test"));

            await ftpStorageManager.CreateAsync(fileEntry, fileStream);

            var content1 = Encoding.UTF8.GetString(await ftpStorageManager.ReadAsync(fileEntry));

            fileStream = new MemoryStream(Encoding.UTF8.GetBytes("Test2"));

            await ftpStorageManager.CreateAsync(fileEntry, fileStream);

            var content2 = Encoding.UTF8.GetString(await ftpStorageManager.ReadAsync(fileEntry));

            await ftpStorageManager.ArchiveAsync(fileEntry);

            await ftpStorageManager.UnArchiveAsync(fileEntry);

            var path = Path.GetTempFileName();
            await ftpStorageManager.DownloadAsync(fileEntry, path);
            var content3 = File.ReadAllText(path);
            File.Delete(path);

            path = Path.GetTempFileName();
            using (var tempFileStream = File.OpenWrite(path))
            {
                await ftpStorageManager.DownloadAsync(fileEntry, tempFileStream);
            }
            var content4 = File.ReadAllText(path);
            File.Delete(path);

            await ftpStorageManager.DeleteAsync(fileEntry);

            Assert.Equal("Test", content1);
            Assert.Equal("Test2", content2);
            Assert.Equal("Test2", content3);
            Assert.Equal("Test2", content4);
        }
    }
}
