using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.CrossCuttingConcerns.Csv;

public interface ICsvWriter<T>
{
    Task WriteAsync(IEnumerable<T> collection, Stream stream);
}
