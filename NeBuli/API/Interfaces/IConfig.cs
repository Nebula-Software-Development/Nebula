
using System.ComponentModel;

namespace Nebuli.API.Interfaces;


public interface IConfig
{
    /// <summary>
    /// Determines if the plugin is enabled or not.
    /// </summary>
    [Description("Determines if the plugin is enabled or not.")]
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Determines if the plugin's debug logs are enabled or not
    /// </summary>
    [Description("Determines if the plugin's debug logs are enabled or not.")]
    public bool Debug { get; set; }
}
