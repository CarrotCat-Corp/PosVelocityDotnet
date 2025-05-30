using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.DevicePrint;

public sealed class PrintRequest
{
    [JsonPropertyName("text")]  public IEnumerable<string> ReceiptLines { get; private set; }

    public PrintRequest(IEnumerable<string> receiptLines)
    {
        ReceiptLines = receiptLines;
    }
}