using System.Text.Json.Serialization;
using PosVelocityDotnet.Model.Common;

namespace PosVelocityDotnet.Model.TransactionPayment;

public class PaymentResponse : IPosVelocityApiResponse
{
    [JsonPropertyName("issues")] public Issues? Issues { get; set; }
    [JsonPropertyName("payment")] public Payment? Payment { get; set; }
    [JsonPropertyName("paymentId")] public string? PaymentId { get; set; }
    [JsonPropertyName("voidReason")] public string? VoidReason { get; set; }
    [JsonPropertyName("voidStatus")] public string? VoidStatus { get; set; }
    [JsonPropertyName("refund")] public Refund? Refund { get; set; }
    [JsonPropertyName("fullRefund")] public bool? FullRefund { get; set; }
    public bool IsValid => Payment is not null;
}
