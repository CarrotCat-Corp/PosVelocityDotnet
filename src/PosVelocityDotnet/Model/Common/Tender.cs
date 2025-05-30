using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.Common;

public class Tender
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("label")] public string? Label { get; set; }
    [JsonPropertyName("labelKey")] public string? LabelKey { get; set; }
    [JsonPropertyName("opensCashDrawer")] public bool? OpensCashDrawer { get; set; }
}