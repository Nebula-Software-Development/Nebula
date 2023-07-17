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
    /// <param name="consoleColor">The color of the text in the console.</param>
    public static void Info(object message, string prefix = null, ConsoleColor consoleColor = ConsoleColor.Cyan)
    {
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        if(prefix == "Nebuli")
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7] {message}", "7"), consoleColor);
        else
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7] &7[&b&2{prefix}&B&7]&r  {message}", "7"), consoleColor);


    }

    /// <summary>
    /// Sends a debug message to the console.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="prefix">The prefix of the message.</param>
    ///  <param name="consoleColor">The color of the text in the console.</param>
    public static void Debug(object message, string prefix = null, ConsoleColor consoleColor = ConsoleColor.Green)
    {
        
        if (!Loader.RegistedAssemblys[Assembly.GetCallingAssembly()].Config.Debug)
        {
            return;
        }

        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        if (prefix == "Nebuli")
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7] {message}", "7"), consoleColor);
        else
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7] &7[&b&2{prefix}&B&7]&r  {message}", "7"), consoleColor);
    }

    /// <summary>
    /// Sends a warning message to the console.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="prefix">The prefix of the message.</param>
    /// <param name="consoleColor">The color of the text in the console.</param>
    public static void Warning(object message, string prefix = null, ConsoleColor consoleColor = ConsoleColor.Magenta)
    {
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        if (prefix == "Nebuli")
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7] {message}", "7"), consoleColor);
        else
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7] &7[&b&2{prefix}&B&7]&r  {message}", "7"), consoleColor);
    }

    /// <summary>
    /// Sends a error message to the console.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="prefix">The prefix of the message.</param>
    /// <param name="consoleColor">The color of the text in the console.</param>
    public static void Error(object message, string prefix = null, ConsoleColor consoleColor = ConsoleColor.Red)
    {
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        if (prefix == "Nebuli")
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7] {message}", "7"), consoleColor);
        else
            ServerConsole.AddLog(PluginAPILogger.FormatText($"&7[&b&3Nebuli&B&7] &7[&b&2{prefix}&B&7]&r  {message}", "7"), consoleColor);
    }
}