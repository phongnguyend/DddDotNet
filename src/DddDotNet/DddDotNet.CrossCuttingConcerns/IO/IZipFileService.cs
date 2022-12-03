using System.Collections.Generic;
using System.IO;

namespace DddDotNet.CrossCuttingConcerns.IO
{
    public interface IZipFileService
    {
        void CreateFromStreams(Dictionary<string, Stream> input, Stream output);

        Stream CreateFromStreams(Dictionary<string, Stream> input);
    }
}
