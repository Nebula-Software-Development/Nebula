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
    /// Determines if the Events should be patched or not (Disabling this will cause the events to not be working).
    /// </summary>
    [Description("Determines if the Events should be patched or not (Disabling this will cause the events to not be working).")]
    public bool PatchEvents { get; set; } = true;
    
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

    /// <summary>
    /// The startup message that the framework will display after initializing.
    /// </summary>
    [Description("The startup message that the framework will display in the console after initializing")]
    public string StartupMessage { get; set; } = "Welcome to... \r\n███╗░░██╗███████╗██████╗░██╗░░░██╗██╗░░░░░██╗\r\n████╗░██║██╔════╝██╔══██╗██║░░░██║██║░░░░░██║\r\n██╔██╗██║█████╗░░██████╦╝██║░░░██║██║░░░░░██║\r\n██║╚████║██╔══╝░░██╔══██╗██║░░░██║██║░░░░░██║\r\n██║░╚███║███████╗██████╦╝╚██████╔╝███████╗██║\r\n╚═╝░░╚══╝╚══════╝╚═════╝░░╚═════╝░╚══════╝╚═╝";  
}
