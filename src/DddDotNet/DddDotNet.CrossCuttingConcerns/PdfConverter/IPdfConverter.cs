using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.CrossCuttingConcerns.PdfConverter
{
    public interface IPdfConverter
    {
        Stream Convert(string html);

        Task<Stream> ConvertAsync(string html);
    }
}
