using DddDotNet.CrossCuttingConcerns.Excel;
using DddDotNet.Domain.Entities;
using ExcelDataReader;
using System.Collections.Generic;
using System.IO;

namespace DddDotNet.Infrastructure.Excel.ExcelDataReader;

public class ConfigurationEntryExcelReader : IExcelReader<List<ConfigurationEntry>>
{
    public List<ConfigurationEntry> Read(Stream stream)
    {
        var rows = new List<ConfigurationEntry>();
        var reader = ExcelReaderFactory.CreateReader(stream);
        do
        {
            if (reader.VisibleState == "hidden")
            {
                continue;
            }

            var sheetName = reader.Name;
            if (sheetName == "IgnoredSheet")
            {
                continue;
            }

            var rowIndex = 0;
            var headerIndex = 0;
            while (reader.Read())
            {
                if (rowIndex < headerIndex)
                {
                }
                else if (rowIndex == headerIndex)
                {
                    var headers = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        headers.Add(reader.GetValue(i)?.ToString());
                    }

                    // TODO: Validate Headers
                }
                else
                {
                    rows.Add(new ConfigurationEntry
                    {
                        Key = reader.GetValue(0)?.ToString(),
                        Value = reader.GetValue(1)?.ToString(),
                    });
                }

                rowIndex++;
            }
        }
        while (reader.NextResult());

        return rows;
    }

    private static Dictionary<string, string> GetCorrectHeaders()
    {
        return new Dictionary<string, string>
        {
            { "A", "Key" },
            { "B", "Value" },
        };
    }
}
