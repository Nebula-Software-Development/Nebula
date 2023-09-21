using TemporaryHazardBase = Hazards.TemporaryHazard;

namespace Nebuli.API.Features.Hazards;

public class TemporaryHazard : EnviormentHazard
{
    /// <summary>
    /// Gets the <see cref="TemporaryHazardBase"/> base.
    /// </summary>
    public new TemporaryHazardBase Base { get; }

    internal TemporaryHazard(TemporaryHazardBase hazardBase) : base(hazardBase)
    {
        Base = hazardBase;
    }

    /// <summary>
    /// Gets the maximum amount of time the hazard can last.
    /// </summary>
    public float MaxDuration => Base.HazardDuration;

    /// <summary>
    /// Gets or sets if the hazard is destroyed.
    /// </summary>
    public bool Destroyed
    {
        get => Base._destroyed;
        set => Base._destroyed = value;
    }

    /// <summary>
    /// Gets or sets the elapsed time of the hazard.
    /// </summary>
    public float ElapsedTime
    {
        get => Base._elapsed;
        set => Base._elapsed = value;
    }

    /// <summary>
    /// Destroys the hazard.
    /// </summary>
    public void Destroy() => Base.ServerDestroy();
}