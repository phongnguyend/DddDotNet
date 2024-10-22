using DddDotNet.CrossCuttingConcerns.Security;

namespace DddDotNet.Infrastructure.Security;

public class HtmlSanitizer : IHtmlSanitizer
{
    private readonly Ganss.Xss.HtmlSanitizer _sanitizer;

    public HtmlSanitizer()
    {
        _sanitizer = new Ganss.Xss.HtmlSanitizer();
    }

    public string Sanitize(string html)
    {
        return _sanitizer.Sanitize(html);
    }
}
