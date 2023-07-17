using PluginAPILogger = PluginAPI.Core.Log;
using System.Reflection;

namespace Nebuli.API.Features;

public static class Log
{
    public static void Info(object message, string prefix = null)
    {
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        PluginAPILogger.Info(message.ToString(), prefix);
    }

    public static void Debug(object message, string prefix = null)
    {
        if (!Loader.Configuration.ShowDebugLogs)
        {
            PluginAPILogger.Info("Debug logs are disabled in the Loader Configuration", Assembly.GetCallingAssembly().GetName().Name);
            return;
        }

        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        PluginAPILogger.Debug(message.ToString(), prefix);
    }
    
    public static void Warning(object message, string prefix = null)
    {
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        PluginAPILogger.Warning(message.ToString(), prefix);
    }
    
    public static void Error(object message, string prefix = null)
    {
        prefix ??= Assembly.GetCallingAssembly().GetName().Name;
        PluginAPILogger.Error(message.ToString(), prefix);
    }
}