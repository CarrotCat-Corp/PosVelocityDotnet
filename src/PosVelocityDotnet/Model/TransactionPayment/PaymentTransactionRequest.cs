using System.Text.Json.Serialization;
using PosVelocityDotnet.JsonConverters;
using PosVelocityDotnet.Model.Common;

namespace PosVelocityDotnet.Model.TransactionPayment;

public sealed class PaymentTransactionRequest
{
    [JsonPropertyName("final")] public bool? Final { get; private set; }

    [JsonPropertyName("amount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal Amount { get; private set; }

    [JsonPropertyName("external")] public string? External { get; private set; }
    [JsonPropertyName("options")] public PaymentOptions? Options { get; private set; }

    /// <summary>
    /// Represents a request to process a payment transaction.
    /// This request contains information about the transaction amount, whether it is final,
    /// any external reference, and optional additional payment options.
    /// <br/>
    /// NOTE: Since there is no transaction adjustment mechanism implemented by API itself,
    /// this constructor defaults to a final=true and to options.Capture=true.
    /// </summary>
    public PaymentTransactionRequest(
        decimal amount,
        PaymentOptions? options,
        bool? final = true,
        string? external = null
    )
    {
        // Amount = Amount = ValueConverter.ToIntAmount(amount);
        Amount = amount;
        Options = options ?? PaymentOptions.Default;
        Final = final;
        External = external;
    }
}