///------------------------------------
/// menu generated class
///------------------------------------
using System;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Globalization;

public class TimeTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(float))
        {
            return true;
        }
        return base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is float typedValue)
            return new Time((float) typedValue);
        return base.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(Time))
        {
            return new Time((float)value);
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}

public class TimeConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var id = (Time)value;
        writer.WriteValue(id.Value);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var value = (float)reader.Value;
        return new Time(value);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Time);
    }
}

[TypeConverter(typeof(TimeTypeConverter))]
[JsonConverter(typeof(TimeConverter))]
public readonly struct Time : IEquatable<Time>
{
    public readonly float Value;

    public static Time Zero => new Time(0);

    [JsonConstructor]
    public Time(float value)
    {
        Value = value;
    }

    public static bool operator ==(Time a, Time b)
    {
        return a.Equals(b);
    }
    
    public static Time operator +(Time a, Time b)
    {
        return new Time(a.Value + b.Value);
    }
    
    public static bool operator >(Time a, Time b)
    {
        return a.Value > b.Value;
    }

    public static bool operator <(Time a, Time b)
    {
        return a.Value < b.Value;
    }
    
    public static bool operator <=(Time a, Time b)
    {
        return a.Value <= b.Value;
    }

    public static bool operator >=(Time a, Time b)
    {
        return a.Value >= b.Value;
    }

    public static Time operator -(Time a, Time b)
    {
        return new Time(a.Value - b.Value);
    }

    public static bool operator !=(Time a, Time b)
    {
        return !a.Equals(b);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public bool Equals(Time other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object obj)
    {
        return obj is Time other && Equals(other);
    }
    
    public override string ToString()
    {
        return Value.ToString();
    }
}
