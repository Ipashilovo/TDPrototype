///------------------------------------
/// menu generated class
///------------------------------------
using System;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Globalization;

public class AmountTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(long))
        {
            return true;
        }
        return base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is long typedValue)
            return new Amount((long) typedValue);
        return base.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(Amount))
        {
            return new Amount((long)value);
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}

public class AmountConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var id = (Amount)value;
        writer.WriteValue(id.Value);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var value = (long)reader.Value;
        return new Amount(value);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Amount);
    }
}

[TypeConverter(typeof(AmountTypeConverter))]
[JsonConverter(typeof(AmountConverter))]
public readonly struct Amount : IEquatable<Amount>
{
    public readonly long Value;

    public static Amount Zero => new Amount(0);

    [JsonConstructor]
    public Amount(long value)
    {
        Value = value;
    }

    public static bool operator ==(Amount a, Amount b)
    {
        return a.Equals(b);
    }
    
    public static bool operator <=(Amount a, Amount b)
    {
        return a.Value <= b.Value;
    }
    
    public static bool operator >=(Amount a, Amount b)
    {
        return a.Value >= b.Value;
    }
    
    public static bool operator >(Amount a, Amount b)
    {
        return a.Value > b.Value;
    }

    public static bool operator <(Amount a, Amount b)
    {
        return a.Value < b.Value;
    }
    
    public static Amount operator +(Amount a, Amount b)
    {
        return new Amount(a.Value + b.Value);
    }
    
    public static Amount operator -(Amount a, Amount b)
    {
        return new Amount(a.Value - b.Value);
    }

    public static bool operator !=(Amount a, Amount b)
    {
        return !a.Equals(b);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public bool Equals(Amount other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        return obj is Amount other && Equals(other);
    }
    
    public override string ToString()
    {
        return Value.ToString();
    }
}
