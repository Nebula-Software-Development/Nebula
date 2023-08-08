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

    public static ISerializer Serializer { get; private set; }
    public static IDeserializer Deserializer { get; private set; }

    internal static void EDisablePlugins() => DisablePlugins();

    internal static Dictionary<Assembly, IConfiguration> _plugins = new();

    private static Dictionary<IPlugin<IConfiguration>, IConfiguration> PluginConfig = new();

    private static Dictionary<IConfiguration, string> configPaths = new();

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
        foreach (IPlugin<IConfiguration> plugin in PluginConfig.Keys)
        {
            plugin.OnDisabled();
        }

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

        Serializer = new SerializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        Deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();

        foreach (FileInfo file in files)
        {
            try
            {
                Assembly loadPlugin = Assembly.Load(File.ReadAllBytes(file.FullName));
                IPlugin<IConfiguration> newPlugin = NewPlugin(loadPlugin);

                if (IsPluginOutdated(newPlugin))
                {
                    continue;
                }

                IConfiguration config = SetupPluginConfig(newPlugin, Serializer, Deserializer);

                if (!config.IsEnabled)
                {
                    continue;
                }

                Log.Info($"Plugin '{newPlugin.Name}' by '{newPlugin.Creator}', (v{newPlugin.Version}), has been successfully enabled!");

                newPlugin.LoadCommands();
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
            BuildInfoCommand.ModDescription = $"Framework : Nebuli\nFramework Version : {NebuliInfo.NebuliVersion}\nCopyright : Copyright (c) 2023 Nebuli Team";
        }

        Log.Info(Configuration.StartupMessage);
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

    private static bool IsDerivedFromPlugin(Type type)
    {
        return typeof(IPlugin<IConfiguration>).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface;
    }

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
                    Log.Warning($"Nebuli is outdated! Please update Nebuli because it can cause plugin issues! (Plugin Version: {plugin.NebuliVersion} | Nebuli Version: {NebuliInfo.NebuliVersion})");
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

    private static IConfiguration SetupPluginConfig(IPlugin<IConfiguration> plugin, ISerializer serializer, IDeserializer deserializer)
    {
        string configPath = Path.Combine(Paths.PluginConfigDirectory.FullName, plugin.Name + "_Config.yml");
        try
        {
            if (!File.Exists(configPath))
            {
                Log.Warning($"{plugin.Name} does not have configs! Generating...");
                Log.Debug("Serializing new config and writing it to the path...");
                File.WriteAllText(configPath, serializer.Serialize(plugin.Config));
                configPaths.Add(plugin.Config, configPath);
                return plugin.Config;
            }
            else
            {
                Log.Debug($"Deserializing {plugin.Name} config at {configPath}...");
                IConfiguration config = (IConfiguration)deserializer.Deserialize(File.ReadAllText(configPath), plugin.Config.GetType());
                configPaths.Add(config, configPath);
                plugin.ReloadConfig(config);
                return config;
            }
        }
        catch (YamlException yame)
        {
            Log.Error($"A YamlException occured while loading {plugin.Name} configs! Full Error --> \n{yame}");
            return null;
        }
        catch (Exception e)
        {
            Log.Error($"Error while loading {plugin.Name} configs! Full Error --> \n{e}");
            return null;
        }
    }

    private static void ReloadConfigs()
    {
        Log.Info("Reloading plugin configs...");

        foreach (IPlugin<IConfiguration> plugin in PluginConfig.Keys)
        {
            try
            {
                plugin.ReloadConfig((IConfiguration)Deserializer.Deserialize(File.ReadAllText(configPaths[plugin.Config]), plugin.Config.GetType()));
                PluginConfig[plugin] = plugin.Config;
            }
            catch (Exception e)
            {
                Log.Error($"Error reloading config for plugin {plugin.Name}: {e.Message}");
            }
        }
    }

    private static void DisablePlugins()
    {
        foreach (IPlugin<IConfiguration> plugin in PluginConfig.Keys)
        {
            plugin.OnDisabled();
            plugin.UnLoadCommands();
        }
    }
}