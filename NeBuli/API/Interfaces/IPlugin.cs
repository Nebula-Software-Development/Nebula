// -----------------------------------------------------------------------
// <copyright file=IPlugin.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Reflection;
using Nebuli.API.Features.Enum;
using Nebuli.API.Interfaces;

namespace Nebuli.API.Features
{
    /// <summary>
    ///     The default plugin interface for Nebuli.
    /// </summary>
    /// <typeparam name="TConfig">The plugins config.</typeparam>
    public interface IPlugin<out TConfig> where TConfig : IConfiguration
    {
        /// <summary>
        ///     Gets the plugins assembly.
        /// </summary>
        Assembly Assembly { get; set; }

        /// <summary>
        ///     Gets the plugins name.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets the plugin's creator.
        /// </summary>
        string Creator { get; }

        /// <summary>
        ///     Gets the plugins current version.
        /// </summary>
        Version Version { get; }

        /// <summary>
        ///     Gets the plugins current Nebulis version.
        /// </summary>
        Version NebuliVersion { get; }

        /// <summary>
        ///     Gets the plugins <see cref="LoadOrderType" />.
        /// </summary>
        LoadOrderType LoadOrder { get; }

        /// <summary>
        ///     If true, skips checking if the plugins current Nebulis version lines up with the Nebulis version loading the
        ///     plugin.
        /// </summary>
        bool SkipVersionCheck { get; }

        /// <summary>
        ///     Gets the plugins configuration file path.
        /// </summary>
        string ConfigPath { get; internal set; }

        /// <summary>
        ///     The plugins config.
        /// </summary>
        TConfig Config { get; }

        /// <summary>
        ///     Called after loading the plugin succesfully.
        /// </summary>
        void OnEnabled();

        /// <summary>
        ///     Called after disabling the plugin.
        /// </summary>
        void OnDisabled();

        /// <summary>
        ///     Reloads the plugin's config.
        /// </summary>
        void ReloadConfig(IConfiguration config);

        void LoadCommands();

        void UnLoadCommands();
    }
}