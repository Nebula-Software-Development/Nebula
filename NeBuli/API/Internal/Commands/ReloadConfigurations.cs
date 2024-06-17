// -----------------------------------------------------------------------
// <copyright file=ReloadConfigurations.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using CommandSystem;
using Nebula.API.Features;
using Nebula.Loader;
using Nebula.Permissions;

namespace Nebula.API.Internal.Commands
{
    public class ReloadConfigurations : ICommand
    {
        public string Command => "configs";

        public string[] Aliases => Array.Empty<string>();

        public string Description => "Config reloading.";

        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            try
            {
                if (!sender.HasPermission("reloadconfig"))
                {
                    response = "No permission! Permission needed : 'reloadconfig'.";
                    return false;
                }

                LoaderClass.ReloadConfigs();
            }
            catch (Exception e)
            {
                Log.Error($"Error occured while reloading plugin configs! Full error --> \n{e}");
                response = e.Message;
                return false;
            }

            response = "Plugin configs reloaded!";
            return true;
        }
    }
}