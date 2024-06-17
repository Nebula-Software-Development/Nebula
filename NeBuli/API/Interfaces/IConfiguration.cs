// -----------------------------------------------------------------------
// <copyright file=IConfiguration.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.ComponentModel;

namespace Nebula.API.Interfaces
{
    /// <summary>
    ///     Default Interface for plugin configurations.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        ///     Determines if the plugin is enabled or not.
        /// </summary>
        [Description("Determines if the plugin is enabled or not.")]
        bool IsEnabled { get; set; }

        /// <summary>
        ///     Determines if the plugin's debug logs are enabled or not
        /// </summary>
        [Description("Determines if the plugin's debug logs are enabled or not.")]
        bool Debug { get; set; }
    }
}