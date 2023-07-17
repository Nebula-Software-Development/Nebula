using System.ComponentModel;

namespace Nebuli;

/// <summary>
/// 
/// </summary>
public class LoaderConfiguration
{
    /// <summary>
    /// Determines if the Loader's debug logs should be shown or not.
    /// </summary>
    [Description("Determines if plugin debug logs show or not.")]
    public bool ShowDebugLogs { get; set; } = true;
}