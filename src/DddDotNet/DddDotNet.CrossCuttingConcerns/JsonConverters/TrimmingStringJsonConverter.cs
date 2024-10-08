using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddDotNet.CrossCuttingConcerns.JsonConverters;

public class TrimmingStringJsonConverter : JsonConverter<string>
{
    public override string Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) => reader.GetString()?.Trim();

    public override void Write(
            Utf8JsonWriter writer,
            string value,
            JsonSerializerOptions options) => writer.WriteStringValue(value?.Trim());
}
