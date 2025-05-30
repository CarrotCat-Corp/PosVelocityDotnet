using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.Common;

public class CardTransactionExtraCard
{
    [JsonPropertyName("TransID")] public string? TransID { get; set; }
    [JsonPropertyName("ACI")] public string? Aci { get; set; }
    [JsonPropertyName("BanknetData")] public string? BanknetData { get; set; }
    [JsonPropertyName("DevTypeInd")] public string? DevTypeInd { get; set; }
}