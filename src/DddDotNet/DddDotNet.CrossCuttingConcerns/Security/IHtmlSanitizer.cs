namespace DddDotNet.CrossCuttingConcerns.Security;

public interface IHtmlSanitizer
{
    string Sanitize(string html);
}
