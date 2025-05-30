using System.Text.Json.Serialization;
using PosVelocityDotnet.JsonConverters;
using PosVelocityDotnet.Model.TransactionPayment;

namespace PosVelocityDotnet.Model.Common;

public class CardTransaction
{
    [JsonPropertyName("authCode")] public string? AuthCode { get; set; }
    [JsonPropertyName("cardType")] public string? CardType { get; set; }
    [JsonPropertyName("entryType")] public string? EntryType { get; set; }
    [JsonPropertyName("extra")] public CardTransactionExtra? Extra { get; set; }
    [JsonPropertyName("last4")] public string? Last4 { get; set; }
    [JsonPropertyName("referenceId")] public string? ReferenceId { get; set; }
    [JsonPropertyName("state")] public string? State { get; set; }
    [JsonPropertyName("type")] public string? Type { get; set; }
    [JsonPropertyName("first6")] public string? First6 { get; set; }
    [JsonPropertyName("token")] public string? Token { get; set; }
    [JsonPropertyName("transactionNo")] public string? TransactionNo { get; set; }
    [JsonPropertyName("vaultedCard")] public VaultedCard? VaultedCard { get; set; }
    [JsonPropertyName("captured")] public bool? Captured { get; set; }
    [JsonPropertyName("cardholderName")] public string? CardholderName { get; set; }
    [JsonPropertyName("currency")] public string? Currency { get; set; }

    [JsonPropertyName("avsResult")] public string? AvsResult { get; set; }

    [JsonPropertyName("begBalance")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? BegBalance { get; set; }

    [JsonPropertyName("endBalance")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? EndBalance { get; set; }
}