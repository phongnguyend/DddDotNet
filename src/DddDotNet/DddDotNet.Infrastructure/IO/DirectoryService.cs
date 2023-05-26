using DddDotNet.CrossCuttingConcerns.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace DddDotNet.Infrastructure.IO
{
    public class DirectoryService : IDirectoryService
    {
        private static class DirectorySeparator
        {
            public const string Windows = "\\";
            public const string Unix = "/";
            public const string Mac = ":";
        }

        private static class VolumeSeparator
        {
            public const string Windows = ":";
            public const string Unix = "/";
            public const string Mac = ":";
        }

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

        public DateTime GetCreationTime(string path)
        {
            return Directory.GetCreationTime(path);
        }
    }
}
