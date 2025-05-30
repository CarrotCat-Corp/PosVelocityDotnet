using System.Text.Json.Serialization;
using PosVelocityDotnet.Model.Common;

namespace PosVelocityDotnet.Model.ThreeDs;

public sealed class ThreeDsResponse : IPosVelocityApiResponse
{
    [JsonPropertyName("id")] public string? Id { get; set; }

    [JsonPropertyName("date")] public DateTimeOffset? Date { get; set; }

    [JsonPropertyName("amount")]
    public decimal? Amount { get; set; }

    [JsonPropertyName("notes")] public string? Notes { get; set; }

    [JsonPropertyName("email")] public string? Email { get; set; }

    [JsonPropertyName("phone")] public string? Phone { get; set; }

    [JsonPropertyName("reference")] public string? Reference { get; set; }

    [JsonPropertyName("status")] public string? Status { get; set; }

    [JsonPropertyName("url")] public string? Url { get; set; }

    [JsonPropertyName("callback")] public string? Callback { get; set; }

    public bool IsValid => Id is not null;
}