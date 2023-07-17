using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HarmonyLib;
using Nebuli.API.Features;
using PluginAPI.Core.Attributes;

namespace Nebuli;

#pragma warning disable CS1591
public class Loader
{
    private Harmony _harmony;

    [PluginConfig]
    public static LoaderConfiguration Configuration;
    
    [PluginEntryPoint("Nebuli Loader", "0.0.1", "Nebuli Plugin Framework", "Nebuli Team")]
    public void Load()
    {
        Log.Info("Nebuli loading...");
        Log.Debug("Loading file paths...");
        Paths.LoadPaths();
        Log.Info("Loading dependencies...");
        LoadDependencies(Paths.DependenciesDirectory.GetFiles("*.dll"));
        Log.Info("Loading plugins...");
        LoadPlugins(Paths.PluginsDirectory.GetFiles("*.dll"));
        _harmony = new("nebuli.patching.core");
        _harmony.PatchAll();
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
        Log.Info("Dependencies Loaded!");
    }

    private void LoadPlugins(IEnumerable<FileInfo> files)
    {
        foreach(FileInfo file in files)
        {
            try
            {

            }
            catch(Exception e)
            {
                Log.Error($"Failed to load plugin {file.Name}. Full error : \n{e}");
            }
        }
    }
}