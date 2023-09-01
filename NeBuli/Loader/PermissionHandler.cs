using CommandSystem;
using Nebuli.API.Features;
using System;
using System.Collections.Generic;
using System.IO;

namespace Nebuli.Loader;

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

            PermissionsConfig permissionsConfig = Loader.Deserializer.Deserialize<PermissionsConfig>(File.ReadAllText(Paths.Permissions.FullName));

            foreach(string group in permissionsConfig.Permissions.Keys)
            {
                if (string.Equals(group, "user", StringComparison.OrdinalIgnoreCase) || ServerStatic.PermissionsHandler._groups.ContainsKey(group))
                    continue;
                Log.Info($"{group} is not a valid permission group!", "Permissions");
                permissionsConfig.Permissions.Remove(group);
            }

            if (permissionsConfig?.Permissions != null)
                Groups = permissionsConfig.Permissions;
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

            string yaml = Loader.Serializer.Serialize(permissionsConfig);
            File.WriteAllText(Paths.Permissions.FullName, yaml);
        }
        catch (Exception e)
        {
            Log.Error($"Failed to save permissions to file: {e.Message}", "Permissions");
        }
    }

    public static void GenerateDefaultPermissionsFile(string filePath)
    {
        PermissionsConfig defaultPermissionsConfig = new()
        {
            Permissions = new Dictionary<string, Group>
                {
                    { "user", new Group { Default = true, Permissions = new List<string> { } } },
                    { "owner", new Group { Permissions = new List<string> { ".*" } } },
                    { "admin", new Group { Permissions = new List<string> { "testplugin.admin", "testplugin.*" } } },
                    { "moderator", new Group { Permissions = new List<string> { "testplugin.moderator" } } }
                }
        };

        try
        {            
            File.WriteAllText(filePath, Loader.Serializer.Serialize(defaultPermissionsConfig));
        }
        catch (Exception e)
        {
            Log.Error($"Failed to generate default permissions file: {e.Message}", "Permissions");
        }
    }
}

public class Group
{
    public bool Default { get; set; }
    public List<string> Permissions { get; set; } = new List<string>();
}

public class PermissionsConfig
{
    public Dictionary<string, Group> Permissions { get; set; }
}