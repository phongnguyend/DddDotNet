using DddDotNet.CrossCuttingConcerns.Html;
using DddDotNet.Domain.Entities;
using RazorLight;
using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.HtmlGenerators;

public class ConfigurationEntryHtmlWriter : IHtmlWriter<ConfigurationEntry>
{
    private readonly IRazorLightEngine _razorLightEngine;

    public ConfigurationEntryHtmlWriter(IRazorLightEngine razorLightEngine)
    {
        _razorLightEngine = razorLightEngine;
    }

    public Task<string> GenerateAsync(string template, object model)
    {
        return _razorLightEngine.CompileRenderAsync(template, model);
    }

    public async Task WriteAsync(ConfigurationEntry data, Stream stream)
    {
        var template = "";
        var html = await _razorLightEngine.CompileRenderAsync(template, data);
    }
}
