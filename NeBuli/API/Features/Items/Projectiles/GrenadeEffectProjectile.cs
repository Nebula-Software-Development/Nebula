using InventorySystem.Items.ThrowableProjectiles;

namespace Nebuli.API.Features.Items.Projectiles;

public class GrenadeEffectProjectile : TimedExplosiveProjectile
{
    /// <summary>
    /// Gets the <see cref="EffectGrenade"/> base.
    /// </summary>
    public new EffectGrenade Base { get; }
    internal GrenadeEffectProjectile(EffectGrenade timeGrenade) : base(timeGrenade)
    {
        Base = timeGrenade;
    }
}
