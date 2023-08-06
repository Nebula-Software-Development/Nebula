using InventorySystem.Items.ThrowableProjectiles;
using UnityEngine;

namespace Nebuli.API.Features.Items.Projectiles;

public class ExplosionGrenadeProjectile : GrenadeEffectProjectile
{
    /// <summary>
    /// Gets the <see cref="ExplosionGrenade"/> base.
    /// </summary>
    public new ExplosionGrenade Base { get; }
    internal ExplosionGrenadeProjectile(ExplosionGrenade itemBase) : base(itemBase)
    {
        Base = itemBase;
    }

    /// <summary>
    /// Gets or sets the burned duration of the grenade.
    /// </summary>
    public float BurnedDuration
    {
        get => Base._burnedDuration;
        set => Base._burnedDuration = value;
    }

    /// <summary>
    /// Gets or sets the concussed duration of the grenade.
    /// </summary>
    public float ConcussedDuration
    {
        get => Base._concussedDuration;
        set => Base._concussedDuration = value;
    }

    /// <summary>
    /// Gets or sets the deafened duration of the grenade.
    /// </summary>
    public float DeafenedDuration
    {
        get => Base._deafenedDuration;
        set => Base._deafenedDuration = value;
    }

    /// <summary>
    /// Gets or sets the detection mask of the grenade.
    /// </summary>
    public LayerMask DetectionMask
    {
        get => Base._detectionMask;
        set => Base._detectionMask = value;
    }

    /// <summary>
    /// Gets or sets the hume shield multiplier of the grenade.
    /// </summary>
    public float HumeShieldMultiplier
    {
        get => Base._humeShieldMultipler;
        set => Base._humeShieldMultipler = value;
    }

    /// <summary>
    /// Gets or sets the max radius of the grenade.
    /// </summary>
    public float MaxRadius
    {
        get => Base._maxRadius;
        set => Base._maxRadius = value;
    }
}
