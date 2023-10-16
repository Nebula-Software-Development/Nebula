using Nebuli.API.Features.Map;
using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp079;

/// <summary>
/// Triggered when SCP-079 is changing its camera.
/// </summary>
public class Scp079ChangingCameraEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp079ChangingCameraEvent(ReferenceHub player, float auxdrain, Scp079Camera camera)
    {
        Player = NebuliPlayer.Get(player);
        AuxDrain = auxdrain;
        Camera = Camera.Get(camera);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player changing the camera.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the amount of power that will be drained.
    /// </summary>
    public float AuxDrain { get; set; }

    /// <summary>
    /// Gets the <see cref="API.Features.Map.Camera"/> being switched to.
    /// </summary>
    public Camera Camera { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}