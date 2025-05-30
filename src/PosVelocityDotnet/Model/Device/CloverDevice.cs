using System.Text.Json.Serialization;
using PosVelocityDotnet.Model.Common;

namespace PosVelocityDotnet.Model.Device;


public sealed class CloverDevice : IPosVelocityApiResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("model")]
    public string? Model { get; set; }

    [JsonPropertyName("terminalPrefix")]
    public long? TerminalPrefix { get; set; }

    [JsonPropertyName("serial")]
    public string? Serial { get; set; }

    [JsonPropertyName("secureId")]
    public string? SecureId { get; set; }

    [JsonPropertyName("buildType")]
    public string? BuildType { get; set; }

    [JsonPropertyName("productName")]
    public string? ProductName { get; set; }

    [JsonPropertyName("pinDisabled")]
    public bool? PinDisabled { get; set; }

    [JsonPropertyName("deviceTypeName")]
    public string? DeviceTypeName { get; set; }

    [JsonPropertyName("offlinePayments")]
    public bool? OfflinePayments { get; set; }

    [JsonPropertyName("offlinePaymentsAll")]
    public bool? OfflinePaymentsAll { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("extensions")]
    public Extensions? Extensions { get; set; }

    public bool IsValid => Id is not null && Serial is not null;
}