namespace DddDotNet.CrossCuttingConcerns.Security
{
    public static class SecurityUtils
    {
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
    }
}
