using PluginAPI.Core.Attributes;

namespace NebuliLoader;

public class Loader
{
    [PluginEntryPoint("NeBuli Loader", "0.0.1", "The NeBuli framework loader", "NeBuli Team")]
    public void Load()
    {
    }

    [PluginUnload]
    public void UnLoad()
    {
    }
}