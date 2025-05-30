using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.Common;

public sealed class Extensions
{
    [JsonPropertyName("label")]
    public string? Label { get; set; }
}