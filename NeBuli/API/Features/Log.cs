using System;
using System.Reflection;
using PluginAPILogger = PluginAPI.Core.Log;

namespace Nebuli.API.Features;

public static class Log
{
    private static void AddLog(string message, ConsoleColor consoleColor)
    {
        ServerConsole.AddLog(PluginAPILogger.FormatText(message, "7"), consoleColor);
    }

    private static string FormatLogMessage(string messageType, object message, string prefix = null, Assembly callingAssembly = null)
    {
        callingAssembly ??= Assembly.GetCallingAssembly();
        prefix ??= callingAssembly.GetName().Name;
        string formattedMessage = callingAssembly.GetName().Name == "Nebuli" ? $"&7[&b&3Nebuli&B&7] {message}" : $"&7[&b&3Nebuli | {messageType}&B&7] &7[&b&2{prefix}&B&7]&r {message}";

        return formattedMessage;
    }

    public static void Info(object message, string prefix = null, ConsoleColor consoleColor = ConsoleColor.Cyan)
    {
        AddLog(FormatLogMessage("Info", message, prefix, Assembly.GetCallingAssembly()), consoleColor);
    }

    public static void Debug(object message, string prefix = null, ConsoleColor consoleColor = ConsoleColor.Green)
    {
        Assembly callingAssembly = Assembly.GetCallingAssembly();
        if (prefix == "Nebuli" && Loader.Loader.Configuration.ShowDebugLogs)
        {
            AddLog(FormatLogMessage("Debug", message, prefix, callingAssembly), consoleColor);
        }
        else if (!Loader.Loader._plugins.TryGetValue(callingAssembly, out var plugin) || !plugin.Debug)
        {
            return;
        }

        AddLog(FormatLogMessage("Debug", message, prefix, callingAssembly), consoleColor);
    }

    public static void Warning(object message, string prefix = null, ConsoleColor consoleColor = ConsoleColor.Magenta)
    {
        AddLog(FormatLogMessage("Warn", message, prefix, Assembly.GetCallingAssembly()), consoleColor);
    }

    public static void Error(object message, string prefix = null, ConsoleColor consoleColor = ConsoleColor.Red)
    {
        AddLog(FormatLogMessage("Error", message, prefix, Assembly.GetCallingAssembly()), consoleColor);
    }
}