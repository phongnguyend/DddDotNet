using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.CrossCuttingConcerns.PdfConverter
{
    public interface IPdfConverter
    {
        Stream Convert(string html, PdfOptions pdfOptions = null);

        Task<Stream> ConvertAsync(string html, PdfOptions pdfOptions = null);
    }

    public class PdfOptions
    {

    }
}
