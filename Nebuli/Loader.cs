using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HarmonyLib;
using Nebuli.API.Features;
using PluginAPI.Core.Attributes;

namespace Nebuli;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public class Loader
{
    private Harmony _harmony;

    [PluginConfig]
    public static LoaderConfiguration Configuration;
    
    [PluginEntryPoint("Nebuli Loader", "0.0.1", "Nebuli Plugin Framework", "Nebuli Team")]
    public void Load()
    {
        Paths.LoadPaths();
        
        LoadDependencies(Paths.DependenciesDirectory.GetFiles("*.dll"));
        
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
                string assemblies = Assembly.Load(File.ReadAllBytes(file.FullName)).FullName;
                Log.Info($"Dependency {assemblies} loaded.");
            }
            catch (Exception e)
            {
                Log.Error($"Failed to load dependency {file.Name}.\n{e}");
            }
        }
    }
}