using DddDotNet.CrossCuttingConcerns.PdfConverter;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;
using System.Threading.Tasks;

namespace DddDotNet.Infrastructure.PdfConverters.DinkToPdf
{
    public class DinkToPdfConverter : IPdfConverter
    {
        private readonly IConverter _converter;

        public DinkToPdfConverter(IConverter converter)
        {
            _converter = converter;
        }

        public Stream Convert(string html, PdfOptions pdfOptions = null)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4Plus,
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        PagesCount = true,
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 },
                    },
                },
            };

            byte[] pdf = _converter.Convert(doc);

            return new MemoryStream(pdf);
        }

        public Task<Stream> ConvertAsync(string html, PdfOptions pdfOptions = null)
        {
            return Task.FromResult(Convert(html, pdfOptions));
        }
    }
}
