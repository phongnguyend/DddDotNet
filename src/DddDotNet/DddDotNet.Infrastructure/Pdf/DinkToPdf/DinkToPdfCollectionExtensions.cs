using DddDotNet.CrossCuttingConcerns.Pdf;
using DddDotNet.Domain.Entities;
using DddDotNet.Infrastructure.PdfConverters.DinkToPdf;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace Microsoft.Extensions.DependencyInjection;

public static class DinkToPdfCollectionExtensions
{
    public static IServiceCollection AddDinkToPdfConverter(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        services.AddSingleton<IPdfWriter<ConfigurationEntry>, ConfigurationEntryPdfWriter>();

        return services;
    }
}
