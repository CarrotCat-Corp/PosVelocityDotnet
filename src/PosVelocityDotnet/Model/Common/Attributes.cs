using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.Common;

public class Attributes
{
    [JsonPropertyName("source_type")] public string? SourceType { get; set; }

    [JsonPropertyName("is_quick_chip")] public string? IsQuickChip { get; set; }

    [JsonPropertyName("tip_source")] public string? TipSource { get; set; }

    [JsonPropertyName("payment_app_version")]
    public string? PaymentAppVersion { get; set; }

    [JsonPropertyName("create_auth")] public string? CreateAuth { get; set; }

    [JsonPropertyName("source_ip")] public string? SourceIp { get; set; }


}