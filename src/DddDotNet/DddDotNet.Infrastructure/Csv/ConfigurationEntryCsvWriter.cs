using DddDotNet.CrossCuttingConcerns.Csv;
using DddDotNet.Domain.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Csv;

public class ConfigurationEntryCsvWriter : ICsvWriter<List<ConfigurationEntry>>
{
    public Task WriteAsync(List<ConfigurationEntry> collection, Stream stream)
    {
        using var writer = new StreamWriter(stream);
        using var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(collection);
        return Task.CompletedTask;
    }
}
