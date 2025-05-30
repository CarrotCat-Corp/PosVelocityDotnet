using System.Text.Json.Serialization;
using PosVelocityDotnet.Model.Common;

namespace PosVelocityDotnet.Model.CardRead;

public sealed class CardReadResponse : IPosVelocityApiResponse
{
    [JsonPropertyName("encrypted")] public bool? Encrypted { get; set; }
    [JsonPropertyName("exp")] public string? Exp { get; set; }
    [JsonPropertyName("first6")] public string? First6 { get; set; }
    [JsonPropertyName("last4")] public string? Last4 { get; set; }
    [JsonPropertyName("track2")] public string? Track2 { get; set; }
    public bool IsValid => Track2 is not null;
}