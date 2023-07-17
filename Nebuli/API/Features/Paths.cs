using NwPaths = PluginAPI.Helpers.Paths;
using System.IO;

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
        PluginsDirectory = Directory.CreateDirectory(Path.Combine(MainDirectory.FullName, "Plugins"));
        DependenciesDirectory = Directory.CreateDirectory(Path.Combine(MainDirectory.FullName, "Dependencies"));
        PluginConfigDirectory = Directory.CreateDirectory(Path.Combine(PluginsDirectory.FullName, "PluginConfigs"));
    }

}
