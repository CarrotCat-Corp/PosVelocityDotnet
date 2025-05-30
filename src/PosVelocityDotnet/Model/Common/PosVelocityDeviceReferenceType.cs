
namespace PosVelocityDotnet.Model.Common;

/// <summary>
/// Represents a device reference type used within the context of POS Velocity operations for identifying or interacting with devices.
/// </summary>
/// <remarks>
/// This type provides various predefined device reference types, such as "serial" or "label,"
/// to standardize the way devices are referenced in semi-integration client requests.
/// </remarks>
public sealed record PosVelocityDeviceReferenceType
{
    public string Type { get; private set; }

    public PosVelocityDeviceReferenceType()
    {
    }

    private PosVelocityDeviceReferenceType(string type) => Type = type.ToLower();

    public static PosVelocityDeviceReferenceType FromString(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"Invalid PosVelocity terminal reference type: '{value}'");

        return new PosVelocityDeviceReferenceType(value);
    }

    public static bool TryParse(string? value, out PosVelocityDeviceReferenceType? type)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            type = null;
            return false;
        }

        type = new PosVelocityDeviceReferenceType(value);
        return true;
    }

    public static PosVelocityDeviceReferenceType FromStringOrDefault(string? value) =>
        string.IsNullOrWhiteSpace(value)
            ? Default
            : new PosVelocityDeviceReferenceType(value);


    public static readonly PosVelocityDeviceReferenceType Default = new("serial");
    public static readonly PosVelocityDeviceReferenceType Serial = new("serial");
    public static readonly PosVelocityDeviceReferenceType Label = new("label");


    public override string ToString()
    {
        return Type;
    }

    public string ToDisplayString() => Type switch
    {
        "serial" => "Serial Number",
        "label" => "Label",
        _ => "Unknown"
    };

    public static readonly IReadOnlyCollection<PosVelocityDeviceReferenceType> All = new[]
    {
        Serial,
        Label
    };
}