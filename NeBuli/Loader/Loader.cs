using CommandSystem.Commands.Shared;
using HarmonyLib;
using Nebuli.API.Features;
using Nebuli.API.Interfaces;
using Nebuli.Events;
using PluginAPI.Core.Attributes;
using PluginAPI.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Nebuli.Loader;

#pragma warning disable CS1591

public class Loader
{
    private Harmony _harmony;
    private int pluginCount;
    private static bool _loaded = false;

    internal static void EDisablePlugins() => DisablePlugins();

    internal static Dictionary<Assembly, IConfig> _plugins = new();

    private static Dictionary<IPlugin<IConfig>, IConfig> PluginConfig = new();

    private static Dictionary<IConfig, string> configPaths = new();

    [PluginConfig]
    public static LoaderConfiguration Configuration;

    [PluginEntryPoint("Nebuli Loader", "0, 0, 0", "Nebuli Plugin Framework", "Nebuli Team")]
    [PluginPriority(LoadPriority.Highest)]
    public void FrameworkLoader()
    {
        if (!Configuration.LoaderEnabled)
        {
            Log.Info("Nebuli Loader is disabled, Nebuli will not load");
            return;
        }

        if (_loaded) return;
        else _loaded = true;

        Log.Info($"Nebuli Version {NebuliInfo.NebuliVersion} loading...", consoleColor: ConsoleColor.Red);

        SetupFilePaths();

        Log.Debug($"Dependency path is {Paths.DependenciesDirectory}");

        if (Configuration.ShouldCheckForUpdates)
        {
            Updater updater = new();
            updater.CheckForUpdates();
        }

        LoadDependencies(Paths.DependenciesDirectory.GetFiles("*.dll"));

        Log.Debug($"Plugin path is {Paths.PluginsDirectory}");

        LoadPlugins(Paths.PluginsDirectory.GetFiles("*.dll"));

        EventManager.RegisterBaseEvents();

        try
        {
            if (Configuration.PatchEvents)
            {
                _harmony = new("nebuli.patching.core");
                _harmony.PatchAll();
            }
            else
                Log.Info("Event patching is disabled, Events will not work");
        }
        catch (Exception e)
        {
            Log.Error($"A error has occured while patching! Full error: \n{e}");
        }
    }

    [PluginUnload]
    public void UnLoad()
    {
        _harmony.UnpatchAll(_harmony.Id);
        _harmony = null;

        EventManager.UnRegisterBaseEvents();
    }

