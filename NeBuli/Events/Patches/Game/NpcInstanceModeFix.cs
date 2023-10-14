using Nebuli.API.Extensions;

namespace Nebuli.Events.Patches.Game;
internal static class NpcInstanceModeFix
{
    internal static void HandleInstanceModeChange(ReferenceHub arg1, ClientInstanceMode arg2)
    {
        if ((arg2 != ClientInstanceMode.Unverified || arg2 != ClientInstanceMode.Host) 
            && arg2 != ClientInstanceMode.DedicatedServer && arg1.ToNebuliPlayer() != null && arg1.ToNebuliPlayer().IsNPC)
            arg1.characterClassManager.InstanceMode = ClientInstanceMode.Host;
    }
}
