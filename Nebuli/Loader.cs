using HarmonyLib;
using PluginAPI.Core.Attributes;

namespace Nebuli;

public class Loader
{
    private Harmony _harmony;
    
    [PluginEntryPoint("Nebuli Loader", "0.0.1", "Nebuli Plugin Framework", "Nebuli Team")]
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