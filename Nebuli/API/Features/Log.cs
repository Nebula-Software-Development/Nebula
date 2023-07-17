using PluginAPILogger = PluginAPI.Core.Log;
using System.Reflection;
using System;
using PluginAPI.Enums;

namespace Nebuli.API.Features;

/// <summary>
/// Class for handling logs.
/// </summary>
public static class Log
{
    /// <summary>
    /// Sends a message to the console.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="prefix">The prefix of the message.</param>
    public static void Info(object message, string prefix = null)
    {
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        if(prefix == "Nebuli")
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7] {message}", "7"), ConsoleColor.Cyan);
        else
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7]  &7[&b&2{prefix}&B&7]&r  {message}", "7"), ConsoleColor.Cyan);


    }

    /// <summary>
    /// Sends a debug message to the console.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="prefix">The prefix of the message.</param>
    public static void Debug(object message, string prefix = null)
    {
        if (!Loader.Configuration.ShowDebugLogs)
        {
            PluginAPILogger.Info("Debug logs are disabled in the Loader Configuration", Assembly.GetCallingAssembly().GetName().Name);
            return;
        }

        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        if (prefix == "Nebuli")
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7] {message}", "7"), ConsoleColor.Green);
        else
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7]  &7[&b&2{prefix}&B&7]&r  {message}", "7"), ConsoleColor.Green);
    }

    /// <summary>
    /// Sends a warning message to the console.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="prefix">The prefix of the message.</param>
    public static void Warning(object message, string prefix = null)
    {
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        if (prefix == "Nebuli")
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7] {message}", "7"), ConsoleColor.Magenta);
        else
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7]  &7[&b&2{prefix}&B&7]&r  {message}", "7"), ConsoleColor.Magenta);
    }

    /// <summary>
    /// Sends a error message to the console.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="prefix">The prefix of the message.</param>
    public static void Error(object message, string prefix = null)
    {
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        if (prefix == "Nebuli")
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7] {message}", "7"), ConsoleColor.Red);
        else
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7]  &7[&b&2{prefix}&B&7]&r  {message}", "7"), ConsoleColor.Red);
    }
}