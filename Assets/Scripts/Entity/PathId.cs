///------------------------------------
/// menu generated class
///------------------------------------
using System;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Globalization;

public class PathIdTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(string))
        {
            return true;
        }
        return base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string typedValue)
            return new PathId((string) typedValue);
        return base.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(PathId))
        {
            return new PathId((string)value);
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}

public class PathIdConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var id = (PathId)value;
        writer.WriteValue(id.Value);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var value = (string)reader.Value;
        return new PathId(value);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(PathId);
    }
}

[TypeConverter(typeof(PathIdTypeConverter))]
[JsonConverter(typeof(PathIdConverter))]
public readonly struct PathId : IEquatable<PathId>
{
    public readonly string Value;

    [JsonConstructor]
    public PathId(string value)
    {
        Value = value;
    }

    public static bool operator ==(PathId a, PathId b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(PathId a, PathId b)
    {
        return !a.Equals(b);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public bool Equals(PathId other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        return obj is PathId other && Equals(other);
    }
    
    public override string ToString()
    {
        return Value.ToString();
    }
}
