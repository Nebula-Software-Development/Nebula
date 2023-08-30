using Nebuli.API.Interfaces;
using System;
using System.Reflection;
using PluginAPILogger = PluginAPI.Core.Log;

namespace Nebuli.API.Features;

public static class Log
{
    private static void AddLog(string message, ConsoleColor consoleColor) => ServerConsole.AddLog(PluginAPILogger.FormatText(message, "7"), consoleColor);

    private static string FormatLogMessage(string messageType, object message, string prefix = null, Assembly callingAssembly = null)
    {
        callingAssembly ??= Assembly.GetCallingAssembly();
        if (callingAssembly.GetName().Name == "Nebuli")
        {
            string text = $"&7[&b&3Nebuli&B&7] {message}";
            if (!string.IsNullOrEmpty(prefix))
                return $"&7[&b&3Nebuli {prefix}&B&7] {message}";
            return text;
        }
        return $"&7[&b&3Nebuli {messageType}&B&7] &7[&b&2{callingAssembly.GetName().Name}&B&7]&r {message}";
    }


    public static void Info(object message, string prefix = null, ConsoleColor consoleColor = ConsoleColor.Cyan) => AddLog(FormatLogMessage("Info", message, prefix, Assembly.GetCallingAssembly()), consoleColor);

    public static void Debug(object message, string prefix = null, ConsoleColor consoleColor = ConsoleColor.Green)
    {
        Assembly callingAssembly = Assembly.GetCallingAssembly();
        if (prefix == "Nebuli" && Loader.Loader.Configuration.ShowDebugLogs)
        {
            AddLog(FormatLogMessage("Debug", message, prefix, callingAssembly), consoleColor);
        }
        else if (!Loader.Loader._plugins.TryGetValue(callingAssembly, out IConfiguration plugin) || !plugin.Debug)
            return;

        AddLog(FormatLogMessage("Debug", message, prefix, callingAssembly), consoleColor);
    }

    public static void Warning(object message, string prefix = null, ConsoleColor consoleColor = ConsoleColor.Magenta) => AddLog(FormatLogMessage("Warn", message, prefix, Assembly.GetCallingAssembly()), consoleColor);
    public static void Error(object message, string prefix = null, ConsoleColor consoleColor = ConsoleColor.Red) => AddLog(FormatLogMessage("Error", message, prefix, Assembly.GetCallingAssembly()), consoleColor);
}