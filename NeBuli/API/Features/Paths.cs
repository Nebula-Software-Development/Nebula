using Nebuli.Loader;
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
    /// Gets the specific plugin-port directory.
    /// </summary>
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
    /// The <see cref="FileInfo"/> for the permissions file.
    /// </summary>
    public static FileInfo Permissions { get; private set; }

    internal static void LoadPaths()
    {
        MainDirectory = CreateDirectory(NwPaths.AppData, "Nebuli");
        PluginsDirectory = CreateDirectory(MainDirectory, "Plugins");

        if (LoaderClass.Configuration.SeperatePluginsByPort)
            PluginsPortDirectory = CreateDirectory(PluginsDirectory, Server.ServerPort + "-Plugins");
        else
            PluginsPortDirectory = PluginsDirectory;

        PluginConfigDirectory = CreateDirectory(MainDirectory, "Plugin-Configurations");
        PluginPortConfigDirectory = CreateDirectory(PluginConfigDirectory, Server.ServerPort.ToString());
        DependenciesDirectory = CreateDirectory(PluginsDirectory, "Dependencies");

        Permissions = new FileInfo(Path.Combine(MainDirectory.FullName, "Permissions.yml"));
    }

    /// <summary>
    /// Creates a directory with the specified name within the parent directory specified by <paramref name="parentPath"/>.
    /// </summary>
    /// <param name="parentPath">The path to the parent directory where the new directory will be created.</param>
    /// <param name="directoryName">The name of the new directory to create.</param>
    /// <returns>A <see cref="DirectoryInfo"/> representing the newly created directory.</returns>
    public static DirectoryInfo CreateDirectory(string parentPath, string directoryName)
    {
        string fullPath = Path.Combine(parentPath, directoryName);
        return Directory.CreateDirectory(fullPath);
    }

    /// <summary>
    /// Creates a directory with the specified name within the parent directory specified by <paramref name="parentDirectory"/>.
    /// </summary>
    /// <param name="parentDirectory">The parent directory where the new directory will be created.</param>
    /// <param name="directoryName">The name of the new directory to create.</param>
    /// <returns>A <see cref="DirectoryInfo"/> representing the newly created directory.</returns>
    public static DirectoryInfo CreateDirectory(DirectoryInfo parentDirectory, string directoryName)
    {
        string fullPath = Path.Combine(parentDirectory.FullName, directoryName);
        return Directory.CreateDirectory(fullPath);
    }
}