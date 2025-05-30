using System.Text.Json.Serialization;
using PosVelocityDotnet.JsonConverters;

namespace PosVelocityDotnet.Model.Common;

public class Issues
{
    [JsonPropertyName("duplicate")]      public Duplicate? Duplicate { get; set;  }
    [JsonPropertyName("offline")]        public Offline? Offline { get; set;  }
    [JsonPropertyName("partialPayment")] public PartialPayment? PartialPayment { get; set;  }
    [JsonPropertyName("signature")]      public IssuesSignature? Signature { get; set;  }
}

public class Duplicate
{
    [JsonPropertyName("description")] public string? Description { get; set; }
}

public class Offline
{
    [JsonPropertyName("description")] public string? Description { get; set; }
}

public class PartialPayment
{
    [JsonPropertyName("actualAmount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? ActualAmount { get; set; }

    [JsonPropertyName("requestedAmount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? RequestedAmount { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
}

public class IssuesSignature
{
    [JsonPropertyName("signature")] public SignatureSignature? Signature { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
}

public class SignatureSignature
{
    [JsonPropertyName("format")] public string? Format { get; set; }
    [JsonPropertyName("gzip")] public bool? Gzip { get; set; }
    [JsonPropertyName("response")] public string? Response { get; set; }
}