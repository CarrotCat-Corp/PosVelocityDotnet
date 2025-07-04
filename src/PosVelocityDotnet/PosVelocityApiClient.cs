using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using PosVelocityDotnet.Model.CardRead;
using PosVelocityDotnet.Model.Common;
using PosVelocityDotnet.Model.Device;
using PosVelocityDotnet.Model.DeviceCustomerInput;
using PosVelocityDotnet.Model.DevicePrint;
using PosVelocityDotnet.Model.DeviceScreen;
using PosVelocityDotnet.Model.ThreeDs;
using PosVelocityDotnet.Model.TransactionCredit;
using PosVelocityDotnet.Model.TransactionPayment;
using PosVelocityDotnet.Utilities;

namespace PosVelocityDotnet;

public sealed class PosVelocityApiClient : IPosVelocityApiClient, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly bool _shouldDisposeClient;
    private bool _disposed;

    // Constructor for dependency injection and production(preferred)
    /// <summary>
    /// Represents a client for interacting with the POS Velocity API.
    /// </summary>
    /// <remarks>
    /// This client allows integration with the POS Velocity API for operations such as managing devices,
    /// employees, transactions, credit processing, payment processing, and terminal interactions.
    /// It implements IDisposable for proper resource management.
    /// </remarks>
    public PosVelocityApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _shouldDisposeClient = false;
        ConfigureClient();
    }

    // Constructor for manual creation (fallback and tests)
    /// <summary>
    /// Represents a client for interacting with the POS Velocity API.
    /// </summary>
    /// <remarks>
    /// This client allows integration with the POS Velocity API for operations such as managing devices,
    /// employees, transactions, credit processing, payment processing, and terminal interactions.
    /// It implements IDisposable for proper resource management.
    /// </remarks>
    public PosVelocityApiClient(string hostAddress)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(hostAddress)
        };
        _shouldDisposeClient = true;
        ConfigureClient();
    }

    private void ConfigureClient()
    {
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        // Set timeout (default is 100 seconds)
        // _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing && _shouldDisposeClient)
        {
            _httpClient.Dispose();
        }

        _disposed = true;
    }


    [Obsolete("Not implemented due to API returning an error")]
    public Task<PosVelocityResult<object>> GetPrintersAsync(
        PosVelocityAuthParameters auth
    )
    {
        throw new NotImplementedException(
            "GetPrintersAsync is not yet implemented due to API returning an error. It will be implemented in future releases.");
    }

    public async Task<PosVelocityResult<IEnumerable<CloverDevice>>> GetAccountDevicesAsync(
        PosVelocityAuthParameters auth
    )
    {
        const string resourceUrl = "/api/v2/integrations/account/devices";

        var uri = new PosVelocityUriBuilder(resourceUrl, auth)
            .Build();

        var httpResult = await _httpClient.GetAsync(uri);

        return await ResponseProcessor.ProcessCollectionHttpResponseMessageAsync<IEnumerable<CloverDevice>>(httpResult);
    }

    public async Task<PosVelocityResult<IEnumerable<Employee>>> GetAccountEmployeesAsync(
        PosVelocityAuthParameters auth
    )
    {
        const string resourceUrl = "/api/v2/integrations/account/employees";

        var uri = new PosVelocityUriBuilder(resourceUrl, auth)
            .Build();

        var httpResult = await _httpClient.GetAsync(uri);

        return await ResponseProcessor.ProcessCollectionHttpResponseMessageAsync<IEnumerable<Employee>>(httpResult);
    }


    [Obsolete("Not implemented due to parameters start and end is not documented")]
    public Task<PosVelocityResult<object>> GetAccountTransactionsAsync(
        PosVelocityAuthParameters auth,
        DateTimeOffset start,
        DateTimeOffset end
    )
    {
        throw new NotImplementedException(
            "GetAccountTransactionsAsync is not yet implemented due incomplete API documentation. It will be implemented in future releases.");
    }


    public async Task<PosVelocityResult<object>> PingTerminalAsync(
        PosVelocityAuthParameters auth,
        PosVelocityDeviceTarget targetDevice,
        string? pos = null
    )
    {
        const string resourceUrl = "/api/v2/integrations/terminal/ping";

        var uri = new PosVelocityUriBuilder(resourceUrl, auth)
            .AddDeviceTarget(targetDevice)
            .Build();

        var httpResult = await _httpClient.GetAsync(uri);

        return await ResponseProcessor.ProcessHttpResponseMessageAsync(httpResult);
    }

    public async Task<PosVelocityResult<CardReadResponse>> ReadCardAsync(
        PosVelocityAuthParameters auth,
        PosVelocityDeviceTarget targetDevice,
        int? timeout = 60
    )
    {
        const string resourceUrl = "/api/v2/integrations/terminal/card";

        var uri = new PosVelocityUriBuilder(resourceUrl, auth)
            .AddDeviceTarget(targetDevice)
            .AddTimeout(timeout)
            .Build();

        var httpResult = await _httpClient.GetAsync(uri);

        return await ResponseProcessor.ProcessHttpResponseMessageAsync<CardReadResponse>(httpResult);
    }

    public async Task<PosVelocityResult<object>> SetScreenMessageAsync(
        PosVelocityAuthParameters auth,
        PosVelocityDeviceTarget targetDevice,
        DisplayScreenMessageRequest request,
        string? pos = null
    )
    {
        const string resourceUrl = "/api/v2/integrations/terminal/screen";

        var uri = new PosVelocityUriBuilder(resourceUrl, auth)
            .AddDeviceTarget(targetDevice)
            .AddPosId(pos)
            .Build();

        var httpResult = await _httpClient.PutAsync(uri,
            new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        return await ResponseProcessor.ProcessHttpResponseMessageAsync(httpResult);
    }

    public async Task<PosVelocityResult<object>> PrintTextAsync(
        PosVelocityAuthParameters auth,
        PosVelocityDeviceTarget targetDevice,
        PrintRequest request,
        string? pos = null
    )
    {
        const string resourceUrl = "/api/v2/integrations/terminal/print";

        var uri = new PosVelocityUriBuilder(resourceUrl, auth)
            .AddDeviceTarget(targetDevice)
            .AddPosId(pos)
            .Build();

        var httpResult = await _httpClient.PostAsync(uri,
            new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        return await ResponseProcessor.ProcessHttpResponseMessageAsync(httpResult);
    }

    public async Task<PosVelocityResult<object>> CancelPaymentRequestAsync(
        PosVelocityAuthParameters auth,
        PosVelocityDeviceTarget targetDevice,
        int? timeout = 60
    )
    {
        const string resourceUrl = "/api/v2/integrations/terminal/cancel";

        var uri = new PosVelocityUriBuilder(resourceUrl, auth)
            .AddDeviceTarget(targetDevice)
            .AddTimeout(timeout)
            .Build();

        var httpResult = await _httpClient.PutAsync(uri, new StringContent(string.Empty, Encoding.UTF8, "text/plain"));

        return await ResponseProcessor.ProcessHttpResponseMessageAsync(httpResult);
    }

    public async Task<PosVelocityResult<CreditResponse>> ProcessCreditTransactionAsync(
        PosVelocityAuthParameters auth,
        PosVelocityDeviceTarget targetDevice,
        CreditRequest request,
        string? pos = null,
        int? timeout = 60
    )
    {
        const string resourceUrl = "/api/v2/integrations/terminal/credit";

        var uri = new PosVelocityUriBuilder(resourceUrl, auth)
            .AddDeviceTarget(targetDevice)
            .AddPosId(pos)
            .AddTimeout(timeout)
            .Build();

        var httpResult = await _httpClient.PostAsync(uri,
            new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        return await ResponseProcessor.ProcessHttpResponseMessageAsync<CreditResponse>(httpResult);
    }

    public async Task<PosVelocityResult<PaymentResponse>> ProcessPaymentTransactionAsync(
        PosVelocityAuthParameters auth,
        PosVelocityDeviceTarget targetDevice,
        PaymentTransactionRequest request,
        string? pos = null,
        int? timeout = 60
    )
    {
        const string resourceUrl = "/api/v2/integrations/terminal/payment";

        var uri = new PosVelocityUriBuilder(resourceUrl, auth)
            .AddDeviceTarget(targetDevice)
            .AddPosId(pos)
            .AddTimeout(timeout)
            .Build();

        var httpResult = await _httpClient.PostAsync(uri,
            new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        return await ResponseProcessor.ProcessHttpResponseMessageAsync<PaymentResponse>(httpResult);
    }

    public async Task<PosVelocityResult<PaymentResponse>> FetchPaymentDetailsAsync(
        PosVelocityAuthParameters auth,
        PosVelocityDeviceTarget targetDevice,
        string paymentId,
        string? pos = null
    )
    {
        const string resourceUrl = "/api/v2/integrations/terminal/payment";

        var uri = new PosVelocityUriBuilder($"{resourceUrl}/{paymentId}", auth)
            .AddDeviceTarget(targetDevice)
            .AddPosId(pos)
            .Build();

        var httpResult = await _httpClient.GetAsync(uri);

        return await ResponseProcessor.ProcessHttpResponseMessageAsync<PaymentResponse>(httpResult);
    }

    public async Task<PosVelocityResult<PaymentResponse>> ProcessVoidTransactionAsync(
        PosVelocityAuthParameters auth,
        PosVelocityDeviceTarget targetDevice,
        string paymentId,
        VoidTransactionRequest request,
        string? pos = null
    )
    {
        const string resourceUrl = "/api/v2/integrations/terminal/payment";
        const string action = "void";

        var uri = new PosVelocityUriBuilder($"{resourceUrl}/{paymentId}/{action}", auth)
            .AddDeviceTarget(targetDevice)
            .AddPosId(pos)
            .Build();

        var httpResult = await _httpClient.PutAsync(uri,
            new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        return await ResponseProcessor.ProcessHttpResponseMessageAsync<PaymentResponse>(httpResult);
    }

    public async Task<PosVelocityResult<PaymentResponse>> ProcessRefundTransactionAsync(
        PosVelocityAuthParameters auth,
        PosVelocityDeviceTarget targetDevice,
        string paymentId,
        RefundTransactionRequest? request = null,
        string? pos = null,
        int? timeout = 60
    )
    {
        const string resourceUrl = "/api/v2/integrations/terminal/payment";
        const string action = "refund";

        var uri = new PosVelocityUriBuilder($"{resourceUrl}/{paymentId}/{action}", auth)
            .AddDeviceTarget(targetDevice)
            .AddPosId(pos)
            .AddTimeout(timeout)
            .Build();

        var content = request == null
            ? null
            : new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        var httpResult = await _httpClient.PutAsync(uri, content);

        return await ResponseProcessor.ProcessHttpResponseMessageAsync<PaymentResponse>(httpResult);
    }

    [Obsolete("GetPrintersAsync is not yet implemented due to API returning a webpage")]
    public Task<PosVelocityResult<object>> GetPaymentReceiptAsync(
        PosVelocityAuthParameters auth,
        PosVelocityDeviceTarget targetDevice,
        string paymentId,
        string? pos = null
    )
    {
        throw new NotImplementedException("GetPrintersAsync is not yet implemented due to API returning a webpage");
    }

    [Obsolete("GetCustomerInputAsync is not yet implemented due to API returning a webpage")]
    public Task<PosVelocityResult<object>> GetCustomerInputAsync(
        PosVelocityAuthParameters auth,
        PosVelocityDeviceTarget targetDevice,
        CustomerInputRequest request,
        string? pos = null
    )
    {
        throw new NotImplementedException("GetCustomerInputAsync is not yet implemented due to API returning a webpage");
    }

    public async Task<PosVelocityResult<ThreeDsResponse>> RetrieveThreeDsRequestAsync(
        PosVelocityAuthParameters auth,
        RetrieveThreeDsQuery query
    )
    {
        const string resourceUrl = "/api/v2/integrations/secure";

        var uri = new PosVelocityUriBuilder($"{resourceUrl}/{query.Id}/", auth)
            .AddTreeDsQueryOptions(query)
            .Build();

        var httpResult = await _httpClient.GetAsync(uri);

        return await ResponseProcessor.ProcessHttpResponseMessageAsync<ThreeDsResponse>(httpResult);
    }

    public async Task<PosVelocityResult<ThreeDsResponse>> CreateAndSendThreeDsRequestAsync(
        PosVelocityAuthParameters auth,
        CreateThreeDsRequest request
    )
    {
        const string resourceUrl = "/api/v2/integrations/secure";

        var uri = new PosVelocityUriBuilder(resourceUrl, auth)
            .Build();

        var httpResult = await _httpClient.PostAsync(uri,
            new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        return await ResponseProcessor.ProcessHttpResponseMessageAsync<ThreeDsResponse>(httpResult);
    }
}