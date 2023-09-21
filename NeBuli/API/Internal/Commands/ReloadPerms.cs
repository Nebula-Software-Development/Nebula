using CommandSystem;
using Nebuli.API.Features;
using Nebuli.Permissions;
using System;

namespace Nebuli.API.Internal.Commands;

public class ReloadPerms : ICommand
{
    public string Command { get; } = "permissions";

    public string[] Aliases { get; } = new[] { "perm", "Permissions", "permission" };

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