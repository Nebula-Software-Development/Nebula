using HarmonyLib;
using PluginAPI.Core.Attributes;

namespace Nebuli;

public class Loader
{
    private Harmony _harmony;
    
    [PluginEntryPoint("NeBuli Loader", "0.0.1", "NeBuli Plugin Framework", "NeBuli Team")]
    public void Load()
    {
        _harmony = new("nebuli.patching.core");
        _harmony.PatchAll();
    }

    [PluginUnload]
    public void UnLoad()
    {
        _harmony.UnpatchAll(_harmony.Id);
        _harmony = null;
    }
}