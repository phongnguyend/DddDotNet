using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.CrossCuttingConcerns.Html;

public interface IHtmlWriter<T>
{
    Task WriteAsync(T data, Stream stream);
}
