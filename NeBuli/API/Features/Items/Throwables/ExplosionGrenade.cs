using InventorySystem.Items.ThrowableProjectiles;
using Nebuli.API.Features.Items.Projectiles;

namespace Nebuli.API.Features.Items.Throwables;

public class ExplosionGrenade : Throwable
{
    /// <summary>
    /// Gets the <see cref="ExplosionGrenadeProjectile"/> base.
    /// </summary>
    public new ExplosionGrenadeProjectile Projectile { get; }
    internal ExplosionGrenade(ThrowableItem itemBase) : base(itemBase)
    {
        Projectile = (ExplosionGrenadeProjectile)((Throwable)this).Projectile;
    }

}
