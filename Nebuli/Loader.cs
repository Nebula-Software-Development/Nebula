using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HarmonyLib;
using Nebuli.API.Features;
using PluginAPI.Core.Attributes;

namespace Nebuli;

public class Loader
{
    private Harmony _harmony;

    [PluginConfig]
    public static LoaderConfiguration Configuration;
    
    [PluginEntryPoint("NeBuli Loader", "0.0.1", "NeBuli Plugin Framework", "NeBuli Team")]
    public void Load()
    {
        LoadDependencies(Paths.DepsDict.GetFiles("*.dll"));
        
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
            string assemblies = Assembly.Load(File.ReadAllBytes(file.FullName)).FullName;
        }
    }
}