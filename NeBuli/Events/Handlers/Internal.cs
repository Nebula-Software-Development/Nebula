// -----------------------------------------------------------------------
// <copyright file=Internal.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using MapGeneration.Distributors;
using MEC;
using Nebuli.API.Features;
using Nebuli.API.Features.Map;
using System;
using Locker = Nebuli.API.Features.Map.Locker;
using Object = UnityEngine.Object;

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
                ServerHandlers.OnWaitingForPlayers();

            });
        }
        catch (Exception e)
        {
            Log.Error("Error occured while handling internal Nebuli wrappers! Full error -->\n" + e);
        }

        try
        {
            Permissions.PermissionsHandler.LoadPermissions();
        }
        catch (Exception e)
        {
            Log.Error("Error occured while loading permission handler! Full error -->\n" + e);
        }
    }
}