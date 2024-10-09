using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.CrossCuttingConcerns.Csv;

public interface ICsvReader<T>
{
    Task<T> ReadAsync(Stream stream);
}
