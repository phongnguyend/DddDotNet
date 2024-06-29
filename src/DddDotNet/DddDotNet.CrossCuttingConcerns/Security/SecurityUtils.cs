using System.IO;
using System.Linq;

namespace DddDotNet.CrossCuttingConcerns.Security
{
    public static class SecurityUtils
    {
        private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();

        private static string ReplaceCRLF(this string text, string replacementCR, string replacementLF)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            return text.Replace("\r", replacementCR).Replace("\n", replacementLF);
        }

        public static string ReplaceCRLF(this string text)
        {
            return ReplaceCRLF(text, " ", " ");
        }

        public static string RemoveCRLF(this string text)
        {
            return ReplaceCRLF(text, string.Empty, string.Empty);
        }

        public static string EnsureValidDirectoryName(this string name)
        {
            return EnsureValidFileName(name);
        }

        public static string EnsureValidFileName(this string name)
        {
            if (name.Any(c => InvalidFileNameChars.Contains(c)))
            {
                throw new IOException("Invalid Characters");
            }

            return name;
        }
    }
}
