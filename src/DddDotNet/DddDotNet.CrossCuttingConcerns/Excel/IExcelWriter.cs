using System.IO;

namespace DddDotNet.CrossCuttingConcerns.Excel
{
    public interface IExcelWriter<T>
    {
        void Write(T data, Stream stream);
    }
}
