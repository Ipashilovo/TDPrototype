///------------------------------------
/// menu generated class
///------------------------------------

using System;
using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;

namespace Entity
{
    public class LevelTypeConverter : TypeConverter
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
                return new Level((long) typedValue);
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(Level))
            {
                return new Level((long)value);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class LevelConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var id = (Level)value;
            writer.WriteValue(id.Value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = (long)reader.Value;
            return new Level(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Level);
        }
    }

    [TypeConverter(typeof(LevelTypeConverter))]
    [JsonConverter(typeof(LevelConverter))]
    public readonly struct Level : IEquatable<Level>
    {
        public readonly long Value;

        [JsonConstructor]
        public Level(long value)
        {
            Value = value;
        }

        public static bool operator ==(Level a, Level b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Level a, Level b)
        {
            return !a.Equals(b);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(Level other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Level other && Equals(other);
        }
    
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}