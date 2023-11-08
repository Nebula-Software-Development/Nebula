// -----------------------------------------------------------------------
// <copyright file=LoaderClass.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using CommandSystem.Commands.Shared;
using HarmonyLib;
using MEC;
using Nebuli.API.Features;
using Nebuli.API.Features.Pools;
using Nebuli.API.Interfaces;
using Nebuli.Events;
using Nebuli.Loader.CustomConverters;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Nebuli.Loader;

/// <summary>
/// Nebuli's loader class for loading itself, plugins, 
/// </summary>
public class LoaderClass
{
    private Harmony _harmony;
    private static bool _loaded = false;

    /// <summary>
    /// Gets the public static instance of <see cref="LoaderClass"/>.
    /// </summary>
    public static LoaderClass LoaderInstance { get; private set; } = null;

    /// <summary>
    /// Gets a shared instance of the loaders random number generator.
    /// </summary>
    public static Random Random { get; } = new();

    /// <summary>
    /// Gets a static instance of Nebuli's <see cref="Assembly"/>.
    /// </summary>
    public static Assembly NebuliAssembly { get; } = typeof(LoaderClass).Assembly;

    /// <summary>
    /// Gets the loaders <see cref="ISerializer"/>.
    /// </summary>
    public static ISerializer Serializer { get; set; } = new SerializerBuilder()
        .WithTypeConverter(new CustomVectorsConverter())
        .WithEmissionPhaseObjectGraphVisitor((EmissionPhaseObjectGraphVisitorArgs visitor) => new CommentsObjectGraphVisitor(visitor.InnerVisitor)).WithTypeInspector((ITypeInspector typeInspector) => new CommentGatheringTypeInspector(typeInspector))
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .WithNamingConvention(PascalCaseNamingConvention.Instance)
        .WithNamingConvention(HyphenatedNamingConvention.Instance)
        .DisableAliases()
        .IgnoreFields()
        .Build();

