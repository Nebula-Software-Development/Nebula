using NwPaths = PluginAPI.Helpers.Paths;
using System.IO;

namespace Nebuli.API.Features;

public static class Paths
{
    public static DirectoryInfo MainDirectory { get; private set; }
    
    public static DirectoryInfo PluginsDirectory { get; private set; }

    public static DirectoryInfo DependenciesDirectory { get; private set; }
    
    public static FileInfo Configs { get; private set; }
    
    public static FileInfo Permissions { get; private set; }

    public static void LoadPaths()
    {
        MainDirectory = Directory.CreateDirectory(Path.Combine(NwPaths.SecretLab, "NeBuli"));
    }
}