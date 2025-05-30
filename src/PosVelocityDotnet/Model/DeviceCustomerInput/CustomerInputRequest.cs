using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.DeviceCustomerInput;

public sealed class CustomerInputRequest
{
    [JsonPropertyName("screens")] public List<InputScreen> Screens { get; private set; }

    public CustomerInputRequest(List<InputScreen> screens)
    {
        Screens = screens;
    }
}