namespace PosVelocityDotnet.Model.Common;

public class PosVelocityDeviceTarget
{
    /// <summary>
    /// Serial Number string or Label string
    /// </summary>
    public string Terminal { get; private set; } = string.Empty;

    /// <summary>
    /// Specifies a device reference type. Serial or Label.
    /// </summary>
    public PosVelocityDeviceReferenceType Use { get; private set; }

    private PosVelocityDeviceTarget()
    {
    }

    public PosVelocityDeviceTarget(string terminal, PosVelocityDeviceReferenceType use)
    {
        if (string.IsNullOrWhiteSpace(terminal)) throw new ArgumentException("Terminal parameter cannot be null or empty", nameof(terminal));
        Terminal = terminal;
        Use = use;
    }
}