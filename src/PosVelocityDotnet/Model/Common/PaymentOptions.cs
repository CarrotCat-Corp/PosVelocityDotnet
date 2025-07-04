using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.Common;

public sealed class PaymentOptions
{
    [JsonPropertyName("capture")] public bool Capture { get; private set; }
    [JsonPropertyName("allowCashback")] public bool AllowCashback { get; private set; }
    [JsonPropertyName("cardNotPresent")] public bool CardNotPresent { get; private set; }

    public PaymentOptions(bool capture, bool allowCashback = false, bool cardNotPresent = false)
    {
        Capture = capture;
        AllowCashback = allowCashback;
        CardNotPresent = cardNotPresent;
    }

    public static PaymentOptions Default => new(true, false, false);
}