// -----------------------------------------------------------------------
// <copyright file=Log.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Reflection;
using Nebula.API.Interfaces;
using Nebula.Loader;
using PluginAPILogger = PluginAPI.Core.Log;

namespace Nebula.API.Features
{
    /// <summary>
    ///     Utility class for easily logging stuff into the server console.
    /// </summary>
    public static class Log
    {
        private static void AddLog(string message, ConsoleColor consoleColor)
        {
            ServerConsole.AddLog(PluginAPILogger.FormatText(message, "7"), consoleColor);
        }

        private static string FormatLogMessage(string messageType, object message, string prefix = null,
            Assembly callingAssembly = null)
        {
            callingAssembly ??= Assembly.GetCallingAssembly();
            if (callingAssembly == LoaderClass.NebulaAssembly)
            {
                string text = $"&7[&b&3Nebula&B&7] {message}";
                if (!string.IsNullOrEmpty(prefix))
                {
                    return $"&7[&b&3Nebula {prefix}&B&7] {message}";
                }

                return text;
            }

            return $"&7[&b&3Nebula {messageType}&B&7] &7[&b&2{callingAssembly.GetName().Name}&B&7]&r {message}";
        }

        /// <summary>
        ///     Logs an informational message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="prefix">An optional prefix for the log message.</param>
        /// <param name="consoleColor">The console color for displaying the log message.</param>
        public static void Print(object message, string prefix = null, ConsoleColor consoleColor = ConsoleColor.Cyan)
        {
            AddLog(FormatLogMessage("Print", message, prefix, Assembly.GetCallingAssembly()), consoleColor);
        }

        /// <summary>
        ///     Logs a debug message if debug logs are enabled for the calling assembly.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="prefix">An optional prefix for the log message.</param>
        /// <param name="consoleColor">The console color for displaying the log message.</param>
        public static void Debug(object message, string prefix = null, ConsoleColor consoleColor = ConsoleColor.Green)
        {
            Assembly callingAssembly = Assembly.GetCallingAssembly();
            if (callingAssembly == LoaderClass.NebulaAssembly && LoaderClass.Configuration.ShowDebugLogs)
            {
                AddLog(FormatLogMessage("Debug", message, prefix, callingAssembly), consoleColor);
                return;
            }

            if (!LoaderClass.Plugins.TryGetValue(callingAssembly, out IPlugin<IConfiguration> plugin) ||
                !plugin.Config.Debug)
            {
                return;
            }

            AddLog(FormatLogMessage("Debug", message, prefix, callingAssembly), consoleColor);
        }

        /// <summary>
        ///     Logs a warning message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="prefix">An optional prefix for the log message.</param>
        /// <param name="consoleColor">The console color for displaying the log message.</param>
        public static void Warning(object message, string prefix = null,
            ConsoleColor consoleColor = ConsoleColor.Magenta)
        {
            AddLog(FormatLogMessage("Warn", message, prefix, Assembly.GetCallingAssembly()), consoleColor);
        }

        /// <summary>
        ///     Logs an error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="prefix">An optional prefix for the log message.</param>
        /// <param name="consoleColor">The console color for displaying the log message.</param>
        public static void Error(object message, string prefix = null, ConsoleColor consoleColor = ConsoleColor.Red)
        {
            AddLog(FormatLogMessage("Error", message, prefix, Assembly.GetCallingAssembly()), consoleColor);
        }
    }
}