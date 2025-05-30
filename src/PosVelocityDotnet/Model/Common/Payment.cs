using System.Text.Json.Serialization;
using PosVelocityDotnet.JsonConverters;
using PosVelocityDotnet.Model.Device;
using PosVelocityDotnet.Utilities;

namespace PosVelocityDotnet.Model.Common;

public class Payment
{
    [JsonPropertyName("amount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? Amount { get; set; }
    // public decimal? Amount => ValueConverter.ToDecimalAmount(AmountInteger);

    [JsonPropertyName("attributes")] public Attributes? Attributes { get; set; }

    [JsonPropertyName("cardTransaction")] public CardTransaction? CardTransaction { get; set; }

    [JsonPropertyName("createdTime")] public long? CreatedTimeStamp { get; set; }
    public DateTimeOffset? CreatedTime => ValueConverter.ToDateTimeOffset(CreatedTimeStamp);

    [JsonPropertyName("employee")] public Employee? Employee { get; set; }

    [JsonPropertyName("externalPaymentId")]
    public string? ExternalPaymentId { get; set; }

    [JsonPropertyName("id")] public string? Id { get; set; }

    [JsonPropertyName("offline")] public bool? Offline { get; set; }

    [JsonPropertyName("order")] public Order? Order { get; set; }

    [JsonPropertyName("result")] public string? Result { get; set; }

    [JsonPropertyName("taxAmount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? TaxAmount { get; set; }
    // public decimal? TaxAmount => ValueConverter.ToDecimalAmount(TaxAmountInteger);

    [JsonPropertyName("tender")] public Tender? Tender { get; set; }

    [JsonPropertyName("tipAmount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? TipAmount { get; set; }
    // public decimal? TipAmount => ValueConverter.ToDecimalAmount(TipAmountInteger);

    [JsonPropertyName("cashbackAmount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal CashbackAmount { get; set; }
    // public decimal? CashbackAmount => ValueConverter.ToDecimalAmount(CashbackAmountInteger);

    [JsonPropertyName("clientCreatedTime")]
    public long? ClientCreatedTimeStamp { get; set; }

    public DateTimeOffset? ClientCreatedTime => ValueConverter.ToDateTimeOffset(ClientCreatedTimeStamp);

    [JsonPropertyName("device")] public CloverDevice? Device { get; set; }

    [JsonPropertyName("modifiedTime")] public long? ModifiedTimeStamp { get; set; }
    public DateTimeOffset? ModifiedTime => ValueConverter.ToDateTimeOffset(ModifiedTimeStamp);

    [JsonPropertyName("refunds")] public Refund[]? Refunds { get; set; }






    [JsonPropertyName("additionalCharges")]
    public AdditionalCharge[]? AdditionalCharges { get; set; }

    [JsonPropertyName("dccInfo")] public DccInfo? DccInfo { get; set; }

    [JsonPropertyName("externalReferenceId")]
    public string? ExternalReferenceId { get; set; }

    [JsonPropertyName("note")] public string? Note { get; set; }

    [JsonPropertyName("voidPaymentRef")] public VoidPaymentRef? VoidPaymentRef { get; set; }
    [JsonPropertyName("voidReason")] public string? VoidReason { get; set; }
}

public class VoidPaymentRef
{
    [JsonPropertyName("id")] public string? Id { get; set; }
}

public class AdditionalCharge
{
    [JsonPropertyName("amount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? Amount { get; set; }

    [JsonPropertyName("id")] public string? Id { get; set; }

    [JsonPropertyName("type")] public string? Type { get; set; }
}

public class DccInfo
{
    [JsonPropertyName("baseAmount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? BaseAmount { get; set; }

    [JsonPropertyName("baseCurrencyCode")] public string? BaseCurrencyCode { get; set; }
    [JsonPropertyName("dccApplied")] public bool? DccApplied { get; set; }
    [JsonPropertyName("dccEligible")] public bool? DccEligible { get; set; }

    [JsonPropertyName("exchangeRate")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? ExchangeRate { get; set; }

    [JsonPropertyName("exchangeRateId")] public string? ExchangeRateId { get; set; }

    [JsonPropertyName("exchangeRateSourceName")]
    public string? ExchangeRateSourceName { get; set; }

    [JsonPropertyName("exchangeRateSourceTimeStamp")]
    public string? ExchangeRateSourceTimeStamp { get; set; }

    [JsonPropertyName("foreignAmount")]
    [JsonConverter(typeof(AmountConverter))]
    public decimal? ForeignAmount { get; set; }

    [JsonPropertyName("foreignCurrencyCode")]
    public string? ForeignCurrencyCode { get; set; }

    [JsonPropertyName("inquiryRateId")] public long? InquiryRateId { get; set; }

    [JsonPropertyName("marginRatePercentage")]
    public string? MarginRatePercentage { get; set; }

    [JsonPropertyName("rateRequestId")] public string? RateRequestId { get; set; }
}
