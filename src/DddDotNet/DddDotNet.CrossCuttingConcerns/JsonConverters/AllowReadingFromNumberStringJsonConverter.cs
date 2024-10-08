using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddDotNet.CrossCuttingConcerns.JsonConverters;

public class AllowReadingFromNumberStringJsonConverter : JsonConverter<string>
{
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetDecimal().ToString();
        }

        return reader.GetString();
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) => writer.WriteStringValue(value);
}
