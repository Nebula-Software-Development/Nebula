using Nebuli.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebuli.API.Features
{
    /// <summary>
    /// Represents a base class for plugins in the Nebuli framework.
    /// </summary>
    /// <typeparam name="TConfig">The configuration type for the plugin.</typeparam>
    public abstract class Plugin<TConfig> : IPlugin<TConfig> where TConfig : IConfig, new()
    {

        /// <summary>
        /// Gets the plugins name.
        /// </summary>
        public string PluginName { get; }

        /// <summary>
        /// Gets the plugin's author.
        /// </summary>
        public string PluginAuthor { get; }
        /// <summary>
        /// Gets the plugins current version.
        /// </summary>
        public Version Version { get; }

        /// <summary>
        /// Gets the plugins current Nebulis version.
        /// </summary>
        public Version NebulisVersion { get; }

        /// <summary>
        /// If true, skips checking if the plugins current Nebulis version lines up with the Nebulis version loading the plugin.
        /// </summary>
        public bool SkipVersionCheck { get; }

        /// <summary>
        /// Called after loading the plugin succesfully.
        /// </summary>
        public void OnEnabled()
        {

        }

        /// <summary>
        /// Called after disabling the plugin.
        /// </summary>
        public void OnDisabled()
        {

        }
    }
}
