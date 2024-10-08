using System;
using System.Collections.Generic;
using System.IO;

namespace DddDotNet.CrossCuttingConcerns.IO;

public interface IFileService
{
    void Copy(string sourceFileName, string destFileName);

    void Copy(string sourceFileName, string destFileName, bool overwrite);

    void CopyMultipleFiles(List<(string SourceFileName, string DestFileName)> copies, bool overwrite);

    FileStream Create(string path);

    FileStream CreateTempFileStream();

    FileStream CreateTempFileStream(string path);

    StreamWriter CreateText(string path);

    void Delete(string path);

    void DeleteIfExist(string path);

    bool Exists(string path);

    FileAttributes GetAttributes(string path);

    StreamWriter GetStreamWriter(string path);

    FileStream Open(string path, FileMode mode, FileAccess access, FileShare share);

    FileStream OpenRead(string path);

    FileStream OpenWrite(string path);

    byte[] ReadAllBytes(string path);

    void SetAttributes(string path, FileAttributes fileAttributes);

    void WriteAllText(string path, string data);

    StreamReader GetStreamReader(string path);

    void WriteAllBytes(string path, byte[] byteArray);

    string ReadAllText(string path);

    DateTime GetCreationTime(string path);
}
