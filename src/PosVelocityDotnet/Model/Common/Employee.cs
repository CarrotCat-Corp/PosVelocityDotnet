using System.Text.Json.Serialization;

namespace PosVelocityDotnet.Model.Common;

public sealed class Employee : IPosVelocityApiResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("nickname")]
    public string? Nickname { get; set; }

    // The following is removed to avoid exposing user's pin to the clients.
    // It can be added later should the necessity arise.
    // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    // [JsonPropertyName("pin")]
    // public string? Pin { get; set; }

    [JsonPropertyName("role")]
    public string? Role { get; set; }

    [JsonPropertyName("isOwner")]
    public bool? IsOwner { get; set; }

    public bool IsValid => Id is not null;
}