﻿using CommandSystem;
using System;

namespace Nebuli.API.Internal.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class Reload : ParentCommand
{
    public Reload() => LoadGeneratedCommands();

    public override string Command { get; } = "reload";

    public override string[] Aliases { get; } = new[] { "rld" };

    public override string Description { get; } = "Allows easier reloading of plugin-specific stuff.";

    public override void LoadGeneratedCommands()
    {
        RegisterCommand(new ReloadConfigurations());
        RegisterCommand(new ReloadPlugins());
        RegisterCommand(new ReloadPerms());       
    }

    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = "Invalid subcommand! Valid subcommands : configs, permissions, plugins";
        return false;
    }
}

