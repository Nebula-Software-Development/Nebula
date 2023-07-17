using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Nebuli.API.Features;
using Nebuli.API.Interfaces;
using PluginAPI.Core.Attributes;

namespace Nebuli;

#pragma warning disable CS1591
public class Loader
{
    private Harmony _harmony;

    [PluginConfig]
    public static LoaderConfiguration Configuration;
    
    [PluginEntryPoint("Nebuli Loader", "0, 0, 0", "Nebuli Plugin Framework", "Nebuli Team")]
    public void Load()
    {
        Log.Info($"Nebuli Version {NebuliInfo.NebuliVersion} loading...", consoleColor: ConsoleColor.Red);
        Log.Debug("Loading file paths...");
        Paths.LoadPaths();
        Log.Warning($"Dependency path is {Paths.DependenciesDirectory}");
        Log.Info("Loading dependencies...");
        LoadDependencies(Paths.DependenciesDirectory.GetFiles("*.dll"));
        Log.Warning($"Plugin path is {Paths.PluginsDirectory}");
        Log.Info("Loading plugins...");
        LoadPlugins(Paths.PluginsDirectory.GetFiles("*.dll"));
        _harmony = new("nebuli.patching.core");
        try
        {
            _harmony.PatchAll();
        }
        catch (Exception e)
        {
            Log.Error($"A error has occured when patching! Full error : \n{e}");
        }
    }

    [PluginUnload]
    public void UnLoad()
    {
        _harmony.UnpatchAll(_harmony.Id);
        _harmony = null;
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
        foreach(FileInfo file in files)
        {
            try
            {
                Log.Info("Debug1");
                Assembly loadPlugin = Assembly.LoadFile(file.FullName);
                Log.Info("Debug2");
                IPlugin<IConfig> newPlugin = NewPlugin(loadPlugin);
                Log.Info("Debug3");
                Log.Info(newPlugin.NebulisVersion.Major);
                Log.Info(NebuliInfo.NebuliVersion.Major);
                if (newPlugin.NebulisVersion.Major != NebuliInfo.NebuliVersion.Major && !Configuration.LoadOutDatedPlugins || NebuliInfo.NebuliVersion.Major != newPlugin.NebulisVersion.Major && !Configuration.LoadOutDatedPlugins)
                {
                    Log.Info("Debug4");
                    Log.Warning($"{newPlugin.PluginName} is outdated and will not be loaded by Nebuli! (Plugin Version : {newPlugin.NebulisVersion}, Nebuli Version : {NebuliInfo.NebuliVersion})");
                    Log.Info("Debug5");
                    continue;
                }
                Log.Info($"Plugin {newPlugin.PluginName}, by {newPlugin.PluginAuthor}, Version : {newPlugin.NebulisVersion} has been succesfully enabled!");
                newPlugin.OnEnabled();
            }
            catch(Exception e)
            {
                Log.Error($"Failed to load plugin {file.Name}. Full error : \n{e}");
            }
        }
        Log.Info("Plugins loaded!");
    }

    private static IPlugin<IConfig> NewPlugin(Assembly assembly)
    {
        try
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (!IsDerivedFromPlugin(type))
                    continue;

                Log.Info($"Trying to create plugin instance for type: {type.Name}");
                IPlugin<IConfig> plugin = CreatePluginInstance(type);
                if (plugin != null)
                {
                    Log.Info($"Plugin instance created successfully for type: {type.Name}");
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
            Log.Info($"Found constructor for type: {type.Name}");
            return constructor.Invoke(null) as IPlugin<IConfig>;
        }

        PropertyInfo pluginProperty = type.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).FirstOrDefault(property => property.PropertyType == type);

        if (pluginProperty != null)
        {
            Log.Info($"Found plugin property for type: {type.Name}");
        }
        else
        {
            Log.Warning($"No valid constructor or plugin property found for type: {type.Name}");
        }

        return pluginProperty?.GetValue(null) as IPlugin<IConfig>;
    }
}