using System.IO;

namespace DddDotNet.CrossCuttingConcerns.ExtensionMethods
{
    public static class StreamExtensions
    {
        public static void ResetPosition(this Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
        }
    }
}
