using System.Text.Json.Serialization;
using PosVelocityDotnet.JsonConverters;
using PosVelocityDotnet.Model.Device;
using PosVelocityDotnet.Utilities;

namespace PosVelocityDotnet.Model.Common;

public class Refund
{
    [JsonPropertyName("amount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? Amount { get; set; }
    // public decimal? Amount => ValueConverter.ToDecimalAmount(AmountInteger);

    [JsonPropertyName("createdTime")] public long? CreatedTimeStamp { get; set; }
    public DateTimeOffset? CreatedTime => ValueConverter.ToDateTimeOffset(CreatedTimeStamp);

    [JsonPropertyName("device")] public CloverDevice? Device { get; set; }

    [JsonPropertyName("employee")] public Employee? Employee { get; set; }

    [JsonPropertyName("id")] public string? Id { get; set; }

    [JsonPropertyName("payment")] public Payment? Payment { get; set; }

    [JsonPropertyName("taxAmount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? TaxAmount { get; set; }
    // public decimal? TaxAmount => ValueConverter.ToDecimalAmount(TaxAmountInteger);

    [JsonPropertyName("voided")] public bool? Voided { get; set; }


    [JsonPropertyName("additionalCharges")]
    public AdditionalCharge[]? AdditionalCharges { get; set; }

    [JsonPropertyName("cardTransaction")] public CardTransaction? CardTransaction { get; set; }

    [JsonPropertyName("externalReferenceId")]
    public string? ExternalReferenceId { get; set; }

    [JsonPropertyName("orderRef")] public OrderRef? OrderRef { get; set; }

    [JsonPropertyName("tipAmount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? TipAmount { get; set; }

    [JsonPropertyName("voidReason")] public string? VoidReason { get; set; }
}

public class OrderRef
{
    [JsonPropertyName("id")] public string? Id { get; set; }
}