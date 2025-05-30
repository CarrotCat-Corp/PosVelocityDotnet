namespace PosVelocityDotnet.Tests.TestFixtures;

public class PosVelocityApiClientTestFixture : IDisposable
{
    public PosVelocityApiClient PosVelocityClient { get; }
    private bool _disposed;

    public PosVelocityApiClientTestFixture()
    {
        PosVelocityClient = new PosVelocityApiClient(Constants.SandboxApiDomain);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                PosVelocityClient?.Dispose();
            }

            _disposed = true;
        }
    }
}