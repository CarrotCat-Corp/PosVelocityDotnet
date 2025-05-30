using PosVelocityDotnet.Model.Common;
using PosVelocityDotnet.Tests.Helpers;
using PosVelocityDotnet.Tests.TestFixtures;

namespace PosVelocityDotnet.Tests.ApiTests;

public class AccountSandboxApiTests : IClassFixture<PosVelocityApiClientTestFixture>, IDisposable
{
    private readonly PosVelocityApiClient _posVelocityClient;
    private bool _disposed;
    private PosVelocityAuthParameters _auth;
    private PosVelocityDeviceTarget _targetDevice;
    private static string? Pos1 = "POS1";
    private static int? Timeout = 60;


    public AccountSandboxApiTests(PosVelocityApiClientTestFixture fixture)
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

    // this test is suspended because API endpoint returns an error
    // [Fact]
    // public async Task GetPrintersAsync_SandboxApi_ReturnsValidResponse()
    // {
    //     // Act
    //     var result = await _posVelocityClient.GetPrintersAsync(_auth);
    //
    //     // Assert
    //     Assert.True(result.IsSuccess);
    // }

    [Fact]
    public async Task GetAccountDevicesAsync_SandboxApi_ReturnsValidResponse()
    {
        // Act
        var result = await _posVelocityClient.GetAccountDevicesAsync(_auth);

        // Assert
        Assert.True(result.IsSuccess);

        var resultValue = result.Value?.ToList();
        Assert.NotNull(resultValue);
        Assert.True(resultValue.Count > 0);

        var firstDevice = resultValue.First();
        Assert.NotNull(firstDevice);
        AssertExtensions.NotNullOrWhiteSpace(firstDevice.Id);
        AssertExtensions.NotNullOrWhiteSpace(firstDevice.Model);
        Assert.NotNull(firstDevice.TerminalPrefix);
        AssertExtensions.NotNullOrWhiteSpace(firstDevice.Serial);
        AssertExtensions.NotNullOrWhiteSpace(firstDevice.SecureId);
        AssertExtensions.NotNullOrWhiteSpace(firstDevice.BuildType);
        AssertExtensions.NotNullOrWhiteSpace(firstDevice.ProductName);
        Assert.NotNull(firstDevice.PinDisabled);
        AssertExtensions.NotNullOrWhiteSpace(firstDevice.DeviceTypeName);
        Assert.NotNull(firstDevice.OfflinePayments);
        Assert.NotNull(firstDevice.OfflinePaymentsAll);

    }

    [Fact]
    public async Task GetAccountEmployeesAsync_SandboxApi_ReturnsValidResponse()
    {
        // Act
        var result = await _posVelocityClient.GetAccountEmployeesAsync(_auth);

        // Assert
        Assert.True(result.IsSuccess);

        var resultValue = result.Value?.ToList();
        Assert.NotNull(resultValue);
        Assert.True(resultValue.Count > 0);

        var firstEmployee = resultValue.First();
        Assert.NotNull(firstEmployee);
        AssertExtensions.NotNullOrWhiteSpace(firstEmployee.Id);
        AssertExtensions.NotNullOrWhiteSpace(firstEmployee.Name);
        AssertExtensions.NotNullOrWhiteSpace(firstEmployee.Nickname);
        AssertExtensions.NotNullOrWhiteSpace(firstEmployee.Role);
        Assert.NotNull(firstEmployee.IsOwner);
    }

    // this test is suspended because there is no documentation for required parameters "start" and "end"
    // [Fact]
    // public async Task GetAccountTransactionsAsync_SandboxApi_ReturnsValidResponse()
    // {
    //     // Act
    //     var result = await _posVelocityClient.GetAccountTransactionsAsync(_auth);
    //
    //     // Assert
    //     Assert.True(result.IsSuccess);
    // }

}