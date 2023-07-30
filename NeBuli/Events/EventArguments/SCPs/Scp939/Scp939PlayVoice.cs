using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

public class Scp939PlayVoice : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939PlayVoice(ReferenceHub player, ReferenceHub target)
    {
        Player = NebuliPlayer.Get(player);
        VoicePlayer = NebuliPlayer.Get(target);
        IsCancelled = false;
    }
    
    public NebuliPlayer Player { get; }
    
    public NebuliPlayer VoicePlayer { get; }

    public bool IsCancelled { get; set; }
}