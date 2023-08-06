using InventorySystem.Items.ThrowableProjectiles;

namespace Nebuli.API.Features.Items.Projectiles;

public class TimedExplosiveProjectile : Projectile
{
    /// <summary>
    /// Gets the <see cref="TimeGrenade"/> base.
    /// </summary>
    public new TimeGrenade Base { get; }
    internal TimedExplosiveProjectile(TimeGrenade timeGrenade) : base(timeGrenade)
    {
        Base = timeGrenade;
    }

    /// <summary>
    /// Detonates the grenade.
    /// </summary>
    public void Detonate()
    {
        Base.ServerFuseEnd();
        Base._alreadyDetonated = true;
    }

    /// <summary>
    /// Gets if the grenade is detonated.
    /// </summary>
    public bool Detonated => Base._alreadyDetonated;

    /// <summary>
    /// Gets or sets if the owner was the server.
    /// </summary>
    public bool WasServer
    {
        get => Base._wasServer;
        set => Base._wasServer = value;
    }

    /// <summary>
    /// Gets or sets the fuze time of the grenade.
    /// </summary>
    public float FuzeTime
    {
        get => Base._fuseTime;
        set => Base._fuseTime = value;
    }

}