    private void LoadDependencies(IEnumerable<FileInfo> files)
    {
        Log.Info("Loading dependencies...");

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

    private void SetupFilePaths()
    {
        Log.Debug("Loading file paths...");
        Paths.LoadPaths();
    }

    private void LoadPlugins(IEnumerable<FileInfo> files)
    {
        Log.Info("Loading plugins...");

        ISerializer serializer = new SerializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        IDeserializer deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();

        foreach (FileInfo file in files)
        {
            try
            {
                Assembly loadPlugin = Assembly.Load(File.ReadAllBytes(file.FullName));
                IPlugin<IConfig> newPlugin = NewPlugin(loadPlugin);

                if (newPlugin.NebulisVersion.Major < NebuliInfo.NebuliVersion.Major && !Configuration.LoadOutDatedPlugins && !newPlugin.SkipVersionCheck || newPlugin.NebulisVersion.Major > NebuliInfo.NebuliVersion.Major && !Configuration.LoadOutDatedPlugins && !newPlugin.SkipVersionCheck)
                {
                    Log.Warning($"{newPlugin.PluginName} is outdated and will not be loaded by Nebuli! (Plugin Version : {newPlugin.NebulisVersion}, Nebuli Version : {NebuliInfo.NebuliVersion})");
                    continue;
                }

                IConfig config = SetupPluginConfig(newPlugin, serializer, deserializer);

                if (!config.IsEnabled)
                {
                    continue;
                }

                Log.Info($"Plugin {newPlugin.PluginName}, by {newPlugin.PluginAuthor}, Version : {newPlugin.NebulisVersion}, has been succesfully enabled!");

                newPlugin.OnEnabled();

                _plugins.Add(loadPlugin, config);

                pluginCount++;
            }
            catch (Exception e)
            {
                Log.Error($"Failed to load plugin {file.Name}. Full error : \n{e}");
            }
        }
        Log.Info("Plugins loaded!");
        if (pluginCount > 0)
        {
            CustomNetworkManager.Modded = true;
            BuildInfoCommand.ModDescription = $"Framework : Nebuli\n Framework Version : {NebuliInfo.NebuliVersion}\n Copyright : Copyright (c) 2023 Nebuli Team";
        }

        Log.Info(Configuration.StartupMessage);
    }

    private static IPlugin<IConfig> NewPlugin(Assembly assembly)
    {
        try
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (!IsDerivedFromPlugin(type))
                    continue;

                Log.Debug($"Trying to create plugin instance for type: {type.Name}");
                IPlugin<IConfig> plugin = CreatePluginInstance(type);
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

    private static bool IsDerivedFromPlugin(Type type)
    {
        return typeof(IPlugin<IConfig>).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface;
    }

    private static IPlugin<IConfig> CreatePluginInstance(Type type)
    {
        ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
        if (constructor is not null)
        {
            Log.Debug($"Found constructor for type: {type.Name}");
            return constructor.Invoke(null) as IPlugin<IConfig>;
        }

        PropertyInfo pluginProperty = type.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).FirstOrDefault(property => property.PropertyType == type);

        if (pluginProperty is not null)
            Log.Debug($"Found plugin property for type: {type.Name}");
        else
        {
            Log.Error($"Nebuli will not load {type.Name}. No valid constructor or plugin property found for type: \n{type.Name}");
            return null;
        }

        return pluginProperty?.GetValue(null) as IPlugin<IConfig>;
    }

    private static IConfig SetupPluginConfig(IPlugin<IConfig> plugin, ISerializer serializer, IDeserializer deserializer)
    {
        string configPath = Path.Combine(Paths.PluginConfigDirectory.FullName, plugin.PluginName + "_Config.yml");
        try
        {
            if (!File.Exists(configPath))
            {
                Log.Warning($"{plugin.PluginName} does not have configs! Generating...");
                Log.Debug("Serializing new config and writing it to the path...");
                File.WriteAllText(configPath, serializer.Serialize(plugin.Config));
                configPaths.Add(plugin.Config, configPath);
                return plugin.Config;
            }
            else
            {
                Log.Debug($"Deserializing {plugin.PluginName} config at {configPath}...");
                IConfig config = (IConfig)deserializer.Deserialize(File.ReadAllText(configPath), plugin.Config.GetType());
                configPaths.Add(config, configPath);
                plugin.ReloadConfig(config);
                return config;
            }
        }
        catch (YamlException yame)
        {
            Log.Error($"A YamlException occured while loading {plugin.PluginName} configs! Full Error --> \n{yame}");
            return null;
        }
        catch (Exception e)
        {
            Log.Error($"Error while loading {plugin.PluginName} configs! Full Error --> \n{e}");
            return null;
        }
    }

    private static void ReloadConfigs()
    {
        IDeserializer deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        Log.Info("Reloading plugin configs...");

        foreach (IPlugin<IConfig> plugin in PluginConfig.Keys)
        {
            try
            {
                plugin.ReloadConfig((IConfig)deserializer.Deserialize(File.ReadAllText(configPaths[plugin.Config]), plugin.Config.GetType()));
                PluginConfig[plugin] = plugin.Config;
            }
            catch (Exception e)
            {
                Log.Error($"Error reloading config for plugin {plugin.PluginName}: {e.Message}");
            }
        }
    }

    private static void DisablePlugins()
    {
        foreach (IPlugin<IConfig> plugin in PluginConfig.Keys)
        {
            plugin.OnDisabled();
        }
    }
}