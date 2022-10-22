///------------------------------------
/// menu generated class
///------------------------------------
using System;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Globalization;

public class UnitTypeTypeConverter : TypeConverter
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
            return new UnitType((string) typedValue);
        return base.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(UnitType))
        {
            return new UnitType((string)value);
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}

public class UnitTypeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var id = (UnitType)value;
        writer.WriteValue(id.Value);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var value = (string)reader.Value;
        return new UnitType(value);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(UnitType);
    }
}

[TypeConverter(typeof(UnitTypeTypeConverter))]
[JsonConverter(typeof(UnitTypeConverter))]
public readonly struct UnitType : IEquatable<UnitType>
{
    public readonly string Value;

    [JsonConstructor]
    public UnitType(string value)
    {
        Value = value;
    }

    public static bool operator ==(UnitType a, UnitType b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(UnitType a, UnitType b)
    {
        return !a.Equals(b);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public bool Equals(UnitType other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        return obj is UnitType other && Equals(other);
    }
    
    public override string ToString()
    {
        return Value.ToString();
    }
}
