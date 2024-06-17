// -----------------------------------------------------------------------
// <copyright file=IPlugin.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Reflection;
using Nebula.API.Features.Enum;
using Nebula.API.Interfaces;

namespace Nebula.API.Features
{
    /// <summary>
    ///     The default plugin interface for Nebula.
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
        ///     Gets the plugins current Nebulas version.
        /// </summary>
        Version NebulaVersion { get; }

        /// <summary>
        ///     Gets the plugins <see cref="LoadOrderType" />.
        /// </summary>
        LoadOrderType LoadOrder { get; }

        /// <summary>
        ///     If true, skips checking if the plugins current Nebulas version lines up with the Nebulas version loading the
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