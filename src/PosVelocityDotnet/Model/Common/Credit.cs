using System.Text.Json.Serialization;
using PosVelocityDotnet.JsonConverters;
using PosVelocityDotnet.Model.Device;
using PosVelocityDotnet.Utilities;

namespace PosVelocityDotnet.Model.Common;

public class Credit
{
    [JsonPropertyName("amount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? Amount { get; set; }


    // public decimal? Amount => ValueConverter.ToDecimalAmount(AmountInteger);

    [JsonPropertyName("cardTransaction")] public CardTransaction? CardTransaction { get; set; }

    [JsonPropertyName("createdTime")] public long? CreatedTimeStamp { get; set; }
    public DateTimeOffset? CreatedTime => ValueConverter.ToDateTimeOffset(CreatedTimeStamp);

    [JsonPropertyName("employee")] public Employee? Employee { get; set; }

    [JsonPropertyName("id")] public string? Id { get; set; }

    [JsonPropertyName("taxAmount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? TaxAmount { get; set; }

    // public decimal? TaxAmount => ValueConverter.ToDecimalAmount(TaxAmountInteger);

    [JsonPropertyName("device")] public CloverDevice? Device { get; set; }
}

