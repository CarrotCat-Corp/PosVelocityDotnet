using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.ThreeDs;

public sealed record PosVelocityThreeDsRequestExpandType
{
    [JsonPropertyName("type")]
    public string Type { get; private set; }

    public PosVelocityThreeDsRequestExpandType()
    {
    }

    private PosVelocityThreeDsRequestExpandType(string type) => Type = type.ToLower();

    public static PosVelocityThreeDsRequestExpandType FromString(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"Invalid PosVelocity 3DS request expand type: '{value}'");

        return new PosVelocityThreeDsRequestExpandType(value);
    }

    public static bool TryParse(string? value, out PosVelocityThreeDsRequestExpandType? type)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            type = null;
            return false;
        }

        type = new PosVelocityThreeDsRequestExpandType(value);
        return true;
    }

    public static PosVelocityThreeDsRequestExpandType FromStringOrDefault(string? value) =>
        string.IsNullOrWhiteSpace(value)
            ? Default
            : new PosVelocityThreeDsRequestExpandType(value);


    public static readonly PosVelocityThreeDsRequestExpandType Default = new("response");
    public static readonly PosVelocityThreeDsRequestExpandType Response = new("response");
    public static readonly PosVelocityThreeDsRequestExpandType Transaction = new("transaction");


    public override string ToString()
    {
        return Type;
    }

    public string ToDisplayString() => Type switch
    {
        "response" => "Response",
        "transaction" => "Transaction",
        _ => "Unknown"
    };

    public static readonly IReadOnlyCollection<PosVelocityThreeDsRequestExpandType> All = new[]
    {
        Response,
        Transaction
    };
}