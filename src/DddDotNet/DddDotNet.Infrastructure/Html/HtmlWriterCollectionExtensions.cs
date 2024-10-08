using DddDotNet.CrossCuttingConcerns.Html;
using DddDotNet.Domain.Entities;
using DddDotNet.Infrastructure.HtmlGenerators;
using RazorLight;
using System;

namespace Microsoft.Extensions.DependencyInjection;

public static class HtmlWriterCollectionExtensions
{
    public static IServiceCollection AddHtmlGenerator(this IServiceCollection services)
    {
        var engine = new RazorLightEngineBuilder()
               .UseFileSystemProject(Environment.CurrentDirectory)
               .UseMemoryCachingProvider()
               .Build();

        services.AddSingleton<IRazorLightEngine>(engine);
        services.AddSingleton<IHtmlWriter<ConfigurationEntry>, ConfigurationEntryHtmlWriter>();

        return services;
    }
}
