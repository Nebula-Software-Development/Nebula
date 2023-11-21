// -----------------------------------------------------------------------
// <copyright file=ReloadPerms.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using CommandSystem;
using Nebuli.API.Features;
using Nebuli.Permissions;

namespace Nebuli.API.Internal.Commands
{
    public class ReloadPerms : ICommand
    {
        public string Command { get; } = "permissions";

        public string[] Aliases { get; } = { "perm", "Permissions", "permission" };

        public string Description { get; } = "Reloads permissions.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            try
            {
                if (!sender.HasPermission("reloadpermission"))
                {
                    response = "No permission! Permission needed : 'reloadpermission'.";
                    return false;
                }

                Permissions.PermissionsHandler.LoadPermissions();
            }
            catch (Exception e)
            {
                Log.Error($"Error occured while reloading permissions! Full error --> \n{e}");
                response = e.Message;
                return false;
            }

            response = "Permissions reloaded!";
            return true;
        }
    }
}