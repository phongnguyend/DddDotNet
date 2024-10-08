using DddDotNet.CrossCuttingConcerns.Pdf;
using DddDotNet.Domain.Entities;
using PuppeteerSharp;
using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.PdfConverters.PuppeteerSharp;

public class ConfigurationEntryPdfWriter : IPdfWriter<ConfigurationEntry>
{
    public async Task WriteAsync(ConfigurationEntry data, Stream stream)
    {
        var html = "";
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
        await using var page = await browser.NewPageAsync();
        await page.SetContentAsync(html);
        var bytes = await page.PdfDataAsync(new PdfOptions
        {
            PrintBackground = true,
        });
    }
}
