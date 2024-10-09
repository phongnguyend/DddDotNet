using DddDotNet.CrossCuttingConcerns.Csv;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Csv;

public class CsvReader<T> : ICsvReader<T>
{
    public Task<IEnumerable<T>> ReadAsync(Stream stream)
    {
        using var reader = new StreamReader(stream);
        using var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);
        return Task.FromResult<IEnumerable<T>>(csv.GetRecords<T>().ToList());
    }
}
