using DddDotNet.Domain.Entities;
using DddDotNet.Infrastructure.Excel.EPPlus;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace DddDotNet.IntegrationTests.Infrastructure.Excel.EPPlus
{
    public class ConfigurationEntryExcelTests
    {
        [Fact]
        public void Read_ReturnData()
        {
            ConfigurationEntryExcelReader reader = new ConfigurationEntryExcelReader();
            
            using var fileStream = File.OpenRead("Infrastructure/Excel/ConfigurationEntries.xlsx");
            var entries = reader.Read(fileStream);
            
            Assert.Equal(8, entries.Count);
            Assert.Equal("Key1", entries[0].Key);
            Assert.Equal("Value 1", entries[0].Value);
            Assert.Equal("Key8", entries[7].Key);
            Assert.Equal("Value 8", entries[7].Value);
        }

        [Fact]
        public void Write_Ok()
        {
            ConfigurationEntryExcelWriter writer = new ConfigurationEntryExcelWriter();
            
            using (var fileStream = File.OpenWrite("Infrastructure/Excel/ConfigurationEntries1.xlsx"))
            {
                writer.Write(new List<ConfigurationEntry> {
                    new ConfigurationEntry
                    {
                        Key = "Key1",
                        Value = "Value 1",
                    },
                    new ConfigurationEntry
                    {
                        Key = "Key2",
                        Value = "Value 2",
                    },
                    new ConfigurationEntry
                    {
                        Key = "Key5",
                        Value = "Value 5",
                    },
                }, fileStream);
            }

            ConfigurationEntryExcelReader reader = new ConfigurationEntryExcelReader();
            
            using var fileStream2 = File.OpenRead("Infrastructure/Excel/ConfigurationEntries1.xlsx");
            var entries = reader.Read(fileStream2);
            
            Assert.Equal(3, entries.Count);
            Assert.Equal("Key1", entries[0].Key);
            Assert.Equal("Value 1", entries[0].Value);
            Assert.Equal("Key5", entries[2].Key);
            Assert.Equal("Value 5", entries[2].Value);
        }
    }
}
