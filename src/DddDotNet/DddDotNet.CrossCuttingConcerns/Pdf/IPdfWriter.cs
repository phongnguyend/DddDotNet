using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.CrossCuttingConcerns.Pdf;

public interface IPdfWriter<T>
{
    Task WriteAsync(T data, Stream stream);
}
