using Nebuli.API.Interfaces;
using System.IO;
using YamlDotNet.Serialization;

namespace Nebuli.API.Features;

/// <summary>
/// Class for making custom files for plugins easier.
/// </summary>
public abstract class CustomFile : ICustomFile
{
    public virtual string FileName { get; }
    public virtual FileInfo FileInfo { get; }

    public virtual File File { get; }
    public  virtual DirectoryInfo Directory { get; } = Paths.PluginsDirectory;
    public virtual ISerializer Serializer { get; } = Loader.Loader.Serializer;
    public virtual IDeserializer Deserializer { get; } = Loader.Loader.Deserializer;
}
