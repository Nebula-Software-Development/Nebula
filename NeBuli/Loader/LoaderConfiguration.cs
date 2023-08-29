using System.ComponentModel;

namespace Nebuli.Loader;

/// <summary>
/// The loaders configuration class.
/// </summary>
public class LoaderConfiguration
{
    /// <summary>
    /// Determines if the Loader is enabled or not.
    /// </summary>
    [Description("Determines if Loader is enabled or not.")]
    public bool LoaderEnabled { get; set; } = true;

    /// <summary>
    /// Determines if the Events should be patched or not (Disabling this will cause the events to be unfunctional).
    /// </summary>
    [Description("Determines if the Events should be patched or not (Disabling this will cause the events to be unfunctional).")]
    public bool PatchEvents { get; set; } = true;

    /// <summary>
    /// Determines if the Loader's debug logs should be shown or not.
    /// </summary>
    [Description("Determines if Loader's debug logs show or not.")]
    public bool ShowDebugLogs { get; set; } = false;

    /// <summary>
    /// If true, allows the server to be tracked and counted with the total number of Nebuli servers via server name.
    /// </summary>
    [Description("If true, allows the server to be tracked and counted with the total number of Nebuli servers via server name.")]
    public bool ServerNameTracking { get; set; } = true;

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

    /// <summary>
    /// If Nebuli should automatically check for updates.
    /// </summary>
    [Description("If Nebuli should automatically check for updates.")]
    public bool ShouldCheckForUpdates { get; set; } = true;

    /// <summary>
    /// If Nebuli should allow external download URLs for commands like 'forceinstall'..
    /// </summary>
    [Description("If Nebuli should allow external download URLs for commands like 'forceinstall'.")]
    public bool AllowExternalDownloadURLS { get; set; } = false;
}