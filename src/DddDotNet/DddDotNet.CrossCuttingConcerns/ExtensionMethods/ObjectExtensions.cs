using System.Text.Json;

namespace DddDotNet.CrossCuttingConcerns.ExtensionMethods
{
    public static class ObjectExtensions
    {
        public static string AsJsonString(this object obj)
        {
            var content = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
            return content;
        }
    }
}
