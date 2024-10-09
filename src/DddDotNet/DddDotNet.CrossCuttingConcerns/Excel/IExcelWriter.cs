using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.CrossCuttingConcerns.Excel;

public interface IExcelWriter<T>
{
    Task WriteAsync(T data, Stream stream);
}
