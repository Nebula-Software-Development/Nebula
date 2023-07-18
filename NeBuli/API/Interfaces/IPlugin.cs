using System;

namespace Nebuli.API.Interfaces;

/// <summary>
/// The default plugin interface for Nebuli.
/// </summary>
/// <typeparam name="TConfig">The plugins config.</typeparam>
public interface IPlugin<out TConfig> where TConfig : IConfig
{
    /// <summary>
    /// Gets the plugins name.
    /// </summary>
    string PluginName { get; }

    /// <summary>
    /// Gets the plugin's author.
    /// </summary>
    string PluginAuthor { get; }
    /// <summary>
    /// Gets the plugins current version.
    /// </summary>
    Version Version { get; }

    /// <summary>
    /// Gets the plugins current Nebulis version.
    /// </summary>
    Version NebulisVersion { get; }

    /// <summary>
    /// If true, skips checking if the plugins current Nebulis version lines up with the Nebulis version loading the plugin.
    /// </summary>
    bool SkipVersionCheck { get; }

    /// <summary>
    /// Called after loading the plugin succesfully.
    /// </summary>
    void OnEnabled();

    /// <summary>
    /// Called after disabling the plugin.
    /// </summary>
    void OnDisabled();

    /// <summary>
    /// The plugins config.
    /// </summary>
    TConfig Config { get; }
}