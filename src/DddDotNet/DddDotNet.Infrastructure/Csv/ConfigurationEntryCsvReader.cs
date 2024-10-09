using CsvHelper.Configuration;
using DddDotNet.CrossCuttingConcerns.Csv;
using DddDotNet.Domain.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Csv;

public class ConfigurationEntryCsvReader : ICsvReader<List<ConfigurationEntry>>
{
    public Task<List<ConfigurationEntry>> ReadAsync(Stream stream)
    {
        using var reader = new StreamReader(stream);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
        };

        using var csv = new CsvHelper.CsvReader(reader, config);
        return Task.FromResult(csv.GetRecords<ConfigurationEntry>().ToList());
    }
}
