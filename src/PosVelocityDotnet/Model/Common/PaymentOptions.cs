using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.Common;

public sealed class PaymentOptions
{
    [JsonPropertyName("capture")] public bool Capture { get; private set; }

    public PaymentOptions(bool capture)
    {
        Capture = capture;
    }

    public static PaymentOptions Default => new(true);
}