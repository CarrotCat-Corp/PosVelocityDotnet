using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.Common;

public class Order
{
    [JsonPropertyName("id")] public string? Id { get; set; }
}