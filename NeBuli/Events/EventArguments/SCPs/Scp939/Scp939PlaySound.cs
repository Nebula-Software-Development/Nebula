using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

public class Scp939PlaySound : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939PlaySound(ReferenceHub player, byte option)
    {
        Player = NebuliPlayer.Get(player);
        SoundOption = option;
        IsCancelled = false;
    }
    
    public NebuliPlayer Player { get; }

    public bool IsCancelled { get; set; }
    
    public byte SoundOption { get; }
}