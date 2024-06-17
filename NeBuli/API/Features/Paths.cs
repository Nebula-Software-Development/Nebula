// -----------------------------------------------------------------------
// <copyright file=Paths.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.IO;
using Nebula.Loader;
using NwPaths = PluginAPI.Helpers.Paths;

namespace Nebula.API.Features
{
    /// <summary>
    ///     A set of paths that the loader uses.
    /// </summary>
    public static class Paths
    {
        /// <summary>
        ///     The main directory for the framework.
        /// </summary>
        public static DirectoryInfo MainDirectory { get; } = CreateDirectory(NwPaths.AppData, "Nebula");

        /// <summary>
        ///     The plugin directory for the framework.
        /// </summary>
        public static DirectoryInfo PluginsDirectory { get; } = CreateDirectory(MainDirectory, "Plugins");

        /// <summary>
        ///     Gets the specific plugin-port directory.
        /// </summary>
        public static DirectoryInfo PluginsPortDirectory { get; }
            = LoaderClass.Configuration.SeperatePluginsByPort
                ? CreateDirectory(PluginsDirectory, Server.ServerPort + "-Plugins")
                : PluginsDirectory;

        /// <summary>
        ///     The dependency directory for the framework.
        /// </summary>
        public static DirectoryInfo DependenciesDirectory { get; } = CreateDirectory(PluginsDirectory, "Dependencies");

        /// <summary>
        ///     The config directory for the plugins.
        /// </summary>
        public static DirectoryInfo PluginConfigDirectory { get; } =
            CreateDirectory(MainDirectory, "Plugin-Configurations");

        /// <summary>
        ///     Gets the current port folder for the plugin configuration files.
        /// </summary>
        public static DirectoryInfo PluginPortConfigDirectory { get; } =
            CreateDirectory(PluginConfigDirectory, Server.ServerPort.ToString());

        /// <summary>
        ///     The <see cref="FileInfo" /> for the permissions file.
        /// </summary>
        public static FileInfo Permissions { get; } = new(Path.Combine(MainDirectory.FullName, "Permissions.yml"));

        /// <summary>
        ///     Creates a directory with the specified name within the parent directory specified by <paramref name="parentPath" />
        ///     .
        /// </summary>
        /// <param name="parentPath">The path to the parent directory where the new directory will be created.</param>
        /// <param name="directoryName">The name of the new directory to create.</param>
        /// <returns>A <see cref="DirectoryInfo" /> representing the newly created directory.</returns>
        public static DirectoryInfo CreateDirectory(string parentPath, string directoryName)
        {
            string fullPath = Path.Combine(parentPath, directoryName);
            return Directory.CreateDirectory(fullPath);
        }

        /// <summary>
        ///     Creates a directory with the specified name within the parent directory specified by
        ///     <paramref name="parentDirectory" />.
        /// </summary>
        /// <param name="parentDirectory">The parent directory where the new directory will be created.</param>
        /// <param name="directoryName">The name of the new directory to create.</param>
        /// <returns>A <see cref="DirectoryInfo" /> representing the newly created directory.</returns>
        public static DirectoryInfo CreateDirectory(DirectoryInfo parentDirectory, string directoryName)
        {
            string fullPath = Path.Combine(parentDirectory.FullName, directoryName);
            return Directory.CreateDirectory(fullPath);
        }
    }
}