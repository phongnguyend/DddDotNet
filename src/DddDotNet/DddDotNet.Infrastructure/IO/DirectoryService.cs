using DddDotNet.CrossCuttingConcerns.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace DddDotNet.Infrastructure.IO
{
    public class DirectoryService : IDirectoryService
    {
        public DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }

        public string GetTempFolder(string root)
        {
            var path = Path.Combine(root, DateTime.Now.ToString("yyyy-MM-dd"));
            Directory.CreateDirectory(path);

            return path;
        }

        public void Delete(string path, bool recursive)
        {
            Directory.Delete(path, recursive);
        }

        public IEnumerable<DirectoryInfo> EnumerateDirectories(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            return directoryInfo.EnumerateDirectories();
        }

        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public string[] GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }

        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}
