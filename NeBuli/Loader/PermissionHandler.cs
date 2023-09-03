using CommandSystem;
using Nebuli.API.Features;
using Nebuli.API.Features.Player;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nebuli.Permissions;

public static class PermissionsHandler
{
    public static Dictionary<string, Group> Groups { get; internal set; } = new();

    public static void LoadPermissions()
    {
        try
        {
            if(!File.Exists(Paths.Permissions.FullName))
            {
                GenerateDefaultPermissionsFile(Paths.Permissions.FullName);
            }

            PermissionsConfig permissionsConfig = Loader.Loader.Deserializer.Deserialize<PermissionsConfig>(File.ReadAllText(Paths.Permissions.FullName));

            foreach(string group in permissionsConfig.Permissions.Keys.ToList())
            {
                if (string.Equals(group, "user", StringComparison.OrdinalIgnoreCase) || ServerStatic.PermissionsHandler._groups.ContainsKey(group))
                    continue;
                Log.Error($"{group} is not a valid permission group!", "Permissions");
                permissionsConfig.Permissions.Remove(group);
            }

            if (permissionsConfig?.Permissions != null)
            {
                Groups.Clear();
                foreach (var kvp in permissionsConfig.Permissions)
                {
                    kvp.Value.GroupName = kvp.Key;
                    Groups.Add(kvp.Key, kvp.Value);
                }            
            }

        }
        catch (Exception e)
        {
            Log.Error($"Failed to load permissions from file: {e.Message}", "Permissions");
        }
    }

    public static void SavePermissions()
    {
        PermissionsConfig permissionsConfig = new()
        {
            Permissions = Groups
        };

        try
        {

            string yaml = Loader.Loader.Serializer.Serialize(permissionsConfig);
            File.WriteAllText(Paths.Permissions.FullName, yaml);
        }
        catch (Exception e)
        {
            Log.Error($"Failed to save permissions to file: {e.Message}", "Permissions");
        }
    }

    public static bool HasPermission(this NebuliPlayer ply, string permission) => HasPermission(ply.Sender, permission);

    public static bool HasPermission(this ICommandSender commandSender, string permission)
    {
        if(commandSender is ServerConsoleSender) return true;
        if (!NebuliPlayer.TryGet(commandSender, out NebuliPlayer ply)) return false; 
        if (ply.ReferenceHub == ReferenceHub.HostHub || 
            Groups.TryGetValue(ply.GroupName, out var playerGroup) && 
            (playerGroup.Permissions.Contains(".*") || playerGroup.Permissions.Contains(permission)))
        {
            return true;
        }
        return false;
    }

    public static void GenerateDefaultPermissionsFile(string filePath)
    {
        PermissionsConfig defaultPermissionsConfig = new()
        {
            Permissions = new Dictionary<string, Group>
                {
                    { "user", new Group { Default = true, Permissions = new List<string> { "" } } },
                    { "owner", new Group { Permissions = new List<string> { ".*" } } },
                    { "admin", new Group { Permissions = new List<string> { "testplugin.admin", "testplugin.*" } } },
                    { "moderator", new Group { Permissions = new List<string> { "testplugin.moderator" } } }
                }
        };

        try
        {            
            File.WriteAllText(filePath, Loader.Loader.Serializer.Serialize(defaultPermissionsConfig));
        }
        catch (Exception e)
        {
            Log.Error($"Failed to generate default permissions file: {e.Message}", "Permissions");
        }
    }
}

public class Group
{
    public string GroupName { get; set; } = string.Empty;
    public bool Default { get; set; } = false;
    public List<string> Permissions { get; set; } = new List<string>();
}

public class PermissionsConfig
{
    public Dictionary<string, Group> Permissions { get; set; }
}