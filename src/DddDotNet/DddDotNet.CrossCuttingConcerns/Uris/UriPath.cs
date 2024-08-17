using System.Linq;

namespace DddDotNet.CrossCuttingConcerns.Uris
{
    public static class UriPath
    {
        public static string Combine(params string[] paths)
        {
            return paths.Aggregate((current, path) => $"{current.TrimEnd('/')}/{path.TrimStart('/')}");
        }
    }
}