    /// <summary>
    /// Gets the loaders <see cref="IDeserializer"/>.
    /// </summary>
    public static IDeserializer Deserializer { get; set; } = new DeserializerBuilder()
        .WithTypeConverter(new CustomVectorsConverter())
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .WithNamingConvention(PascalCaseNamingConvention.Instance)
        .WithNamingConvention(HyphenatedNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        .IgnoreFields()
        .Build();

    /// <summary>
    /// Gets a dictionary off all loaded <see cref="Assembly"/> with their <see cref="IPlugin{TConfig}"/>.
    /// </summary>
    public static Dictionary<Assembly, IPlugin<IConfiguration>> Plugins { get; private set; } = new();

    /// <summary>
    /// Gets a list of all succesfully enabled plugins.
    /// </summary>
    public static List<IPlugin<IConfiguration>> EnabledPlugins { get; private set; } = new();

    [PluginConfig]
    internal static LoaderConfiguration Configuration;

    [PluginEntryPoint("Nebuli Loader", NebuliInfo.NebuliVersionConst, "Nebuli Plugin Framework", "Nebuli Team")]
    [PluginPriority(LoadPriority.Highest)]
    internal void FrameworkLoader()
    {
        LoaderInstance = this;

        if (!Configuration.LoaderEnabled)
        {
            Log.Info("Nebuli Loader is disabled, Nebuli will not load.", consoleColor: ConsoleColor.Red, prefix: "Loader");
            return;
        }

        if (_loaded) return;
        else _loaded = true;

        Log.Info($"Nebuli Version {NebuliInfo.NebuliVersion} loading...", consoleColor: ConsoleColor.Red, prefix: "Loader");       

        if (Configuration.ShouldCheckForUpdates) 
            Updater.CheckForUpdates();

        LoadDependencies(Paths.DependenciesDirectory.GetFiles("*.dll"));      

        LoadPlugins(Paths.PluginsPortDirectory.GetFiles("*.dll"));

        EventManager.RegisterBaseEvents();

        try
        {
            if (Configuration.PatchEvents)
            {
                _harmony = new("nebuli.patching.core");
                _harmony.PatchAll(NebuliAssembly);
            }
            else
                Log.Warning("Event patching is disabled, Events will not work!");
        }
        catch (Exception e)
        {
            Log.Error($"A error has occured while patching! Full error: \n{e}", "Patching");
        }      

        CustomNetworkManager.Modded = true;
        BuildInfoCommand.ModDescription = $"Framework : Nebuli\nFramework Version : {NebuliInfo.NebuliVersion}\nCopyright : Copyright (c) 2023 Nebuli Team";

        Log.Info("Welcome to... \r\n███╗░░██╗███████╗██████╗░██╗░░░██╗██╗░░░░░██╗\r\n████╗░██║██╔════╝██╔══██╗██║░░░██║██║░░░░░██║\r\n██╔██╗██║█████╗░░██████╦╝██║░░░██║██║░░░░░██║\r\n██║╚████║██╔══╝░░██╔══██╗██║░░░██║██║░░░░░██║\r\n██║░╚███║███████╗██████╦╝╚██████╔╝███████╗██║\r\n╚═╝░░╚══╝╚══════╝╚═════╝░░╚═════╝░╚══════╝╚═╝");
    }

    [PluginUnload]
    internal void UnLoad()
    {
        DisablePlugins();
        _loaded = false;
        _harmony?.UnpatchAll(_harmony.Id);
        _harmony = null;
        EventManager.UnRegisterBaseEvents();
        LoaderInstance = null;
    }

    private void LoadDependencies(IEnumerable<FileInfo> files)
    {
        Log.Info($"Loading dependencies from {Paths.DependenciesDirectory.FullName}");

        foreach (FileInfo file in files)
        {
            try
            {
                Assembly assembly = Assembly.Load(File.ReadAllBytes(file.FullName));
                Log.Info($"Dependency {assembly.GetName().Name} loaded!");
            }
            catch (Exception e)
            {
                Log.Error($"Failed to load dependency {file.Name}. Full error : \n{e}");
            }
        }
        Log.Info("Dependencies loaded!");
    }

    private void LoadPlugins(IEnumerable<FileInfo> files)
    {
        Log.Info($"Loading plugins from {Paths.PluginsPortDirectory.FullName}");

        List<IPlugin<IConfiguration>> pluginsToLoad = ListPool<IPlugin<IConfiguration>>.Instance.Rent();

        EnabledPlugins.Clear();
        Plugins.Clear();

        foreach (FileInfo file in files)
        {
            try
            {
                Assembly loadPlugin = Assembly.Load(File.ReadAllBytes(file.FullName));
                IPlugin<IConfiguration> newPlugin = NewPlugin(loadPlugin);
                newPlugin.Assembly = loadPlugin;
                pluginsToLoad.Add(newPlugin);
            }
            catch (Exception e)
            {
                Log.Error($"Failed to load plugin {file.Name}. Full error : \n{e}");
            }
        }

        pluginsToLoad.Sort((p1, p2) => p1.LoadOrder.CompareTo(p2.LoadOrder));

        foreach (IPlugin<IConfiguration> plugin in pluginsToLoad)
        {
            try
            {
                if (IsPluginOutdated(plugin))
                    continue;

                IConfiguration config = SetupPluginConfig(plugin);

                if (!config.IsEnabled)
                    continue;

                plugin.LoadCommands();
                plugin.OnEnabled();

                string pluginName = string.IsNullOrEmpty(plugin.Name) ? plugin.Assembly.GetName().Name : plugin.Name;

                Log.Info($"Plugin '{pluginName}' by '{plugin.Creator}', (v{plugin.Version}), has been successfully enabled!");

                Plugins.Add(plugin.Assembly, plugin);
                EnabledPlugins.Add(plugin);
            }
            catch (Exception e)
            {
                Log.Error($"Failed to load plugin {plugin.Name}. Full error : \n{e}");
            }
        }

        Log.Info("Plugins loaded!");
        ListPool<IPlugin<IConfiguration>>.Instance.Return(pluginsToLoad);
    }

    private static IPlugin<IConfiguration> NewPlugin(Assembly assembly)
    {
        try
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (!IsDerivedFromPlugin(type))
                    continue;

                Log.Debug($"Trying to create plugin instance for type: {type.Name}");
                IPlugin<IConfiguration> plugin = CreatePluginInstance(type);
                if (plugin is not null)
                {
                    Log.Debug($"Plugin instance created successfully for type: {type.Name}");
                    return plugin;
                }
            }

            Log.Warning($"No valid plugin instance found in assembly: {assembly.GetName().Name}");
            return null;
        }
        catch (Exception e)
        {
            Log.Error($"Failed loading {assembly.GetName().Name}! Full error : \n{e}");
            return null;
        }
    }

    private static bool IsDerivedFromPlugin(Type type) => typeof(IPlugin<IConfiguration>).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface;

    private bool IsPluginOutdated(IPlugin<IConfiguration> plugin)
    {
        if (!Configuration.LoadOutDatedPlugins && !plugin.SkipVersionCheck)
        {
            switch (plugin.NebuliVersion.Major.CompareTo(NebuliInfo.NebuliVersion.Major))
            {
                case -1:
                    Log.Warning($"{plugin.Name} is outdated and will not be loaded by Nebuli! (Plugin Version: {plugin.NebuliVersion} | Nebuli Version: {NebuliInfo.NebuliVersion})");
                    return true;

                case 0:
                    return false;

                case 1:
                    Log.Warning($"Nebuli is outdated! Please update Nebuli because it can cause plugin issues! ({plugin.Name} Version: {plugin.NebuliVersion} | Nebuli Version: {NebuliInfo.NebuliVersion})");
                    return false;
            }
        }
        return false;
    }

    private static IPlugin<IConfiguration> CreatePluginInstance(Type type)
    {
        ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
        if (constructor is not null)
        {
            Log.Debug($"Found constructor for type: {type.Name}");
            return constructor.Invoke(null) as IPlugin<IConfiguration>;
        }

        PropertyInfo pluginProperty = type.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).FirstOrDefault(property => property.PropertyType == type);

        if (pluginProperty is not null)
            Log.Debug($"Found plugin property for type: {type.Name}");
        else
        {
            Log.Error($"Nebuli will not load {type.Name}. No valid constructor or plugin property found for type: \n{type.Name}");
            return null;
        }

        return pluginProperty?.GetValue(null) as IPlugin<IConfiguration>;
    }

    private static IConfiguration SetupPluginConfig(IPlugin<IConfiguration> plugin)
    {
        string configPath = Path.Combine(Paths.PluginPortConfigDirectory.FullName,
            string.IsNullOrEmpty(plugin.Name) ? plugin.Assembly.GetName().Name + $"-({Server.ServerPort})-Config.yml"
            : plugin.Name + $"-({Server.ServerPort})-Config.yml");
        try
        {
            if (!File.Exists(configPath))
            {
                Log.Warning($"{plugin.Name} does not have configs! Generating...");
                Log.Debug("Serializing new config and writing it to the path...");
                File.WriteAllText(configPath, Serializer.Serialize(plugin.Config));
                plugin.ConfigPath = configPath;
                return plugin.Config;
            }
            else
            {
                Log.Debug($"Deserializing {plugin.Name} config at {configPath}...");
                IConfiguration config = (IConfiguration)Deserializer.Deserialize(File.ReadAllText(configPath), plugin.Config.GetType());
                plugin.ConfigPath = configPath;
                plugin.ReloadConfig(config);
                return config;
            }
        }
        catch (YamlException yame)
        {
            Log.Error($"A YamlException occured while loading {plugin.Name} configs! Default configs will be used! Full Error --> \n{yame}");
            return plugin.Config;
        }
        catch (Exception e)
        {
            Log.Error($"Error while loading {plugin.Name} configs! Default configs will be used! Full Error --> \n{e}");
            return plugin.Config;
        }
    }

    internal static void ReloadConfigs()
    {
        Log.Info("Reloading plugin configs...");
        foreach (IPlugin<IConfiguration> plugin in EnabledPlugins)
        {
            try
            {
                Log.Info($"Reloading plugin configs for {plugin.Name}...");
                if (!File.Exists(plugin.ConfigPath))
                    SetupPluginConfig(plugin);
                IConfiguration newConfig = (IConfiguration)Deserializer.Deserialize(File.ReadAllText(plugin.ConfigPath), plugin.Config.GetType());
                plugin.ReloadConfig(newConfig);
            }
            catch (Exception e)
            {
                Log.Error($"Error reloading config for plugin {plugin.Name}: {e.Message}");
            }
        }
    }

    internal void ReloadPlugins()
    {
        DisablePlugins();
        LoadDependencies(Paths.DependenciesDirectory.GetFiles("*.dll"));
        LoadPlugins(Paths.PluginsPortDirectory.GetFiles("*.dll"));
    }

    internal static void DisablePlugins()
    {
        foreach (IPlugin<IConfiguration> plugin in EnabledPlugins)
        {
            plugin.OnDisabled();
            plugin.UnLoadCommands();
        }
        EnabledPlugins.Clear();
        Plugins.Clear();
    }
}