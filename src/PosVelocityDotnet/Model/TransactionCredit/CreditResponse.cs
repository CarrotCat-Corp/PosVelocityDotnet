using System.Text.Json.Serialization;
using PosVelocityDotnet.Model.Common;

namespace PosVelocityDotnet.Model.TransactionCredit;

public class CreditResponse : IPosVelocityApiResponse
{
    [JsonPropertyName("credit")] public Credit? Credit { get; set; }
    public bool IsValid => Credit is not null;
}