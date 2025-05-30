using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.Common;

public class CardTransactionExtraCommon
{
    [JsonPropertyName("LocalDateTime")] public string? LocalDateTime { get; set; }
    [JsonPropertyName("POSEntryMode")] public string? PosEntryMode { get; set; }
    [JsonPropertyName("POSID")] public string? Posid { get; set; }
    [JsonPropertyName("MerchCustom1")] public string? MerchCustom1 { get; set; }
    [JsonPropertyName("TermEntryCapablt")] public string? TermEntryCapablt { get; set; }
    [JsonPropertyName("CardCaptCap")] public string? CardCaptCap { get; set; }
    [JsonPropertyName("STAN")] public string? Stan { get; set; }
    [JsonPropertyName("POSCondCode")] public string? PosCondCode { get; set; }
    [JsonPropertyName("TermLocInd")] public string? TermLocInd { get; set; }
    [JsonPropertyName("PymtType")] public string? PymtType { get; set; }
    [JsonPropertyName("MerchID")] public string? MerchId { get; set; }
    [JsonPropertyName("TermID")] public string? TermId { get; set; }
    [JsonPropertyName("TrnmsnDateTime")] public string? TrnmsnDateTime { get; set; }
    [JsonPropertyName("TermCatCode")] public string? TermCatCode { get; set; }
}