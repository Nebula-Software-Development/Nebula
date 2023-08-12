using CommandSystem;
using Nebuli.API.Features;
using System;

namespace Nebuli.API.Internal.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class Reload : ICommand
{
    public string Command => "Reload";

    public string[] Aliases { get; } = new[] { "rld, reload" };

    public string Description => "Reloads all plugin configs.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        try
        {
            Loader.Loader.ReloadConfigs();
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

