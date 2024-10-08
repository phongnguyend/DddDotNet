using System;
using System.Collections.Generic;
using System.IO;

namespace DddDotNet.CrossCuttingConcerns.IO;

public interface IDirectoryService
{
    DirectoryInfo CreateDirectory(string path);

    void Delete(string path, bool recursive);

    IEnumerable<DirectoryInfo> EnumerateDirectories(string path);

    bool Exists(string path);

    string[] GetDirectories(string path);

    string[] GetFiles(string path);

    string GetTempFolder(string root);

    DateTime GetCreationTime(string path);
}
