using CommandSystem;
using Nebuli.API.Features;
using Nebuli.Permissions;
using System;

namespace Nebuli.API.Internal.Commands;

public class ReloadConfigurations : ICommand
{
    public string Command => "configs";

    public string[] Aliases => Array.Empty<string>();

    public string Description => "Config reloading.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        try
        {
            if (!sender.HasPermission("reloadconfig"))
            {
                response = "No permission! Permission needed : 'reloadconfig'.";
                return false;
            }

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
