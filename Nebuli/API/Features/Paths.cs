using System.IO;

namespace Nebuli.API.Features;

public static class Paths
{
    public static DirectoryInfo MainDict { get; private set; }
    
    public static DirectoryInfo PluginsDict { get; private set; }

    public static DirectoryInfo DepsDict { get; private set; }
    
    public static FileInfo Configs { get; private set; }
    
    public static FileInfo Permissions { get; private set; }

    public static void LoadPaths()
    {
        // Set the paths to the desired directories
    }
}