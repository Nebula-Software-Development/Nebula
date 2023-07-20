using InventorySystem.Items.Firearms;
using Nebuli.API.Features.Item;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerShotEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerShotEventArgs(ReferenceHub player, Firearm firearm)
    {
        Player = NebuliPlayer.Get(player);
        Firearm = firearm;
        IsCancelled = false;
    }

    public Firearm Firearm { get; }

    public bool IsCancelled { get; set; } 

    public NebuliPlayer Player { get; }
}
