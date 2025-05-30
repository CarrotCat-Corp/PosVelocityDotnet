using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.Common;

public class CardTransactionExtra
{
    [JsonPropertyName("iccContainer")] public string? IccContainer { get; set; }

    [JsonPropertyName("applicationLabel")] public string? ApplicationLabel { get; set; }

    [JsonPropertyName("authorizingNetworkName")]
    public string? AuthorizingNetworkName { get; set; }

    [JsonPropertyName("cvmResult")] public string? CvmResult { get; set; }

    [JsonPropertyName("applicationIdentifier")]
    public string? ApplicationIdentifier { get; set; }

    [JsonPropertyName("card")] public CardTransactionExtraCard? Card { get; set; }

    [JsonPropertyName("common")] public CardTransactionExtraCommon? Common { get; set; }

    [JsonPropertyName("func")] public string? Func { get; set; }

    [JsonPropertyName("athNtwkId")] public string? AthNtwkId { get; set; }

    [JsonPropertyName("exp")] public string? Exp { get; set; }

    [JsonPropertyName("tkntype")] public string? Tkntype { get; set; }

    [JsonPropertyName("additionalProp")] public string? AdditionalProp { get; set; }

}