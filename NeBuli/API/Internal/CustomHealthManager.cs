using PlayerStatsSystem;

namespace Nebuli.API.Internal;

public class CustomHealthManager : HealthStat
{
    public override float MaxValue => MaxHealth == default ? base.MaxValue : MaxHealth;
    public override float MinValue => MinHealth == default ? base.MinValue : MinHealth;

    /// <summary>
    /// Gets or sets the max health of the player.
    /// </summary>
    public float MaxHealth { get; set; }

    /// <summary>
    /// Gets or sets the minimum health of the player.
    /// </summary>
    public float MinHealth { get; set; }
}
