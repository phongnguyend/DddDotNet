using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Storages;

public interface IFileStorageManager : IDisposable
{
    Task CreateAsync(IFileEntry fileEntry, Stream stream, CancellationToken cancellationToken = default);

    Task<byte[]> ReadAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default);

    Task DownloadAsync(IFileEntry fileEntry, string path, CancellationToken cancellationToken = default);

    Task DownloadAsync(IFileEntry fileEntry, Stream stream, CancellationToken cancellationToken = default);

    Task DeleteAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default);

    Task ArchiveAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default);

    Task UnArchiveAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default);
}

public interface IFileEntry
{
    public Guid Id { get; set; }

    public string FileName { get; set; }

    public string FileLocation { get; set; }
}

public class FileEntry : IFileEntry
{
    public Guid Id { get; set; }

    public string FileName { get; set; }

    public string FileLocation { get; set; }
}
