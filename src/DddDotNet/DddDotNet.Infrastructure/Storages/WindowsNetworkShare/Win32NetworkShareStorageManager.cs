using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Storages.WindowsNetworkShare
{
    public class Win32NetworkShareStorageManager : IFileStorageManager
    {
        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(Win32NetResource netResource, string password, string username, int flags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string name, int flags, bool force);

        private readonly Win32NetworkShareOptions _option;

        public Win32NetworkShareStorageManager(Win32NetworkShareOptions option)
        {
            _option = option;
        }

        public void Connect()
        {
            if (Regex.IsMatch(_option.ShareName, @"^[a-z]", RegexOptions.IgnoreCase))
            {
                return;
            }

            var netResource = new Win32NetResource()
            {
                Scope = Win32ResourceScope.GlobalNetwork,
                ResourceType = Win32ResourceType.Disk,
                DisplayType = ResourceDisplaytype.Share,
                RemoteName = _option.ShareName,
            };

            var result = WNetAddConnection2(netResource, _option.Password, _option.UserName, 0);

            if (result != 0)
            {
                throw new Win32Exception(result);
            }
        }

        public void Disconnect()
        {
            if (Regex.IsMatch(_option.ShareName, @"^[a-z]", RegexOptions.IgnoreCase))
            {
                return;
            }

            var result = WNetCancelConnection2(_option.ShareName, 0, true);
            if (result != 0)
            {
                throw new Win32Exception(result);
            }
        }

        public void ResetConnection()
        {
            try
            {
                Disconnect();
            }
            catch
            {
            }

            Connect();
        }

        public async Task CreateAsync(IFileEntry fileEntry, Stream stream, CancellationToken cancellationToken = default)
        {
            var filePath = GetFilePath(fileEntry);

            var folder = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            using var fileStream = File.Create(filePath);
            await stream.CopyToAsync(fileStream, cancellationToken);
        }

        private string GetFilePath(IFileEntry fileEntry)
        {
            return Path.Combine(_option.ShareName, _option.Path ?? string.Empty, fileEntry.FileLocation);
        }

        public async Task DeleteAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                var path = GetFilePath(fileEntry);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }, cancellationToken);
        }

        public Task<byte[]> ReadAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default)
        {
            return File.ReadAllBytesAsync(GetFilePath(fileEntry), cancellationToken);
        }

        public Task ArchiveAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default)
        {
            // TODO: move to archive storage
            return Task.CompletedTask;
        }

        public Task UnArchiveAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default)
        {
            // TODO: move to active storage
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}
