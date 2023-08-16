﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Storages.Local;

public class LocalFileStorageManager : IFileStorageManager
{
    private readonly LocalOptions _option;

    public LocalFileStorageManager(LocalOptions option)
    {
        _option = option;
    }

    public async Task CreateAsync(IFileEntry fileEntry, Stream stream, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_option.Path, fileEntry.FileLocation);

        var folder = Path.GetDirectoryName(filePath);

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        using (var fileStream = File.Create(filePath))
        {
            await stream.CopyToAsync(fileStream, cancellationToken);
        }
    }

    public async Task DeleteAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            var path = Path.Combine(_option.Path, fileEntry.FileLocation);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }, cancellationToken);
    }

    public Task<byte[]> ReadAsync(IFileEntry fileEntry, CancellationToken cancellationToken = default)
    {
        return File.ReadAllBytesAsync(Path.Combine(_option.Path, fileEntry.FileLocation), cancellationToken);
    }

    public Task DownloadAsync(IFileEntry fileEntry, string path, CancellationToken cancellationToken = default)
    {
        File.Copy(Path.Combine(_option.Path, fileEntry.FileLocation), path, true);
        return Task.CompletedTask;
    }

    public Task DownloadAsync(IFileEntry fileEntry, Stream stream, CancellationToken cancellationToken = default)
    {
        using (FileStream fileStream = File.OpenRead(Path.Combine(_option.Path, fileEntry.FileLocation)))
        {
            fileStream.CopyTo(stream);
        }

        return Task.CompletedTask;
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
