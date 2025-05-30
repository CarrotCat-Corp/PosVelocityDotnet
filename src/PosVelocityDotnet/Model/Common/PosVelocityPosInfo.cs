namespace PosVelocityDotnet.Model.Common;

public class PosVelocityPosInfo
{
    /// <summary>
    /// POS ID or label or name
    /// </summary>
    public string Pos { get; private set; } = string.Empty;


    private PosVelocityPosInfo()
    {
    }

    public PosVelocityPosInfo(string pos)
    {
        if (string.IsNullOrWhiteSpace(pos)) throw new ArgumentException("POS ID cannot be null or empty", nameof(pos));
        Pos = pos;
    }
}