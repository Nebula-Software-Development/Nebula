using InventorySystem.Items.ThrowableProjectiles;
using Nebuli.API.Features.Items.Pickups;

namespace Nebuli.API.Features.Items.Projectiles;

public class Projectile : Pickup
{
    /// <summary>
    /// Gets the <see cref="ThrownProjectile"/> base.
    /// </summary>
    public new ThrownProjectile Base { get; }

    internal Projectile(ThrownProjectile thrownProjectile) : base(thrownProjectile)
    {
        Base = thrownProjectile;
    }

    /// <summary>
    /// Activates the projectile.
    /// </summary>
    public void Activate() => Base.ServerActivate();
}