using DddDotNet.Infrastructure.Excel.ExcelDataReader;
using System.IO;
using Xunit;

namespace DddDotNet.IntegrationTests.Infrastructure.Excel.ExcelDataReader
{
    public class ConfigurationEntryExcelTests
    {
        [Fact]
        public void Read_ReturnData()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            ConfigurationEntryExcelReader reader = new ConfigurationEntryExcelReader();
            
            using var fileStream = File.OpenRead("Infrastructure/Excel/ConfigurationEntries.xlsx");
            var entries = reader.Read(fileStream);
            
            Assert.Equal(8, entries.Count);
            Assert.Equal("Key1", entries[0].Key);
            Assert.Equal("Value 1", entries[0].Value);
            Assert.Equal("Key8", entries[7].Key);
            Assert.Equal("Value 8", entries[7].Value);
        }
    }
}
