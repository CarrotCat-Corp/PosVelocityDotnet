using System.Text.Json;
using System.Text.Json.Serialization;
using PosVelocityDotnet.Utilities;

namespace PosVelocityDotnet.JsonConverters;

/// <summary>
/// Converts between decimal values (for API) and long integer values (for JSON serialization)
/// Works with both decimal and decimal? properties
/// </summary>
public class AmountConverter : JsonConverter<object>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(decimal) || typeToConvert == typeof(decimal?);
    }

    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Handle null case for nullable decimal
        if (reader.TokenType == JsonTokenType.Null)
        {
            if (typeToConvert == typeof(decimal?))
                return null;

            throw new JsonException($"Cannot convert null value to {typeToConvert}");
        }

        // Read the integer value from JSON
        if (reader.TokenType == JsonTokenType.Number)
        {
            long value = reader.GetInt64();
            decimal? decimalValue = ValueConverter.ToDecimalAmount(value);

            // Return appropriate type (decimal or decimal?)
            if (typeToConvert == typeof(decimal?))
                return decimalValue;
            else if (typeToConvert == typeof(decimal))
                return decimalValue ?? 0m;
        }

        throw new JsonException($"Expected number or null, got {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        decimal decimalValue = (decimal)value;
        long intAmount = ValueConverter.ToIntAmount(decimalValue);
        writer.WriteNumberValue(intAmount);
    }
}

