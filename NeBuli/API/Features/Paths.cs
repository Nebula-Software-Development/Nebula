using System.IO;
using NwPaths = PluginAPI.Helpers.Paths;

namespace Nebuli.API.Features;

/// <summary>
/// A set of paths that the loader uses.
/// </summary>
public static class Paths
{
    /// <summary>
    /// The main directory for the framework.
    /// </summary>
    public static DirectoryInfo MainDirectory { get; private set; }

    /// <summary>
    /// The plugin directory for the framework.
    /// </summary>
    public static DirectoryInfo PluginsDirectory { get; private set; }

    /// <summary>
    /// The dependency directory for the framework.
    /// </summary>
    public static DirectoryInfo DependenciesDirectory { get; private set; }

    /// <summary>
    /// The config directory for the plugins.
    /// </summary>
    public static DirectoryInfo PluginConfigDirectory { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public static FileInfo Configs { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public static FileInfo Permissions { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public static void LoadPaths()
    {
        MainDirectory = Directory.CreateDirectory(Path.Combine(NwPaths.AppData, "Nebuli"));
        PluginsDirectory = MainDirectory.CreateSubdirectory("Plugins");
        PluginConfigDirectory = MainDirectory.CreateSubdirectory("PluginConfigs");
        DependenciesDirectory = PluginsDirectory.CreateSubdirectory("Dependencies");
        Configs = new FileInfo(Path.Combine(MainDirectory.FullName, "Configuration.yml"));
        Permissions = new FileInfo(Path.Combine(MainDirectory.FullName, "Permissions.yml"));
    }
}