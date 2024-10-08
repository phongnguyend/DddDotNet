using System.Threading.Tasks;

namespace DddDotNet.CrossCuttingConcerns.HtmlGenerator;

public interface IHtmlGenerator
{
    Task<string> GenerateAsync(string template, object model);
}
