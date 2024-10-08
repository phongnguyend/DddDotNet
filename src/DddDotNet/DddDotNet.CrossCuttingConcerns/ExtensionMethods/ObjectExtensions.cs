﻿using DddDotNet.CrossCuttingConcerns.JsonConverters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DddDotNet.CrossCuttingConcerns.ExtensionMethods;

public static class ObjectExtensions
{
    public static string AsJsonString(this object obj)
    {
        var content = JsonSerializer.Serialize(obj, new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
        });
        return content;
    }

    public static T TrimText<T>(this T obj)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
        };

        options.Converters.Add(new TrimmingStringJsonConverter());

        var json = JsonSerializer.Serialize(obj, options);

        return JsonSerializer.Deserialize<T>(json, options);
    }
}
