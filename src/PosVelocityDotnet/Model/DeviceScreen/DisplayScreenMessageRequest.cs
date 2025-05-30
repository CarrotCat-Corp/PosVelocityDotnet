using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.DeviceScreen;


public sealed class DisplayScreenMessageRequest
{
    [JsonPropertyName("screen")]  public string? Screen { get; private set; }
    [JsonPropertyName("message")] public string? Message { get; private set; }

    public DisplayScreenMessageRequest(string screen, string message)
    {
        Screen = screen;
        Message = message;
    }
}
