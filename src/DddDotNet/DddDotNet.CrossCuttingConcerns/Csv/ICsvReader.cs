using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.CrossCuttingConcerns.Csv;

public interface ICsvReader<T>
{
    Task<IEnumerable<T>> ReadAsync(Stream stream);
}
