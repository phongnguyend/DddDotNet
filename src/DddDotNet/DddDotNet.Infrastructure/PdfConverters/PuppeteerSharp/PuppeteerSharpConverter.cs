using DddDotNet.CrossCuttingConcerns.PdfConverter;
using PuppeteerSharp;
using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.PdfConverters.PuppeteerSharp
{
    public class PuppeteerSharpConverter : IPdfConverter
    {
        public Stream Convert(string html)
        {
            return ConvertAsync(html).GetAwaiter().GetResult();
        }

        public async Task<Stream> ConvertAsync(string html)
        {
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(html);
            return new MemoryStream(await page.PdfDataAsync());
        }
    }
}
