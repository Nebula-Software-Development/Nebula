using MapGeneration.Distributors;
using Nebuli.API.Features.Map;
using MEC;
using Locker = Nebuli.API.Features.Map.Locker;
using System;
using Object = UnityEngine.Object;
using Nebuli.API.Features;

namespace Nebuli.Events.Handlers;

internal class Internal
{
    internal static void Handler()
    {
        try
        {
            Timing.CallDelayed(0.8f, () =>
            {
                foreach (Scp079Generator generator in Object.FindObjectsOfType<Scp079Generator>())
                    Generator.Get(generator);
                foreach (MapGeneration.Distributors.Locker locker in Object.FindObjectsOfType<MapGeneration.Distributors.Locker>())
                    Locker.Get(locker);
                ServerHandler.OnWaitingForPlayers();
            });
        }
        catch(Exception e)
        {
            Log.Error("Error occured while handling internal Nebuli wrappers! Full error -->\n" + e);
        }    
    }
}
