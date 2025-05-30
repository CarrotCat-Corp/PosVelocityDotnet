using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.DeviceCustomerInput;

public sealed class InputScreen
{
    [JsonPropertyName("text")] public string Text { get; private set; }
    [JsonPropertyName("type")] public PosVelocityInputType Type { get; private set; }

    public InputScreen(string text, PosVelocityInputType type)
    {
        Text = text;
        Type = type;
    }
}