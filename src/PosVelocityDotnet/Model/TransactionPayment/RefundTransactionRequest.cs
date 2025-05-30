using System.Text.Json.Serialization;
using PosVelocityDotnet.JsonConverters;

namespace PosVelocityDotnet.Model.TransactionPayment;

public sealed class RefundTransactionRequest
{
    [JsonPropertyName("amount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal Amount { get; private set; }

    public RefundTransactionRequest(decimal amount)
    {
        Amount = amount;
    }
}