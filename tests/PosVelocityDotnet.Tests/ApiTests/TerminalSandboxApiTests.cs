using PosVelocityDotnet.Model.Common;
using PosVelocityDotnet.Model.DeviceCustomerInput;
using PosVelocityDotnet.Model.DevicePrint;
using PosVelocityDotnet.Model.DeviceScreen;
using PosVelocityDotnet.Model.TransactionCredit;
using PosVelocityDotnet.Model.TransactionPayment;
using PosVelocityDotnet.Tests.Helpers;
using PosVelocityDotnet.Tests.TestFixtures;

namespace PosVelocityDotnet.Tests.ApiTests;

public class TerminalSandboxApiTests : IClassFixture<PosVelocityApiClientTestFixture>, IDisposable
{
    private readonly PosVelocityApiClient _posVelocityClient;
    private bool _disposed;
    private PosVelocityAuthParameters _auth;
    private PosVelocityDeviceTarget _targetDevice;
    private static string? Pos1 = "POS1";
    private static int? Timeout = 60;
    private string? _creditTransactionId;
    private string? _paymentTransactionId;


    public TerminalSandboxApiTests(PosVelocityApiClientTestFixture fixture)
    {
        _posVelocityClient = fixture.PosVelocityClient;

        _auth = new PosVelocityAuthParameters(Secrets.ApiKey);
        _targetDevice = new PosVelocityDeviceTarget(
            Secrets.TerminalSerialNumber,
            PosVelocityDeviceReferenceType.Serial
        );
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing) _posVelocityClient.Dispose();
        _disposed = true;
    }

    [Fact]
    public async Task PingTerminalAsync_SandboxApi_ReturnsValidResponse()
    {
        // Act
        var result = await _posVelocityClient.PingTerminalAsync(_auth, _targetDevice);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task ReadCardAsync_SandboxApi_ReturnsValidResponse()
    {
        // Act
        var result = await _posVelocityClient.ReadCardAsync(_auth, _targetDevice);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value?.Encrypted);
        Assert.NotNull(result.Value?.Track2);
    }

    [Fact]
    public async Task SetScreenMessageAsync_SandboxApi_ReturnsValidResponse()
    {
        // Arrange
        var request = new DisplayScreenMessageRequest("welcome", "Hello World!");

        // Act
        var result = await _posVelocityClient.SetScreenMessageAsync(_auth, _targetDevice, request);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task PrintTextAsync_SandboxApi_ReturnsValidResponse()
    {
        // Arrange
        var request = new PrintRequest(["Test Line 1"]);

        // Act
        var result = await _posVelocityClient.PrintTextAsync(_auth, _targetDevice, request);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task CancelPaymentRequestAsync_SandboxApi_ReturnsValidResponse()
    {
        // Act
        var result = await _posVelocityClient.CancelPaymentRequestAsync(_auth, _targetDevice);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task ProcessCreditTransactionAsync_SandboxApi_ReturnsValidResponse()
    {
        // Arrange
        var request = new CreditRequest(1.00m);


        // Act
        var result = await _posVelocityClient.ProcessCreditTransactionAsync(_auth, _targetDevice, request);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value?.Credit);

        var transactionResult = result.Value?.Credit;
        AssertExtensions.NotNullOrWhiteSpace(transactionResult?.Id);
        _creditTransactionId = transactionResult?.Id;
        Assert.NotNull(transactionResult?.CreatedTime);
        Assert.NotNull(transactionResult.TaxAmount);
        AssertExtensions.NotNullOrWhiteSpace(transactionResult.CardTransaction?.AuthCode);
        // Assert.NotNull(transactionResult?.Employee); // No need to test for employee at this moment in time.
        Assert.Equal(1m, transactionResult.Amount);


        var cardTransaction = transactionResult.CardTransaction;
        Assert.NotNull(cardTransaction);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.AuthCode);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.CardType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.EntryType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Last4);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.ReferenceId);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.State);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Type);

        var cardTransactionExtra = cardTransaction.Extra;
        Assert.NotNull(cardTransactionExtra);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.ApplicationLabel);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.AuthorizingNetworkName);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.CvmResult);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.ApplicationIdentifier);

        var cardTransactionExtraCard = cardTransactionExtra.Card;
        Assert.NotNull(cardTransactionExtraCard);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCard.TransID);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCard.Aci);
    }

    [Fact]
    public async Task ProcessPaymentTransactionAsync_ApprovedTransaction_ReturnsValidApprovedResponse()
    {
        // Arrange
        var request = new PaymentTransactionRequest(1.01m, null);

        // Act
        var result = await _posVelocityClient.ProcessPaymentTransactionAsync(_auth, _targetDevice, request);

        // Assert
        Assert.True(result.IsSuccess);

        var transactionResult = result.Value?.Payment;
        Assert.NotNull(transactionResult);

        Assert.Equal(request.Amount, transactionResult.Amount);
        Assert.NotNull(transactionResult.CreatedTime);
        AssertExtensions.NotNullOrWhiteSpace(transactionResult.Id);
        _paymentTransactionId = transactionResult.Id;
        Assert.NotNull(transactionResult.Offline);
        Assert.Equal("SUCCESS", transactionResult.Result?.ToUpper());
        Assert.NotNull(transactionResult.TaxAmount);

        var attributes = transactionResult.Attributes;
        Assert.NotNull(attributes);
        AssertExtensions.NotNullOrWhiteSpace(attributes.SourceType);
        AssertExtensions.NotNullOrWhiteSpace(attributes.IsQuickChip);
        AssertExtensions.NotNullOrWhiteSpace(attributes.TipSource);
        AssertExtensions.NotNullOrWhiteSpace(attributes.PaymentAppVersion);
        AssertExtensions.NotNullOrWhiteSpace(attributes.CreateAuth);
        AssertExtensions.NotNullOrWhiteSpace(attributes.SourceIp);

        var cardTransaction = transactionResult.CardTransaction;
        Assert.NotNull(cardTransaction);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.AuthCode);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.CardType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.EntryType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.First6);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Last4);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.ReferenceId);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.State);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Token);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.TransactionNo);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Type);

        var cardTransactionExtra = cardTransaction.Extra;
        Assert.NotNull(cardTransactionExtra);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.ApplicationLabel);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.AuthorizingNetworkName);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.CvmResult);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.ApplicationIdentifier);

        var cardTransactionExtraCard = cardTransactionExtra.Card;
        Assert.NotNull(cardTransactionExtraCard);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCard.TransID);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCard.Aci);

        var vaultedCard = cardTransaction.VaultedCard;
        Assert.NotNull(vaultedCard);
        AssertExtensions.NotNullOrWhiteSpace(vaultedCard.ExpirationDate);
        AssertExtensions.NotNullOrWhiteSpace(vaultedCard.First6);
        AssertExtensions.NotNullOrWhiteSpace(vaultedCard.Last4);
        AssertExtensions.NotNullOrWhiteSpace(vaultedCard.Token);

        var employee = transactionResult.Employee;
        Assert.NotNull(employee);
        AssertExtensions.NotNullOrWhiteSpace(employee.Id);

        var order = transactionResult.Order;
        Assert.NotNull(order);
        AssertExtensions.NotNullOrWhiteSpace(order.Id);

        var tender = transactionResult.Tender;
        Assert.NotNull(tender);
        AssertExtensions.NotNullOrWhiteSpace(tender.Id);
        AssertExtensions.NotNullOrWhiteSpace(tender.Label);
        AssertExtensions.NotNullOrWhiteSpace(tender.LabelKey);
        Assert.NotNull(tender.OpensCashDrawer);
    }

    [Fact]
    public async Task FetchPaymentDetailsAsync_SandboxApi_ReturnsValidResponse()
    {
       // // Arrange
       //  Assert.False(
       //      _paymentTransactionId is null,
       //      $"Payment transaction id is null. Please run the test {nameof(ProcessPaymentTransactionAsync_ApprovedTransaction_ReturnsValidApprovedResponse)} first");

        _paymentTransactionId = "2BRTK9PFEDEPE";

        // Act
        var result = await _posVelocityClient.FetchPaymentDetailsAsync(_auth, _targetDevice, _paymentTransactionId);


        // Assert
        Assert.True(result.IsSuccess);

        var transactionResult = result.Value?.Payment;
        Assert.NotNull(transactionResult);

        Assert.NotNull(transactionResult.Amount);
        Assert.NotNull(transactionResult.CreatedTime);
        AssertExtensions.NotNullOrWhiteSpace(transactionResult.Id);
        Assert.NotNull(transactionResult.Offline);
        Assert.Equal("SUCCESS", transactionResult.Result?.ToUpper());
        Assert.NotNull(transactionResult.TaxAmount);

        var attributes = transactionResult.Attributes;
        Assert.NotNull(attributes);
        AssertExtensions.NotNullOrWhiteSpace(attributes.SourceType);
        AssertExtensions.NotNullOrWhiteSpace(attributes.IsQuickChip);
        AssertExtensions.NotNullOrWhiteSpace(attributes.TipSource);
        AssertExtensions.NotNullOrWhiteSpace(attributes.PaymentAppVersion);
        AssertExtensions.NotNullOrWhiteSpace(attributes.CreateAuth);
        AssertExtensions.NotNullOrWhiteSpace(attributes.SourceIp);

        var cardTransaction = transactionResult.CardTransaction;
        Assert.NotNull(cardTransaction);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.AuthCode);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.CardType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.EntryType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.First6);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Last4);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.ReferenceId);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.State);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Token);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.TransactionNo);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Type);

        var cardTransactionExtra = cardTransaction.Extra;
        Assert.NotNull(cardTransactionExtra);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.ApplicationLabel);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.AuthorizingNetworkName);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.CvmResult);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.ApplicationIdentifier);

        var cardTransactionExtraCommon = cardTransactionExtra.Common;
        Assert.NotNull(cardTransactionExtraCommon);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.LocalDateTime);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.PosEntryMode);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.Posid);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.MerchCustom1);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.TermEntryCapablt);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.CardCaptCap);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.Stan);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.PosCondCode);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.TermLocInd);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.PymtType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.MerchId);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.TermId);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.TrnmsnDateTime);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.TermCatCode);

        var cardTransactionExtraCard = cardTransactionExtra.Card;
        Assert.NotNull(cardTransactionExtraCard);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCard.TransID);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCard.Aci);

        // var vaultedCard = cardTransaction.VaultedCard;
        // Assert.NotNull(vaultedCard);
        // AssertExtensions.NotNullOrWhiteSpace(vaultedCard.ExpirationDate);
        // AssertExtensions.NotNullOrWhiteSpace(vaultedCard.First6);
        // AssertExtensions.NotNullOrWhiteSpace(vaultedCard.Last4);
        // AssertExtensions.NotNullOrWhiteSpace(vaultedCard.Token);

        var employee = transactionResult.Employee;
        Assert.NotNull(employee);
        AssertExtensions.NotNullOrWhiteSpace(employee.Id);

        var order = transactionResult.Order;
        Assert.NotNull(order);
        AssertExtensions.NotNullOrWhiteSpace(order.Id);

        var tender = transactionResult.Tender;
        Assert.NotNull(tender);
        AssertExtensions.NotNullOrWhiteSpace(tender.Id);
        AssertExtensions.NotNullOrWhiteSpace(tender.Label);
        AssertExtensions.NotNullOrWhiteSpace(tender.LabelKey);
        Assert.NotNull(tender.OpensCashDrawer);
    }

    [Fact]
    public async Task ProcessVoidTransactionAsync_ForCreditTransaction_ReturnsNotFoundResponse()
    {
        // // Arrange
        // Assert.False(
        //     _creditTransactionId is null,
        //     $"Credit transaction id is null. Please run the test {nameof(ProcessCreditTransactionAsync_SandboxApi_ReturnsValidResponse)} first");

        _creditTransactionId = "DZGHSHX6MKDHJ";

        var request = new VoidTransactionRequest(VoidReason.Default);


        // Act
        var result = await _posVelocityClient.ProcessVoidTransactionAsync(_auth, _targetDevice, _creditTransactionId, request);

        // Assert
        Assert.True(result.IsError);
        Assert.NotNull(result.Error?.Message);
    }

    [Fact]
    public async Task ProcessVoidTransactionAsync_ForPayment_ReturnsApprovedResponse()
    {
        // // Arrange
        // Assert.False(
        //     _paymentTransactionId is null,
        //     $"Payment transaction id is null. Please run the test {nameof(ProcessPaymentTransactionAsync_ApprovedTransaction_ReturnsValidApprovedResponse)} first");

        _paymentTransactionId = "GEJ25BH5D6BE8";

        var request = new VoidTransactionRequest(VoidReason.Default);

        // Act
        var result = await _posVelocityClient.ProcessVoidTransactionAsync(_auth, _targetDevice, _paymentTransactionId, request);

                // Assert
        Assert.True(result.IsSuccess);
        AssertExtensions.NotNullOrWhiteSpace(result.Value?.PaymentId);
        AssertExtensions.NotNullOrWhiteSpace(result.Value?.VoidReason);
        AssertExtensions.NotNullOrWhiteSpace(result.Value?.VoidStatus);


        var payment = result.Value?.Payment;
        Assert.NotNull(payment);

        Assert.NotNull(payment.Amount);
        Assert.NotNull(payment.CreatedTime);
        AssertExtensions.NotNullOrWhiteSpace(payment.Id);
        Assert.NotNull(payment.Offline);
        Assert.Equal("SUCCESS", payment.Result?.ToUpper());
        Assert.NotNull(payment.TaxAmount);

        var attributes = payment.Attributes;
        Assert.NotNull(attributes);
        AssertExtensions.NotNullOrWhiteSpace(attributes.SourceType);
        AssertExtensions.NotNullOrWhiteSpace(attributes.IsQuickChip);
        AssertExtensions.NotNullOrWhiteSpace(attributes.TipSource);
        AssertExtensions.NotNullOrWhiteSpace(attributes.PaymentAppVersion);
        AssertExtensions.NotNullOrWhiteSpace(attributes.CreateAuth);
        AssertExtensions.NotNullOrWhiteSpace(attributes.SourceIp);

        var cardTransaction = payment.CardTransaction;
        Assert.NotNull(cardTransaction);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.AuthCode);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.CardType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.EntryType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.First6);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Last4);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.ReferenceId);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.State);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Token);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.TransactionNo);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Type);

        var cardTransactionExtra = cardTransaction.Extra;
        Assert.NotNull(cardTransactionExtra);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.ApplicationLabel);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.AuthorizingNetworkName);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.CvmResult);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.ApplicationIdentifier);

        var cardTransactionExtraCommon = cardTransactionExtra.Common;
        Assert.NotNull(cardTransactionExtraCommon);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.LocalDateTime);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.PosEntryMode);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.Posid);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.MerchCustom1);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.TermEntryCapablt);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.CardCaptCap);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.Stan);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.PosCondCode);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.TermLocInd);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.PymtType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.MerchId);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.TermId);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.TrnmsnDateTime);
        AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.TermCatCode);

        var cardTransactionExtraCard = cardTransactionExtra.Card;
        Assert.NotNull(cardTransactionExtraCard);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCard.TransID);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCard.Aci);

        // var vaultedCard = cardTransaction.VaultedCard;
        // Assert.NotNull(vaultedCard);
        // AssertExtensions.NotNullOrWhiteSpace(vaultedCard.ExpirationDate);
        // AssertExtensions.NotNullOrWhiteSpace(vaultedCard.First6);
        // AssertExtensions.NotNullOrWhiteSpace(vaultedCard.Last4);
        // AssertExtensions.NotNullOrWhiteSpace(vaultedCard.Token);

        var employee = payment.Employee;
        Assert.NotNull(employee);
        AssertExtensions.NotNullOrWhiteSpace(employee.Id);

        var order = payment.Order;
        Assert.NotNull(order);
        AssertExtensions.NotNullOrWhiteSpace(order.Id);

        var refunds = payment.Refunds;
        Assert.NotNull(refunds);

        var tender = payment.Tender;
        Assert.NotNull(tender);
        AssertExtensions.NotNullOrWhiteSpace(tender.Id);
        AssertExtensions.NotNullOrWhiteSpace(tender.Label);
        AssertExtensions.NotNullOrWhiteSpace(tender.LabelKey);
        Assert.NotNull(tender.OpensCashDrawer);
    }

    [Fact]
    public async Task ProcessRefundTransactionAsync_ForPayment_ReturnsValidResponse()
    {
        // // Arrange
        // Assert.False(
        //     _paymentTransactionId is null,
        //     $"Payment transaction id is null. Please run the test {nameof(ProcessPaymentTransactionAsync_ApprovedTransaction_ReturnsValidApprovedResponse)} first");

        _paymentTransactionId = "T1TSDPYARQW20";


        // Act
        var result = await _posVelocityClient.ProcessRefundTransactionAsync(_auth, _targetDevice, _paymentTransactionId);

        // Assert
        Assert.True(result.IsSuccess);
        AssertExtensions.NotNullOrWhiteSpace(result.Value?.PaymentId);
        Assert.NotNull(result.Value?.FullRefund);

        var payment = result.Value?.Payment;
        Assert.NotNull(payment);

        Assert.NotNull(payment.Amount);
        Assert.NotNull(payment.CreatedTime);
        AssertExtensions.NotNullOrWhiteSpace(payment.Id);
        Assert.NotNull(payment.Offline);
        Assert.Equal("SUCCESS", payment.Result?.ToUpper());
        Assert.NotNull(payment.TaxAmount);

        var attributes = payment.Attributes;
        Assert.NotNull(attributes);
        AssertExtensions.NotNullOrWhiteSpace(attributes.SourceType);
        AssertExtensions.NotNullOrWhiteSpace(attributes.IsQuickChip);
        AssertExtensions.NotNullOrWhiteSpace(attributes.TipSource);
        AssertExtensions.NotNullOrWhiteSpace(attributes.PaymentAppVersion);
        AssertExtensions.NotNullOrWhiteSpace(attributes.CreateAuth);
        AssertExtensions.NotNullOrWhiteSpace(attributes.SourceIp);

        var cardTransaction = payment.CardTransaction;
        Assert.NotNull(cardTransaction);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.AuthCode);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.CardType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.EntryType);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.First6);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Last4);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.ReferenceId);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.State);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Token);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.TransactionNo);
        AssertExtensions.NotNullOrWhiteSpace(cardTransaction.Type);

        // var cardTransactionExtra = cardTransaction.Extra;
        // Assert.NotNull(cardTransactionExtra);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.ApplicationLabel);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.AuthorizingNetworkName);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.CvmResult);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtra.ApplicationIdentifier);

        // var cardTransactionExtraCommon = cardTransactionExtra.Common;
        // Assert.NotNull(cardTransactionExtraCommon);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.LocalDateTime);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.PosEntryMode);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.Posid);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.MerchCustom1);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.TermEntryCapablt);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.CardCaptCap);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.Stan);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.PosCondCode);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.TermLocInd);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.PymtType);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.MerchId);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.TermId);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.TrnmsnDateTime);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCommon.TermCatCode);
        //
        // var cardTransactionExtraCard = cardTransactionExtra.Card;
        // Assert.NotNull(cardTransactionExtraCard);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCard.TransID);
        // AssertExtensions.NotNullOrWhiteSpace(cardTransactionExtraCard.Aci);

        // var vaultedCard = cardTransaction.VaultedCard;
        // Assert.NotNull(vaultedCard);
        // AssertExtensions.NotNullOrWhiteSpace(vaultedCard.ExpirationDate);
        // AssertExtensions.NotNullOrWhiteSpace(vaultedCard.First6);
        // AssertExtensions.NotNullOrWhiteSpace(vaultedCard.Last4);
        // AssertExtensions.NotNullOrWhiteSpace(vaultedCard.Token);

        var employee = payment.Employee;
        Assert.NotNull(employee);
        AssertExtensions.NotNullOrWhiteSpace(employee.Id);

        var order = payment.Order;
        Assert.NotNull(order);
        AssertExtensions.NotNullOrWhiteSpace(order.Id);

        var tender = payment.Tender;
        Assert.NotNull(tender);
        AssertExtensions.NotNullOrWhiteSpace(tender.Id);
        AssertExtensions.NotNullOrWhiteSpace(tender.Label);
        AssertExtensions.NotNullOrWhiteSpace(tender.LabelKey);
        Assert.NotNull(tender.OpensCashDrawer);

        var refund = result.Value?.Refund;
        Assert.NotNull(refund);
        Assert.NotNull(refund.Amount);
        Assert.NotNull(refund.CreatedTime);
        AssertExtensions.NotNullOrWhiteSpace(refund.Device?.Id);
        AssertExtensions.NotNullOrWhiteSpace(refund.Employee?.Id);
        AssertExtensions.NotNullOrWhiteSpace(refund.Id);
        AssertExtensions.NotNullOrWhiteSpace(refund.Payment?.Id);
        Assert.NotNull(refund.TaxAmount);
        Assert.NotNull(refund.Voided);
    }

    [Fact]
    public async Task GetPaymentReceiptAsync_SandboxApi_ReturnsValidResponse()
    {
        // Arrange
        _paymentTransactionId = "T1TSDPYARQW20";


        Assert.False(
            _paymentTransactionId is null,
            $"Payment transaction id is null. Please run the test {nameof(ProcessPaymentTransactionAsync_ApprovedTransaction_ReturnsValidApprovedResponse)} first");

        // Act
        var result = await _posVelocityClient.GetPaymentReceiptAsync(_auth, _targetDevice, _paymentTransactionId);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetCustomerInputAsync_SandboxApi_ReturnsValidResponse()
    {
        // Arrange
        var request = new CustomerInputRequest([
            new InputScreen("What time is it?", PosVelocityInputType.Text)
        ]);

        // Act
        var result = await _posVelocityClient.GetCustomerInputAsync(_auth, _targetDevice, request);

        // Assert
        Assert.True(result.IsSuccess);
    }
}