using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.CrossCuttingConcerns.Csv;

public interface ICsvWriter<T>
{
    Task WriteAsync(T data, Stream stream);
}
