// -----------------------------------------------------------------------
// <copyright file=Reload.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using CommandSystem;

namespace Nebula.API.Internal.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Reload : ParentCommand
    {
        public Reload()
        {
            LoadGeneratedCommands();
        }

        public override string Command { get; } = "reload";

        public override string[] Aliases { get; } = { "rld" };

        public override string Description { get; } = "Allows easier reloading of plugin-specific stuff.";

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new ReloadConfigurations());
            RegisterCommand(new ReloadPlugins());
            RegisterCommand(new ReloadPerms());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender,
            out string response)
        {
            response = "Invalid subcommand! Valid subcommands : configs, permissions, plugins";
            return false;
        }
    }
}