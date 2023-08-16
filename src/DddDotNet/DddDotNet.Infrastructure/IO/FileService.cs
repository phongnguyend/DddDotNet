using DddDotNet.CrossCuttingConcerns.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.IO;

public class FileService : IFileService
{
    public void Copy(string sourceFileName, string destFileName)
    {
        File.Copy(sourceFileName, destFileName);
    }

    public void Copy(string sourceFileName, string destFileName, bool overwrite)
    {
        File.Copy(sourceFileName, destFileName, overwrite);
    }

    public void CopyMultipleFiles(List<(string SourceFileName, string DestFileName)> copies, bool overwrite)
    {
        Parallel.ForEach(
                      copies,
                      new ParallelOptions()
                      {
                          MaxDegreeOfParallelism = 4,
                      },
                      (copy) =>
                      {
                          if (Exists(copy.SourceFileName))
                          {
                              Copy(copy.SourceFileName, copy.DestFileName, overwrite);
                          }
                      });
    }

    public FileStream Create(string path)
    {
        return File.Create(path);
    }

    public FileStream CreateTempFileStream()
    {
        string tempFilePath;

        do
        {
            tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        }
        while (File.Exists(tempFilePath));

        return new FileStream(tempFilePath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.DeleteOnClose);
    }

    public FileStream CreateTempFileStream(string path)
    {
        return new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.DeleteOnClose);
    }

    public StreamWriter CreateText(string path)
    {
        return File.CreateText(path);
    }

    public void Delete(string path)
    {
        File.SetAttributes(path, FileAttributes.Normal);
        File.Delete(path);
    }

    public void DeleteIfExist(string path)
    {
        var exist = Exists(path);
        if (exist)
        {
            Delete(path);
        }
    }

    public bool Exists(string path)
    {
        return File.Exists(path);
    }

    public FileAttributes GetAttributes(string path)
    {
        return File.GetAttributes(path);
    }

    public StreamWriter GetStreamWriter(string path)
    {
        return new StreamWriter(path);
    }

    public FileStream Open(string path, FileMode mode, FileAccess access, FileShare share)
    {
        return File.Open(path, mode, access, share);
    }

    public FileStream OpenRead(string path)
    {
        return File.OpenRead(path);
    }

    public FileStream OpenWrite(string path)
    {
        return File.OpenWrite(path);
    }

    public byte[] ReadAllBytes(string path)
    {
        return File.ReadAllBytes(path);
    }

    public void SetAttributes(string path, FileAttributes fileAttributes)
    {
        File.SetAttributes(path, fileAttributes);
    }

    public void WriteAllText(string path, string data)
    {
        File.WriteAllText(path, data);
    }

    public StreamReader GetStreamReader(string path)
    {
        return new StreamReader(path);
    }

    public void WriteAllBytes(string path, byte[] byteArray)
    {
        File.WriteAllBytes(path, byteArray);
    }

    public string ReadAllText(string path)
    {
        return File.ReadAllText(path);
    }

    public DateTime GetCreationTime(string path)
    {
        FileInfo fi = new FileInfo(path);
        return fi.CreationTime;
    }
}
