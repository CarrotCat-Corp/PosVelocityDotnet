namespace PosVelocityDotnet.Model.ThreeDs;

public sealed class RetrieveThreeDsQuery
{
    public string Id { get; private set; }
    public PosVelocityThreeDsRequestExpandType? Expand { get; private set; }

    private RetrieveThreeDsQuery()
    {

    }

    public RetrieveThreeDsQuery(string id, PosVelocityThreeDsRequestExpandType? expand)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("3DS request ID cannot be null or empty", nameof(id));
        Id = id;
        Expand = expand;
    }
}