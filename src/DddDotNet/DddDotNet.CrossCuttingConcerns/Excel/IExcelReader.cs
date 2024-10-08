using System.IO;

namespace DddDotNet.CrossCuttingConcerns.Excel;

public interface IExcelReader<T>
{
    T Read(Stream stream);
}
