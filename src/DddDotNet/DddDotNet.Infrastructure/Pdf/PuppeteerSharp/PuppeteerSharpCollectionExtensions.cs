using DddDotNet.CrossCuttingConcerns.Pdf;
using DddDotNet.Domain.Entities;
using DddDotNet.Infrastructure.PdfConverters.PuppeteerSharp;
using PuppeteerSharp;

namespace Microsoft.Extensions.DependencyInjection;

public static class PuppeteerSharpCollectionExtensions
{
    public static IServiceCollection AddPuppeteerSharpPdfConverter(this IServiceCollection services)
    {
        var browserFetcher = new BrowserFetcher();
        browserFetcher.DownloadAsync().GetAwaiter().GetResult();

        services.AddSingleton<IPdfWriter<ConfigurationEntry>, ConfigurationEntryPdfWriter>();

        return services;
    }
}
