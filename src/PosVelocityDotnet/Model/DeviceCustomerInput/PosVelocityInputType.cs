using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.DeviceCustomerInput;

public class PosVelocityInputType
{
    [JsonPropertyName("type")]
    public string Type { get; private set; }

    public PosVelocityInputType()
    {
    }

    private PosVelocityInputType(string type) => Type = type.ToUpper();

    public static PosVelocityInputType FromString(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"Invalid PosVelocity customer input type: '{value}'");

        return new PosVelocityInputType(value);
    }

    public static bool TryParse(string? value, out PosVelocityInputType type)
    {
        if (IsValidValue(value))
        {
            type = new PosVelocityInputType(value!);
            return true;
        }

        type = Default;
        return false;
    }

    public static PosVelocityInputType FromStringOrDefault(string? value) =>
        IsValidValue(value)
            ? new PosVelocityInputType(value!)
            : Default;

    private static bool IsValidValue(string? value) => ValidValues.Any(x => x == value);
    
    private static readonly IReadOnlyCollection<string> ValidValues =
    [
        "TEXT", "PHONE", "AMOUNT", "SIGNATURE"
    ];

    public static readonly PosVelocityInputType Default = new("TEXT");
    public static readonly PosVelocityInputType Text = new("TEXT");
    public static readonly PosVelocityInputType Phone = new("PHONE");
    public static readonly PosVelocityInputType Amount = new("AMOUNT");
    public static readonly PosVelocityInputType Signature = new("SIGNATURE");

    public override string ToString() => Type;

}