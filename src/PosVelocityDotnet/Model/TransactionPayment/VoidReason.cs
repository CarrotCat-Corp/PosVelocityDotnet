namespace PosVelocityDotnet.Model.TransactionPayment;

public class VoidReason
{
    public string Value { get; private set; }

    public VoidReason()
    {
    }

    private VoidReason(string type) => Value = type;

    public static VoidReason FromString(string? value)
    {
        if(!IsValidValue(value))
            throw new ArgumentException($"Invalid Void Reason: '{value}'");

        return new VoidReason(value);
    }

    public static VoidReason FromStringOrDefault(string? value)
    {
        return IsValidValue(value) ? new VoidReason(value!) : Default;
    }

    public static bool TryParse(string? value, out VoidReason? type)
    {
        if (IsValidValue(value))
        {
            type = null;
            return false;
        }

        type = new VoidReason(value);
        return true;
    }

    private static bool IsValidValue(string? value) => All.Any(x => x.Value == value);


    public static readonly VoidReason Default = new("USER_CANCEL");
    public static readonly VoidReason UserCancel = new("USER_CANCEL");
    public static readonly VoidReason TransportError = new("TRANSPORT_ERROR");
    public static readonly VoidReason RejectSignature = new("REJECT_SIGNATURE");
    public static readonly VoidReason RejectPartialAuth = new("REJECT_PARTIAL_AUTH");
    public static readonly VoidReason NotApproved = new("NOT_APPROVED");
    public static readonly VoidReason Failed = new("FAILED");
    public static readonly VoidReason AuthClosedNewCard = new("AUTH_CLOSED_NEW_CARD");
    public static readonly VoidReason DeveloperPayPartialAuth = new("DEVELOPER_PAY_PARTIAL_AUTH");
    public static readonly VoidReason RejectDuplicate = new("REJECT_DUPLICATE");
    public static readonly VoidReason RejectOffline = new("REJECT_OFFLINE");
    public static readonly VoidReason GiftCardLoadFailed = new("GIFTCARD_LOAD_FAILED");
    public static readonly VoidReason UserGiftCardLoadCancel = new("USER_GIFTCARD_LOAD_CANCEL");
    public static readonly VoidReason DeveloperPayTipAdjustFailed = new("DEVELOPER_PAY_TIP_ADJUST_FAILED");
    public static readonly VoidReason UserCustomerCancel = new("USER_CUSTOMER_CANCEL");
    public static readonly VoidReason Fraud = new("FRAUD");



    public override string ToString()
    {
        return Value;
    }

    public string ToDisplayString() => string.Join(" ",
        Value.Split('_')
            .Where(word => !string.IsNullOrEmpty(word))
            .Select<string, string>(word => word.Length > 0
                ? char.ToUpper(word[0]) + word.Substring(1).ToLower()
                : word));


    public static readonly IReadOnlyCollection<VoidReason> All = new[]
    {
        UserCancel,
        TransportError,
        RejectSignature,
        RejectPartialAuth,
        NotApproved,
        Failed,
        AuthClosedNewCard,
        DeveloperPayPartialAuth,
        RejectDuplicate,
        RejectOffline,
        GiftCardLoadFailed,
        UserGiftCardLoadCancel,
        DeveloperPayTipAdjustFailed,
        UserCustomerCancel,
        Fraud
    };
}