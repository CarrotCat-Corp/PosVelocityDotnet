using PosVelocityDotnet.Model.Common;
using PosVelocityDotnet.Model.ThreeDs;

namespace PosVelocityDotnet.Utilities;

internal class PosVelocityUriBuilder
{
    private readonly Dictionary<string, string> _queryParams;
    private readonly string _url;

    private const string ApiKeyKey = "apikey";
    private const string StartKey = "start";
    private const string EndKey = "end";
    private const string TerminalTargetKey = "terminal";
    private const string TerminalTargetTypeKey = "use";
    private const string PosIdKey = "pos";
    private const string TimeoutKey = "timeout";

    internal PosVelocityUriBuilder(string url, PosVelocityAuthParameters auth)
    {
        _queryParams = new Dictionary<string, string>
        {
            { ApiKeyKey, auth.ApiKey }
        };
        _url = url;
    }

    internal PosVelocityUriBuilder AddStartAndEnd(DateTimeOffset start, DateTimeOffset end)
    {
        _queryParams.Add(StartKey, start.ToString());
        _queryParams.Add(EndKey, end.ToString());
        return this;
    }

    internal PosVelocityUriBuilder AddDeviceTarget(PosVelocityDeviceTarget target)
    {
        _queryParams.Add(TerminalTargetKey, target.Terminal);
        _queryParams.Add(TerminalTargetTypeKey, target.Use.ToString());
        return this;
    }

    internal PosVelocityUriBuilder AddPosId(string? pos)
    {
        if (string.IsNullOrWhiteSpace(pos)) return this;
        _queryParams.Add(PosIdKey, pos);
        return this;
    }

    internal PosVelocityUriBuilder AddTimeout(int? timeout)
    {
        if (timeout is null) return this;
        _queryParams.Add(TimeoutKey, timeout.ToString()??string.Empty);
        return this;
    }

    internal PosVelocityUriBuilder AddTreeDsQueryOptions(RetrieveThreeDsQuery query)
    {
        if (query.Expand is null) return this;
        _queryParams.Add(TimeoutKey, query.Expand.ToString()??string.Empty);
        return this;
    }


    internal string Build()
    {
        var queryParams = _queryParams.Select(x => $"{x.Key}={x.Value}");
        return $"{_url}?{string.Join("&", queryParams)}";
    }
}