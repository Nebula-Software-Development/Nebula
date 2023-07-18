using System;
using static BanHandler;

namespace Nebuli.Events.EventArguments.Player
{
    public class PlayerBannedEventArgs : EventArgs, ICancellableEvent
    {
        public PlayerBannedEventArgs(ReferenceHub Issuer, string reason)
        {
            reason = BanDetails.Reason;          
        }

        public BanDetails BanDetails { get; internal set; }
        public bool IsCancelled { get; set; }
    }
}
