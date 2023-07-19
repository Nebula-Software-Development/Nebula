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

    internal static Dictionary<Assembly, IConfig> _plugins = new();

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

        Log.Info($"Nebuli Version {NebuliInfo.NebuliVersion} loading...", consoleColor: ConsoleColor.Red);
        Log.Debug("Loading file paths...");
        Paths.LoadPaths();
        Log.Debug($"Dependency path is {Paths.DependenciesDirectory}");
        Log.Info("Loading dependencies...");
        LoadDependencies(Paths.DependenciesDirectory.GetFiles("*.dll"));
        Log.Debug($"Plugin path is {Paths.PluginsDirectory}");
        Log.Info("Loading plugins...");
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
        ISerializer serializer = new SerializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        IDeserializer deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();

        foreach (FileInfo file in files)
        {
            try
            {
                byte[] assemblyBytes = File.ReadAllBytes(file.FullName);
                Assembly loadPlugin = Assembly.Load(assemblyBytes);
                IPlugin<IConfig> newPlugin = NewPlugin(loadPlugin);

                if (newPlugin.NebulisVersion.Major < NebuliInfo.NebuliVersion.Major && !Configuration.LoadOutDatedPlugins || newPlugin.NebulisVersion.Major > NebuliInfo.NebuliVersion.Major && !Configuration.LoadOutDatedPlugins)
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
            CustomNetworkManager.Modded = true;
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
            Log.Warning($"No valid constructor or plugin property found for type: {type.Name}");

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
                return plugin.Config;
            }
            else
            {
                Log.Debug($"Deserializing {plugin.PluginName} config at {configPath}...");
                return (IConfig)deserializer.Deserialize(File.ReadAllText(configPath), plugin.Config.GetType());
            }
        }
        catch (YamlException yame)
        {
            Log.Error($"Error while loading {plugin.PluginName} configs! Full Error --> \n{yame}");
            return null;
        }
    }

    public void CheckforUpdates()
    {
        /* if (!Configuration.AutoUpdate)  /////TODO, UNFINISHED AND WILL NOT WORK
             return;

         Log.Info("Checking for Nebuli updates...", "Updater", ConsoleColor.DarkBlue);

         using HttpClient client = new();
         try
         {
             string apiUrl = $"";
             client.DefaultRequestHeaders.Add("NebuliAutoUpdater", "Nebuli");
             var response = await client.GetAsync(apiUrl);

             if (response.IsSuccessStatusCode)
             {
                 string json = await response.Content.ReadAsStringAsync();
                 dynamic releaseData = JsonConvert.DeserializeObject(json);

                 string latestVersion = releaseData.tag_name;
                 if (Version.TryParse(latestVersion, out var latestVersionParsed) && latestVersionParsed > NebuliInfo.NebuliVersion)
                 {
                     Log.Info($"A new version of Nebuli is available: {latestVersion}");
                 }
                 else
                 {
                     Log.Info("Nebuli is up-to-date!");
                 }
             }
             else
             {
                 Log.Error($"Failed to check for updates. Response status code: \n{response.StatusCode}", "Updater");
                 return;
             }
         }
         catch (HttpRequestException ex)
         {
             Log.Error($"Failed to check for updates. Exception: \n{ex.Message}", "Updater");
             return;
         }
        */
    }
}