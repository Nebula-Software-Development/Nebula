using NwPaths = PluginAPI.Helpers.Paths;
using System.IO;
using System;

namespace Nebuli.API.Features;

public static class Paths
{
    public static DirectoryInfo MainDirectory { get; private set; }
    
    public static DirectoryInfo PluginsDirectory { get; private set; }

    public static DirectoryInfo DependenciesDirectory { get; private set; }

    public static DirectoryInfo PluginConfigDirectory { get; private set; }
    
    public static FileInfo Configs { get; private set; }
    
    public static FileInfo Permissions { get; private set; }

    public static void LoadPaths()
    {
        MainDirectory = Directory.CreateDirectory(Path.Combine(NwPaths.SecretLab, "Nebuli"));
        PluginsDirectory = MainDirectory.CreateSubdirectory("Plugins");
        PluginConfigDirectory = PluginConfigDirectory.CreateSubdirectory("PluginConfigs");
        DependenciesDirectory = PluginsDirectory.CreateSubdirectory("Dependencies");
        Configs = new FileInfo(Path.Combine(MainDirectory.FullName, "Configuration.yml"));
        Permissions = new FileInfo(Path.Combine(MainDirectory.FullName, "Permissions.yml"));
    }
}
