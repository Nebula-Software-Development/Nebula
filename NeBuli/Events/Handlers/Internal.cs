// -----------------------------------------------------------------------
// <copyright file=Internal.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using MapGeneration.Distributors;
using MEC;
using Nebula.API.Features;
using Nebula.API.Features.Map;
using Locker = MapGeneration.Distributors.Locker;
using Object = UnityEngine.Object;

namespace Nebula.Events.Handlers
{
    internal class Internal
    {
        internal static void Handler()
        {
            try
            {
                Timing.CallDelayed(0.8f, () =>
                {
                    foreach (Scp079Generator generator in Object.FindObjectsOfType<Scp079Generator>())
                    {
                        Generator.Get(generator);
                    }

                    foreach (Locker locker in Object.FindObjectsOfType<Locker>())
                    {
                        API.Features.Map.Locker.Get(locker);
                    }

                    ServerHandlers.OnWaitingForPlayers();
                });
            }
            catch (Exception e)
            {
                Log.Error("Error occured while handling internal Nebula wrappers! Full error -->\n" + e);
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
}