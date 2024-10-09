using DddDotNet.CrossCuttingConcerns.Csv;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Csv;

public class CsvWriter<T> : ICsvWriter<T>
{
    public Task WriteAsync(IEnumerable<T> collection, Stream stream)
    {
        using var writer = new StreamWriter(stream);
        using var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(collection);
        return Task.CompletedTask;
    }
}
