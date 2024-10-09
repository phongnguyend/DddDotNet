using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.CrossCuttingConcerns.Excel;

public interface IExcelReader<T>
{
    Task<T> ReadAsync(Stream stream);
}
