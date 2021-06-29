using DddDotNet.CrossCuttingConcerns.PdfConverter;
using DddDotNet.Infrastructure.PdfConverters.DinkToPdf;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DinkToPdfConverterCollectionExtensions
    {
        public static IServiceCollection AddDinkToPdfConverter(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddSingleton<IPdfConverter, DinkToPdfConverter>();

            return services;
        }
    }
}
