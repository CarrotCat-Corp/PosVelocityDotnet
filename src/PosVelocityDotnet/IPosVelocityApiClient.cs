using PosVelocityDotnet.Model.CardRead;
using PosVelocityDotnet.Model.Common;
using PosVelocityDotnet.Model.Device;
using PosVelocityDotnet.Model.DeviceCustomerInput;
using PosVelocityDotnet.Model.DevicePrint;
using PosVelocityDotnet.Model.DeviceScreen;
using PosVelocityDotnet.Model.ThreeDs;
using PosVelocityDotnet.Model.TransactionCredit;
using PosVelocityDotnet.Model.TransactionPayment;

namespace PosVelocityDotnet;

public interface IPosVelocityApiClient
{
    [Obsolete("Not implemented due to API returning an error")]
    Task<PosVelocityResult<object>> GetPrintersAsync(PosVelocityAuthParameters auth);

    Task<PosVelocityResult<IEnumerable<CloverDevice>>> GetAccountDevicesAsync(PosVelocityAuthParameters auth);

    Task<PosVelocityResult<IEnumerable<Employee>>> GetAccountEmployeesAsync(PosVelocityAuthParameters auth);

    [Obsolete("Not implemented due to parameters start and end is not documented")]
    Task<PosVelocityResult<object>> GetAccountTransactionsAsync(PosVelocityAuthParameters auth, DateTimeOffset start, DateTimeOffset end);


    Task<PosVelocityResult<object>> PingTerminalAsync(PosVelocityAuthParameters auth, PosVelocityDeviceTarget targetDevice, string? pos = null);

    Task<PosVelocityResult<CardReadResponse>> ReadCardAsync(PosVelocityAuthParameters auth, PosVelocityDeviceTarget targetDevice, int? timeout = 60);

    Task<PosVelocityResult<object>> SetScreenMessageAsync(PosVelocityAuthParameters auth, PosVelocityDeviceTarget targetDevice, DisplayScreenMessageRequest request, string? pos = null);

    Task<PosVelocityResult<object>> PrintTextAsync(PosVelocityAuthParameters auth, PosVelocityDeviceTarget targetDevice, PrintRequest request, string? pos = null);

    Task<PosVelocityResult<object>> CancelPaymentRequestAsync(PosVelocityAuthParameters auth, PosVelocityDeviceTarget targetDevice, int? timeout = 60);


    Task<PosVelocityResult<CreditResponse>> ProcessCreditTransactionAsync(PosVelocityAuthParameters auth, PosVelocityDeviceTarget targetDevice, CreditRequest request, string? pos = null, int? timeout = 60);

    Task<PosVelocityResult<PaymentResponse>> ProcessPaymentTransactionAsync(PosVelocityAuthParameters auth, PosVelocityDeviceTarget targetDevice, PaymentTransactionRequest request, string? pos = null, int? timeout = 60);

    Task<PosVelocityResult<PaymentResponse>> FetchPaymentDetailsAsync(PosVelocityAuthParameters auth, PosVelocityDeviceTarget targetDevice, string paymentId, string? pos = null);

    Task<PosVelocityResult<PaymentResponse>> ProcessVoidTransactionAsync(PosVelocityAuthParameters auth, PosVelocityDeviceTarget targetDevice, string paymentId, VoidTransactionRequest request, string? pos = null);

    Task<PosVelocityResult<PaymentResponse>> ProcessRefundTransactionAsync(PosVelocityAuthParameters auth, PosVelocityDeviceTarget targetDevice, string paymentId, RefundTransactionRequest request, string? pos = null, int? timeout = 60);

    [Obsolete("GetPrintersAsync is not yet implemented due to API returning a webpage")]
    Task<PosVelocityResult<object>> GetPaymentReceiptAsync(PosVelocityAuthParameters auth, PosVelocityDeviceTarget targetDevice, string paymentId, string? pos = null);

    [Obsolete("GetCustomerInputAsync is not yet implemented due to API returning a webpage")]
    Task<PosVelocityResult<object>> GetCustomerInputAsync(PosVelocityAuthParameters auth, PosVelocityDeviceTarget targetDevice, CustomerInputRequest request, string? pos = null);


    Task<PosVelocityResult<ThreeDsResponse>> RetrieveThreeDsRequestAsync(PosVelocityAuthParameters auth, RetrieveThreeDsQuery request);

    Task<PosVelocityResult<ThreeDsResponse>> CreateAndSendThreeDsRequestAsync(PosVelocityAuthParameters auth, CreateThreeDsRequest request);
}
