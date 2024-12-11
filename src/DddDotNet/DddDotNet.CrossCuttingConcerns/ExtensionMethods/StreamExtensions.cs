using System.IO;

namespace DddDotNet.CrossCuttingConcerns.ExtensionMethods;

public static class StreamExtensions
{
    public static void ResetPosition(this Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);
    }

    public static byte[] GetBytes(this Stream stream)
    {
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}
