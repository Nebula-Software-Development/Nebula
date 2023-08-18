using Nebuli.API.Features;
using System.Collections.Generic;
using System.IO;

namespace Nebuli.Loader;

public static class PermissionHandler
{
    public static Dictionary<string, List<string>> GroupPermissions { get; set; } = new();

    internal static void LoadPermissionHandler()
    {
        foreach (string userGroup in ServerStatic.PermissionsHandler.GetAllGroupsNames())
        {
            GroupPermissions.Add(userGroup, new List<string>());
        }

        if (!File.Exists(Paths.Permissions.FullName))
        {
            string serializationString = Loader.Serializer.Serialize(GroupPermissions);
            File.WriteAllText(Paths.Permissions.FullName, serializationString);
        }

        Dictionary<string, List<string>> deserializedDict = Loader.Deserializer.Deserialize<Dictionary<string, List<string>>>(File.ReadAllText(Paths.Permissions.FullName));

        foreach (KeyValuePair<string, List<string>> kvp in deserializedDict)
        {
            string groupName = kvp.Key;
            List<string> permissions = kvp.Value;
          
            if (!ServerStatic.PermissionsHandler.GetAllGroupsNames().Contains(groupName))
            {
                Log.Error(groupName + " is not a valid UserGroup! Skipping...");
                continue;
            }
            GroupPermissions[groupName] = permissions;
        }
    }

    //public static bool HasPermission(this ICommandSender commandSender, string permission) => HasPermission(NebuliPlayer.Get(commandSender), permission);

    /*public static bool HasPermission(this NebuliPlayer player, string permission)
    {
        if (player.ReferenceHub == ReferenceHub.HostHub)
            return true;

        if (player is null || player.Group is null)
            return false;

        
       
    }*/
}

