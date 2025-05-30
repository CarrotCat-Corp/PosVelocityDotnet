using PosVelocityDotnet.Model.Common;
using PosVelocityDotnet.Model.TransactionCredit;
using PosVelocityDotnet.Tests.TestFixtures;

namespace PosVelocityDotnet.Tests.ApiTests;

public class ApiErrorsTests : IClassFixture<PosVelocityApiClientTestFixture>, IDisposable
{
    private readonly PosVelocityApiClient _posVelocityClient;
    private bool _disposed;
    private PosVelocityAuthParameters _auth;
    private PosVelocityDeviceTarget _targetDevice;
    private static string? Pos1 = "POS1";
    private static int? Timeout = 60;


    public ApiErrorsTests(PosVelocityApiClientTestFixture fixture)
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

    /// <summary>
    /// Tests response for when an invalid API key is provided.
    /// </summary>
    [Fact]
    public async Task PingTerminalAsync_WithInvalidApiKey_ReturnsForbiddenResponse()
    {
        // Arrange
        var auth = new PosVelocityAuthParameters("an-invalid-api-key");

        // Act
        var result = await _posVelocityClient.PingTerminalAsync(auth, _targetDevice);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("You are not authorized. Please ensure that your configuration is correct.",
            result.Error?.Message);
    }

    /// <summary>
    /// Tests response for when an invalid serial number is provided for the target device.
    /// </summary>
    [Fact]
    public async Task PingTerminalAsync_WithInvalidSerial_ReturnsInvalidSerialErrorResponse()
    {
        // Arrange
        var invalidTargetDevice =
            new PosVelocityDeviceTarget("an-invalid-serial", PosVelocityDeviceReferenceType.Serial);

        // Act
        var result = await _posVelocityClient.PingTerminalAsync(_auth, invalidTargetDevice);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal($"An invalid device serial number [{invalidTargetDevice.Terminal}] or token was provided.",
            result.Error?.Message);
    }

    /// <summary>
    /// Tests response for when the device is not connected to internet
    /// </summary>
    [Fact]
    public async Task PingTerminalAsync_DeviceOffline_ReturnsDeviceConnectionErrorResponse()
    {
        // Arrange


        // Act
        var result = await _posVelocityClient.PingTerminalAsync(_auth, _targetDevice);

        // Assert
        Assert.True(result.IsError);
        Assert.True(result.Error?.Message?.Contains("A connection to your Clover device"));
    }


    /// <summary>
    /// Tests response for when the device times out without any user input
    /// </summary>
    [Fact]
    public async Task ProcessCreditTransactionAsync_DeviceTimeout_ReturnsErrorResponse()
    {
        // Arrange
        var request = new CreditRequest(1.00m);

        // Act
        var result = await _posVelocityClient.ProcessCreditTransactionAsync(_auth, _targetDevice, request);

        // Assert
        Assert.True(result.IsError);
        Assert.True(
            result.Error?.Message?.Contains("User canceled.") == true
            || result.Error?.Message?.Contains(
                "A final response for operation CREDIT was not received within the timeout period") == true
            || result.Error?.Message?.Contains("Request cancelled") == true
        );
    }

    /// <summary>
    /// Tests a scenario where user cancels transaction by taping X on the screen.
    /// </summary>
    [Fact]
    public async Task ProcessCreditTransactionAsync_CardholderCanceled_ReturnsErrorResponse()
    {
        // Arrange
        var request = new CreditRequest(1.00m);

        // Act
        var result = await _posVelocityClient.ProcessCreditTransactionAsync(_auth, _targetDevice, request);

        // Assert
        Assert.True(result.IsError);
        Assert.True(
            result.Error?.Message?.Contains("User canceled.") == true
            || result.Error?.Message?.Contains(
                "The credit request was canceled") == true
            || result.Error?.Message?.Contains("Request cancelled") == true
        );
    }
}