using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HeloEmail.Sdk
{
    /// <summary>
    /// Marks the enum member that stands for a value this version of the SDK doesn't know:
    /// one the API added after it was released.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class UnknownEnumValueAttribute : Attribute
    {
    }

    /// <summary>
    /// Reads and writes an enum as its kebab-case wire value. A value it doesn't know reads as
    /// the member marked <see cref="UnknownEnumValueAttribute"/> rather than failing, so an API
    /// that adds a value doesn't break older SDKs; that member can't be written back.
    /// </summary>
    public sealed class ApiEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
    {
        private static readonly Dictionary<string, TEnum> ByWireValue = new Dictionary<string, TEnum>();
        private static readonly Dictionary<TEnum, string> WireValues = new Dictionary<TEnum, string>();
        private static readonly TEnum? Unknown;

        static ApiEnumConverter()
        {
            foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var value = (TEnum)field.GetValue(null);
                if (field.GetCustomAttribute<UnknownEnumValueAttribute>() != null)
                {
                    Unknown = value;
                    continue;
                }

                var wire = JsonNamingPolicy.KebabCaseLower.ConvertName(field.Name);
                ByWireValue[wire] = value;
                WireValues[value] = wire;
            }
        }

        /// <summary>The value as the API spells it.</summary>
        /// <exception cref="ArgumentException">The value is the unknown member, which can't be sent.</exception>
        public static string WireValue(TEnum value)
        {
            string wire;
            if (WireValues.TryGetValue(value, out wire)) return wire;
            throw new ArgumentException(
                $"{typeof(TEnum).Name}.{value} stands for a value this SDK doesn't know, so it can't be sent.");
        }

        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var wire = reader.GetString();
            TEnum value;
            if (wire != null && ByWireValue.TryGetValue(wire, out value)) return value;
            if (Unknown != null) return Unknown.Value;
            throw new JsonException($"Unable to parse \"{wire}\" as {typeof(TEnum).Name}.");
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            try
            {
                writer.WriteStringValue(WireValue(value));
            }
            catch (ArgumentException ex)
            {
                throw new JsonException(ex.Message, ex);
            }
        }
    }

    /// <summary>
    /// A request with fields the API clears when sent as null. Leaving a property null leaves the
    /// field as it is (null properties aren't sent), so the ones to clear are named in
    /// <see cref="NullFields"/>, as the Go SDK's NullFields does.
    /// </summary>
    public interface IHasNullFields
    {
        /// <summary>Names of the properties to send as null, e.g. <c>nameof(Request.Text)</c>.</summary>
        List<string> NullFields { get; }

        /// <summary>The properties that can be cleared, by name, with their wire names.</summary>
        IReadOnlyDictionary<string, string> NullableFields { get; }
    }
}
