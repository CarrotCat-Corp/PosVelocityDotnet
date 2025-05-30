using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.TransactionPayment;

public sealed class VoidTransactionRequest
{
    [JsonPropertyName("reason")] public string Reason { get; private set; }

    public VoidTransactionRequest(VoidReason reason)
    {
        Reason = reason.Value;
    }
}