using System.Text.Json.Serialization;
using PosVelocityDotnet.JsonConverters;

namespace PosVelocityDotnet.Model.TransactionCredit;

public sealed class CreditRequest
{
    [JsonPropertyName("amount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal Amount { get; private set; }

    public CreditRequest(decimal amount)
    {
        // if (amount <= 0) throw new ArgumentException("Amount must be greater than 0", nameof(amount));
        // Amount = ValueConverter.ToIntAmount(amount);
        Amount = amount;
    }
}