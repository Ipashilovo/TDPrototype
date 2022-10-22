///------------------------------------
/// menu generated class
///------------------------------------

using System;
using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;

namespace Entity
{
    public class UnitIdTypeConverter : TypeConverter
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
                return new UnitId((string) typedValue);
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(UnitId))
            {
                return new UnitId((string)value);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class UnitIdConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var id = (UnitId)value;
            writer.WriteValue(id.Value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;
            return new UnitId(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(UnitId);
        }
    }

    [TypeConverter(typeof(UnitIdTypeConverter))]
    [JsonConverter(typeof(UnitIdConverter))]
    public readonly struct UnitId : IEquatable<UnitId>
    {
        public readonly string Value;

        [JsonConstructor]
        public UnitId(string value)
        {
            Value = value;
        }

        public static bool operator ==(UnitId a, UnitId b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(UnitId a, UnitId b)
        {
            return !a.Equals(b);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(UnitId other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is UnitId other && Equals(other);
        }
    
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}