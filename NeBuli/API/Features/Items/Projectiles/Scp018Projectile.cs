using UnityEngine;
using Scp018ProjectileBase = InventorySystem.Items.ThrowableProjectiles.Scp018Projectile;

namespace Nebuli.API.Features.Items.Projectiles;

public class Scp018Projectile : TimedExplosiveProjectile
{
    /// <summary>
    /// Gets the <see cref="Scp018ProjectileBase"/> base.
    /// </summary>
    public new Scp018ProjectileBase Base { get; }
    internal Scp018Projectile(Scp018ProjectileBase scp018Base) : base(scp018Base)
    {
        Base = scp018Base;
    }

    /// <summary>
    /// Registers a bounce with the specified velocity and point.
    /// </summary>
    /// <param name="velocity">The velocity of the bounce.</param>
    /// <param name="point">The point of the bounce.</param>
    public void RegisterBounce(float velocity, Vector3 point) => Base.RegisterBounce(velocity, point);

    /// <summary>
    /// Gets the current damage of SCP-018.
    /// </summary>
    public float CurrentDamage => Base.CurrentDamage;

    /// <summary>
    /// Gets or sets the damage multiplier applied to SCPs when hit by SCP-018.
    /// </summary>
    public float SCPDamageMultiplier
    {
        get => Base._scpDamageMultiplier;
        set => Base._scpDamageMultiplier = value;
    }

    /// <summary>
    /// Gets or sets the damage multiplier applied to doors when hit by SCP-018.
    /// </summary>
    public float DoorDamageMultiplier
    {
        get => Base._doorDamageMultiplier;
        set => Base._doorDamageMultiplier = value;
    }

    /// <summary>
    /// Gets or sets the velocity addition applied on bounce.
    /// </summary>
    public float BounceVelocityAddition
    {
        get => Base._onBounceVelocityAddition;
        set => Base._onBounceVelocityAddition = value;
    }

    /// <summary>
    /// Gets or sets the radius of each bounce of SCP-018.
    /// </summary>
    public float Radius
    {
        get => Base._radius;
        set => Base._radius = value;
    }

    /// <summary>
    /// Gets or sets the maximum velocity of SCP-018.
    /// </summary>
    public float MaxVelocity
    {
        get => Base._maximumVelocity;
        set => Base._maximumVelocity = value;
    }

    /// <summary>
    /// Gets or sets the last velocity of SCP-018.
    /// </summary>
    public float LastVelocity
    {
        get => Base._lastVelocity;
        set => Base._lastVelocity = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the bounce sound cooldown is bypassed.
    /// </summary>
    public bool BypassBounceSoundCoolDown
    {
        get => Base._bypassBounceSoundCooldown;
        set => Base._bypassBounceSoundCooldown = value;
    }

    /// <summary>
    /// Gets or sets the recreated velocity of SCP-018.
    /// </summary>
    public Vector3 RecreatedVelocity
    {
        get => Base.RecreatedVelocity;
        set => Base.RecreatedVelocity = value;
    }

    /// <summary>
    /// Gets or sets the activation time of SCP-018. 
    /// </summary>
    public double ActivationTime
    {
        get => Base._activationTime;
        set => Base._activationTime = value;
    }

    /// <summary>
    /// Gets or sets the friendly fire time of SCP-018.
    /// </summary>
    public float FriendlyFireTime
    {
        get => Base._friendlyFireTime;
        set => Base._friendlyFireTime = value;
    }

}
