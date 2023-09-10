using SinkholeHazardBase = Hazards.SinkholeEnvironmentalHazard;

namespace Nebuli.API.Features.Hazards;

public class SinkholeHazard : EnviormentHazard
{
    /// <summary>
    /// Gets the <see cref="SinkholeHazardBase"/> base.
    /// </summary>
    public new SinkholeHazardBase Base { get; }
    internal SinkholeHazard(SinkholeHazardBase hazardBase) : base(hazardBase)
    {
        Base = hazardBase;
    }
}
