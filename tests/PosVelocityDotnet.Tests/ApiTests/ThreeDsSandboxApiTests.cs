using PosVelocityDotnet.Model.Common;
using PosVelocityDotnet.Model.ThreeDs;
using PosVelocityDotnet.Tests.TestFixtures;

namespace PosVelocityDotnet.Tests.ApiTests;

public class ThreeDsSandboxApiTests : IClassFixture<PosVelocityApiClientTestFixture>, IDisposable
{
    private readonly PosVelocityApiClient _posVelocityClient;
    private bool _disposed;
    private PosVelocityAuthParameters _auth;
    private PosVelocityDeviceTarget _targetDevice;
    private static string? Pos1 = "POS1";
    private static int? Timeout = 60;


    public ThreeDsSandboxApiTests(PosVelocityApiClientTestFixture fixture)
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
    public async Task CreateAndSendThreeDsRequestAsync_SandboxApi_ReturnsValidResponse()
    {
        // Arrange
        var request = new CreateThreeDsRequest(
            1.01m,
            new ThreeDsRequestRecipient("", "oleg@empyreanms.com", null),
            null,
            "Test request",
            "1234567890-1"
        );

        // Act
        var result = await _posVelocityClient.CreateAndSendThreeDsRequestAsync(_auth, request);

        // Assert
        Assert.True(result.IsSuccess);
        var response = result.Value;
        Assert.NotNull(response);
        Assert.NotNull(response.Id);
        Assert.NotNull(response.Date);
        Assert.NotNull(response.Amount);
        Assert.True(response.Email is not null || response.Phone is not null);
        Assert.NotNull(response.Url);
        Assert.NotNull(response.Status);
    }

}