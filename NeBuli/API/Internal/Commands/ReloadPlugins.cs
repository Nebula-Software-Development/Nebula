// -----------------------------------------------------------------------
// <copyright file=ReloadPlugins.cs company="Nebula-Software-Development">
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
    public class ReloadPlugins : ICommand
    {
        public string Command { get; } = "plugins";

        public string[] Aliases { get; } = { "plugin" };

        public string Description { get; } = "Reloads plugins.";

        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            try
            {
                if (!sender.HasPermission("reloadplugins"))
                {
                    response = "No permission! Permission needed : 'reloadplugins'.";
                    return false;
                }

                LoaderClass.LoaderInstance.ReloadPlugins();
            }
            catch (Exception e)
            {
                Log.Error($"Error occured while reloading plugins! Full error --> \n{e}");
                response = e.Message;
                return false;
            }

            response = "Plugins reloaded!";
            return true;
        }
    }
}