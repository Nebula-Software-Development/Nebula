using CommandSystem;
using Nebuli.API.Features;
using Nebuli.Loader;
using Nebuli.Permissions;
using System;

namespace Nebuli.API.Internal.Commands;

public class ReloadPlugins : ICommand
{
    public string Command { get; } = "plugins";

    public string[] Aliases { get; } = new[] { "plugin" };

    public string Description { get; } = "Reloads plugins.";

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