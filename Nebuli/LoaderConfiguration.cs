using System.ComponentModel;

namespace Nebuli;

/// <summary>
/// 
/// </summary>
public class LoaderConfiguration
{
    /// <summary>
    /// Determines if the Loader is enabled or not.
    /// </summary>
    [Description("Determines if Loader is enabled or not.")]
    public bool LoaderEnabled { get; set; } = true;
    /// <summary>
    /// Determines if the Loader's debug logs should be shown or not.
    /// </summary>
    [Description("Determines if plugin debug logs show or not.")]
    public bool ShowDebugLogs { get; set; } = true;

    /// <summary>
    /// Determines if the Loader should load plugins where their major required version of Nebuli dosent match Nebulis current major version.
    /// </summary>
    [Description("Determines if the Loader should load plugins where their major required version of Nebuli dosent match Nebulis current major version.")]
    public bool LoadOutDatedPlugins { get; set; } = false;
}