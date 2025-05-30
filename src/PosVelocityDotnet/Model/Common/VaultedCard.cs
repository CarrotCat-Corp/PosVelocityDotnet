using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.Common;

public class VaultedCard
{
    [JsonPropertyName("expirationDate")] public string? ExpirationDate { get; set; }

    [JsonPropertyName("first6")] public string? First6 { get; set; }

    [JsonPropertyName("last4")] public string? Last4 { get; set; }
    
    [JsonPropertyName("token")] public string? Token { get; set; }

    [JsonPropertyName("cardholderName")] public string? CardholderName { get; set; }
}