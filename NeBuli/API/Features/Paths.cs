using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using NwPaths = PluginAPI.Helpers.Paths;
using System.Collections.Generic;

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

    public static DirectoryInfo PluginsPortDirectory { get; private set; }

    /// <summary>
    /// The dependency directory for the framework.
    /// </summary>
    public static DirectoryInfo DependenciesDirectory { get; private set; }

    /// <summary>
    /// The config directory for the plugins.
    /// </summary>
    public static DirectoryInfo PluginConfigDirectory { get; private set; }


    /// <summary>
    /// Gets the current port folder for the plugin configuration files.
    /// </summary>
    public static DirectoryInfo PluginPortConfigDirectory { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public static FileInfo Permissions { get; private set; }

    /// <summary>
    ///
    /// </summary>
    internal static void LoadPaths()
    {
        MainDirectory = Directory.CreateDirectory(Path.Combine(NwPaths.AppData, "Nebuli"));
        PluginsDirectory = MainDirectory.CreateSubdirectory("Plugins");
        PluginsPortDirectory = PluginsDirectory.CreateSubdirectory(Server.ServerPort.ToString() + "-Plugins");
        PluginConfigDirectory = MainDirectory.CreateSubdirectory("Plugin-Configurations");
        PluginPortConfigDirectory = PluginConfigDirectory.CreateSubdirectory(Server.ServerPort.ToString());
        DependenciesDirectory = PluginsDirectory.CreateSubdirectory("Dependencies");
        Permissions = new FileInfo(Path.Combine(MainDirectory.FullName, "Permissions.yml"));       
    }
}