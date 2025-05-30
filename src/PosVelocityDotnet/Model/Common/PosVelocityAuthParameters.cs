namespace PosVelocityDotnet.Model.Common;

public class PosVelocityAuthParameters
{
    public string ApiKey { get; private set; }

    private PosVelocityAuthParameters()
    {
    }

    public PosVelocityAuthParameters(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentException("API Key cannot be null or empty", nameof(apiKey));
        ApiKey = apiKey;
    }
}