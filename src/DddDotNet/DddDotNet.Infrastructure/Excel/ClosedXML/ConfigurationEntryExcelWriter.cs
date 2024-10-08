﻿using ClosedXML.Excel;
using DddDotNet.CrossCuttingConcerns.Excel;
using DddDotNet.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Excel.ClosedXML;

public class ConfigurationEntryExcelWriter : IExcelWriter<List<ConfigurationEntry>>
{
    public Task WriteAsync(List<ConfigurationEntry> data, Stream stream)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Sheet1");

        worksheet.Cell("A1").Value = "Key";
        worksheet.Cell("B1").Value = "Value";
        worksheet.Range("A1:B1").Style.Font.Bold = true;

        int i = 2;
        foreach (var row in data)
        {
            worksheet.Cell("A" + i).Value = row.Key;
            worksheet.Cell("B" + i).Value = row.Value;
            i++;
        }

        workbook.SaveAs(stream);

        return Task.CompletedTask;
    }
}
