using Nebuli.API.Features;
using Nebuli.API.Interfaces;
using System;

namespace Nebuli.API.Features;

/// <summary>
/// Represents a base class for plugins in the Nebuli framework.
/// </summary>
/// <typeparam name="TConfig">The configuration type for the plugin.</typeparam>
public abstract class Plugin<TConfig> : IPlugin<TConfig> where TConfig : IConfig, new()
{
    /// <summary>
    /// Gets the plugins name.
    /// </summary>
    public abstract string PluginName { get; }

    /// <summary>
    /// Gets the plugin's author.
    /// </summary>
    public abstract string PluginAuthor { get; }

    /// <summary>
    /// Gets the plugins current version.
    /// </summary>
    public abstract Version Version { get; }

    /// <summary>
    /// Gets the plugins current Nebulis version.
    /// </summary>
    public abstract Version NebulisVersion { get; }

    /// <summary>
    /// If true, skips checking if the plugins current Nebulis version lines up with the Nebulis version loading the plugin.
    /// </summary>
    public abstract bool SkipVersionCheck { get; }

    /// <summary>
    /// The plugins config.
    /// </summary>
    public TConfig Config { get; internal set; } = new TConfig();

    /// <summary>
    /// Called after loading the plugin succesfully.
    /// </summary>
    public virtual void OnEnabled()
    {
    }

    /// <summary>
    /// Called after disabling the plugin.
    /// </summary>
    public virtual void OnDisabled()
    {
    }

    /// <summary>
    /// Reloads the plugin's config.
    /// </summary>
    public void ReloadConfig(IConfig config)
    {
        Config = (TConfig)config;
    }
}
