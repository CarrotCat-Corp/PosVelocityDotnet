using System.Text.Json.Serialization;
using PosVelocityDotnet.Model.Common;

namespace PosVelocityDotnet.Model.Error;

public sealed class TransactionError : IPosVelocityApiResponse
{
    [JsonPropertyName("code")] public string? Code { get; set; }
    [JsonPropertyName("message")] public string? Message { get; set; }
    [JsonPropertyName("requestId")] public string? RequestId { get; set; }
    [JsonPropertyName("requestType")] public string? RequestType { get; set; }
    [JsonPropertyName("type")] public string? Type { get; set; }

    /// <summary>
    /// Determines whether the object is valid based on its properties.
    /// </summary>
    /// <remarks>
    /// The validation is typically implemented by checking whether certain key properties
    /// of the object have non-null values.
    /// </remarks>
    /// <value>
    /// Returns true if the object is considered valid; otherwise, false.
    /// </value>
    public bool IsValid => Message is not null;
}