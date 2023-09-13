using Scp2176ProjectileBase = InventorySystem.Items.ThrowableProjectiles.Scp2176Projectile;

namespace Nebuli.API.Features.Items.Projectiles;

public class Scp2176Projectile : GrenadeEffectProjectile
{
    /// <summary>
    /// Gets the <see cref="Scp2176ProjectileBase"/> base.
    /// </summary>
    public new Scp2176ProjectileBase Base { get; }

    internal Scp2176Projectile(Scp2176ProjectileBase itemBase) : base(itemBase)
    {
        Base = itemBase;
    }

    /// <summary>
    /// Gets or sets if the next collision will trigger the drop sound.
    /// </summary>
    public bool PlayDropSound
    {
        get => Base.Network_playedDropSound;
        set => Base.Network_playedDropSound = value;
    }

    /// <summary>
    /// Gets if SCP-2176 has triggered.
    /// </summary>
    public bool HasTriggered => Base._hasTriggered;
}