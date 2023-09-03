using CommandSystem.Commands.Shared;
using HarmonyLib;
using MEC;
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

public class Loader
{
    private Harmony _harmony;
    private readonly int pluginCount = EnabledPlugins.Count;
    private static bool _loaded = false;

    public static Random Random { get; } = new();

    private static Assembly _assemblyCache = null;
    public static Assembly NebuliAssembly
    {
        get
        {
            if (_assemblyCache != null)
                return _assemblyCache;

            foreach (Assembly assembly in PluginAPI.Loader.AssemblyLoader.Plugins.Keys)
            {
                if (PluginAPI.Loader.AssemblyLoader.Plugins[assembly].Any(plugin => plugin.Value.PluginName == "Nebuli Loader"))
                {
                    _assemblyCache = assembly;
                    break;
                }
            }

            return _assemblyCache;
        }
    }

    public static ISerializer Serializer { get; private set; }
    public static IDeserializer Deserializer { get; private set; }

    internal static void EDisablePlugins() => DisablePlugins();

    internal static Dictionary<Assembly, IConfiguration> _plugins = new();

    public static Dictionary<IPlugin<IConfiguration>, IConfiguration> EnabledPlugins = new();

    [PluginConfig]
    public static LoaderConfiguration Configuration;

    [PluginEntryPoint("Nebuli Loader", $"{NebuliInfo.NebuliVersionConst}", "Nebuli Plugin Framework", "Nebuli Team")]
    [PluginPriority(LoadPriority.Highest)]
    public void FrameworkLoader()
    {
        if (!Configuration.LoaderEnabled)
        {
            Log.Info("Nebuli Loader is disabled, Nebuli will not load.");
            return;
        }       

        if (_loaded) return;
        else _loaded = true;

        Log.Info($"Nebuli Version {NebuliInfo.NebuliVersion} loading...", consoleColor: ConsoleColor.Red, prefix: "Loader");

        SetupFilePaths();

        Log.Debug($"Dependency path is {Paths.DependenciesDirectory}");

        if (Configuration.ShouldCheckForUpdates)
        {
            Updater updater = new();
            updater.CheckForUpdates();
        }

        LoadDependencies(Paths.DependenciesDirectory.GetFiles("*.dll"));

        Log.Debug($"Plugin path is {Paths.PluginsPortDirectory}");

        LoadPlugins(Paths.PluginsPortDirectory.GetFiles("*.dll"));

        EventManager.RegisterBaseEvents();     

        try
        {
            if (Configuration.PatchEvents)
            {
                _harmony = new("nebuli.patching.core");
                _harmony.PatchAll();
            }
            else
                Log.Warning("Event patching is disabled, Events will not work");
        }
        catch (Exception e)
        {
            Log.Error($"A error has occured while patching! Full error: \n{e}", "Patching");
        }

        try
        {
            Timing.CallDelayed(5, () =>
            {
                Permissions.PermissionsHandler.LoadPermissions();
            });
        }
        catch(Exception e) 
        {
            Log.Error("Error occured while loading permission handler! Full error -->\n" + e);
        }

        if (pluginCount > 0)
        {
            CustomNetworkManager.Modded = true;
            BuildInfoCommand.ModDescription = $"Framework : Nebuli\nFramework Version : {NebuliInfo.NebuliVersion}\nCopyright : Copyright (c) 2023 Nebuli Team";
        }

        Log.Info(Configuration.StartupMessage);
    }

    [PluginUnload]
    public void UnLoad()
    {
        DisablePlugins();

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

        EnabledPlugins.Clear();
        _plugins.Clear();

        Serializer = new SerializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        Deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();

        foreach (FileInfo file in files)
        {
            try
            {
                Assembly loadPlugin = Assembly.Load(File.ReadAllBytes(file.FullName));
                IPlugin<IConfiguration> newPlugin = NewPlugin(loadPlugin);

                if (IsPluginOutdated(newPlugin))
                    continue;

                IConfiguration config = SetupPluginConfig(newPlugin, Serializer, Deserializer);

                if (!config.IsEnabled)
                    continue;

                newPlugin.LoadCommands();
                newPlugin.OnEnabled();

                Log.Info($"Plugin '{newPlugin.Name}' by '{newPlugin.Creator}', (v{newPlugin.Version}), has been successfully enabled!");

                _plugins.Add(loadPlugin, config);
                EnabledPlugins.Add(newPlugin, config);
            }
            catch (Exception e)
            {
                Log.Error($"Failed to load plugin {file.Name}. Full error : \n{e}");
            }
        }

        Log.Info("Plugins loaded!");      
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

    private static IConfiguration SetupPluginConfig(IPlugin<IConfiguration> plugin, ISerializer serializer, IDeserializer deserializer)
    {
        string configPath = Path.Combine(Paths.PluginPortConfigDirectory.FullName, plugin.Name + $"-({Server.ServerPort})-Config.yml");
        try
        {
            if (!File.Exists(configPath))
            {
                Log.Warning($"{plugin.Name} does not have configs! Generating...");
                Log.Debug("Serializing new config and writing it to the path...");
                File.WriteAllText(configPath, serializer.Serialize(plugin.Config));
                plugin.ConfigPath = configPath;
                return plugin.Config;
            }
            else
            {
                Log.Debug($"Deserializing {plugin.Name} config at {configPath}...");
                IConfiguration config = (IConfiguration)deserializer.Deserialize(File.ReadAllText(configPath), plugin.Config.GetType());
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
        foreach (IPlugin<IConfiguration> plugin in EnabledPlugins.Keys.ToList())
        {
            try
            {
                Log.Info($"Reloading plugin configs for {plugin.Name}...");
                if (!File.Exists(plugin.ConfigPath))
                    SetupPluginConfig(plugin, Serializer, Deserializer);
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
        Log.Info("Reloading plugins...");
        DisablePlugins();
        LoadDependencies(Paths.DependenciesDirectory.GetFiles("*.dll"));
        LoadPlugins(Paths.PluginsDirectory.GetFiles("*.dll"));
    }

    internal static void DisablePlugins()
    {
        foreach (IPlugin<IConfiguration> plugin in EnabledPlugins.Keys)
        {
            plugin.OnDisabled();
            plugin.UnLoadCommands();
        }
        EnabledPlugins.Clear();
        _plugins.Clear();
    }
}