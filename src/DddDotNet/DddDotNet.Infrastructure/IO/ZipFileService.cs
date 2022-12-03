using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using DddDotNet.CrossCuttingConcerns.IO;

namespace DddDotNet.Infrastructure.IO
{
    public class ZipFileService : IZipFileService
    {
        public void CreateFromStreams(Dictionary<string, Stream> input, Stream output)
        {
            using (ZipArchive archive = new ZipArchive(output, ZipArchiveMode.Update, true))
            {
                foreach (var file in input)
                {
                    ZipArchiveEntry entry = archive.CreateEntry(file.Key);
                    using var entryStream = entry.Open();
                    file.Value.CopyTo(entryStream);
                }
            }

            output.Position = 0;
        }

        public Stream CreateFromStreams(Dictionary<string, Stream> input)
        {
            var output = CreateTempFileStream();

            CreateFromStreams(input, output);

            output.Position = 0;
            return output;
        }

        public static FileStream CreateTempFileStream()
        {
            return new FileStream(Path.GetTempFileName(), FileMode.Open, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.DeleteOnClose);
        }
    }
}
