using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.ThreeDs;

public sealed class CreateThreeDsRequest
{
    [JsonPropertyName("amount")] public decimal Amount { get; private set; }

    [JsonPropertyName("notes")] public string? Notes { get; private set; }

    [JsonPropertyName("reference")] public string? Reference { get; private set; }

    [JsonPropertyName("recipient")] public ThreeDsRequestRecipient Recipient { get; private set; }

    [JsonPropertyName("callback")] public Uri? Callback { get; private set; }

    public CreateThreeDsRequest(
        decimal amount,
        ThreeDsRequestRecipient recipient,
        Uri? callback = null,
        string? notes = null,
        string? reference = null
    )
    {
        Amount = amount;
        Notes = notes;
        Reference = reference;
        Recipient = recipient;
        Callback = callback;
    }
}