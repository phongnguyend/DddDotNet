﻿using DddDotNet.CrossCuttingConcerns.Excel;
using DddDotNet.Domain.Entities;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.Excel.EPPlus;

public class ConfigurationEntryExcelWriter : IExcelWriter<List<ConfigurationEntry>>
{
    public Task WriteAsync(List<ConfigurationEntry> data, Stream stream)
    {
        using var pck = new ExcelPackage();
        var worksheet = pck.Workbook.Worksheets.Add("Sheet1");

        worksheet.Cells["A1"].Value = "Key";
        worksheet.Cells["B1"].Value = "Value";
        worksheet.Cells["A1:B1"].Style.Font.Bold = true;

        int i = 2;
        foreach (var row in data)
        {
            worksheet.Cells["A" + i].Value = row.Key;
            worksheet.Cells["B" + i].Value = row.Value;
            i++;
        }

        pck.SaveAs(stream);

        return Task.CompletedTask;
    }
}
