using UnityEngine;
using MapGeneration.Distributors;
using Nebuli.API.Features.Map;
using MEC;

namespace Nebuli.Events.Handlers;

internal class Internal
{
    internal static void Handler()
    {
        Timing.CallDelayed(1f, () =>
        {
            foreach (Scp079Generator generator in Object.FindObjectsOfType<Scp079Generator>())
                Generator.Get(generator);
            ServerHandler.OnWaitingForPlayers();
        });
    }
}
