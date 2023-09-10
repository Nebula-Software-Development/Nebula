using Interactables.Interobjects.DoorUtils;
using Mirror;
using Nebuli.API.Features;
using Nebuli.API.Features.Doors;
using Nebuli.API.Features.Player;
using PlayerRoles;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerInteractingDoorEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerInteractingDoorEvent(ReferenceHub ply, DoorVariant door, byte id)
    {
        Player = NebuliPlayer.Get(ply);
        Door = Door.Get(door);

        IsCancelled = CalculateIsCancelled(ply, door, id);

        Log.Info(IsCancelled);
    }

    /// <summary>
    /// Gets the player interacting with the door.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the <see cref="API.Features.Doors.Door"/> being interacted with.
    /// </summary>
    public Door Door { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    private bool CalculateIsCancelled(ReferenceHub ply, DoorVariant door, byte colliderId)
    {
        bool isCancelled = false;

        if (!NetworkServer.active)
        {
            isCancelled = true;
        }
        else if (door.ActiveLocks > 0 && !ply.serverRoles.BypassMode)
        {
            DoorLockMode mode = DoorLockUtils.GetMode((DoorLockReason)door.ActiveLocks);
            if ((!mode.HasFlagFast(DoorLockMode.CanClose) || !mode.HasFlagFast(DoorLockMode.CanOpen)) &&
                (!mode.HasFlagFast(DoorLockMode.ScpOverride) || !ply.IsSCP(true)) &&
                (mode == DoorLockMode.FullLock || (door.TargetState && !mode.HasFlagFast(DoorLockMode.CanClose)) ||
                (!door.TargetState && !mode.HasFlagFast(DoorLockMode.CanOpen))))
            {
                isCancelled = true;
            }
        }
        else if (!door.AllowInteracting(ply, colliderId)) 
        {
            isCancelled = true;
        }
        else
        {
            bool flag = ply.GetRoleId() == RoleTypeId.Scp079 || door.RequiredPermissions.CheckPermissions(ply.inventory.CurInstance, ply);
            if (!flag)
            {
                isCancelled = true;
            }
        }

        return isCancelled;
    }
}
