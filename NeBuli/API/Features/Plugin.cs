using CommandSystem;
using Nebuli.API.Interfaces;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nebuli.API.Features;

/// <summary>
/// Represents a base class for plugins in the Nebuli framework.
/// </summary>
/// <typeparam name="TConfig">The configuration type for the plugin.</typeparam>
public abstract class Plugin<TConfig> : IPlugin<TConfig> where TConfig : IConfiguration, new()
{
    public Assembly Assembly { get; internal set; } = Assembly.GetCallingAssembly();
    /// <summary>
    /// Gets the plugins name.
    /// </summary>
    public virtual string Name { get; }

    /// <summary>
    /// Gets the plugin's creator.
    /// </summary>
    public virtual string Creator { get; }

    /// <summary>
    /// Gets the plugins current version.
    /// </summary>
    public virtual Version Version { get; }

    /// <summary>
    /// Gets the plugins current Nebulis version.
    /// </summary>
    public virtual Version NebuliVersion { get; }

    /// <summary>
    /// If true, skips checking if the plugins current Nebulis version lines up with the Nebulis version loading the plugin.
    /// </summary>
    public virtual bool SkipVersionCheck { get; } = false;

    /// <summary>
    /// The plugins config.
    /// </summary>
    public TConfig Config { get; set; } = new TConfig();

    /// <summary>
    /// Called after loading the plugin succesfully.
    /// </summary>
    public virtual void OnEnabled()
    {
    }

    /// <summary>
    /// Called after disabling the plugin.
    /// </summary>
    public virtual void OnDisabled()
    {
    }

    /// <summary>
    /// Reloads the plugin's config.
    /// </summary>
    public void ReloadConfig(IConfiguration config)
    {
        Config = (TConfig)config;
    }

    private readonly Dictionary<Type, ICommandHandler> CommandHandlers = new()
    {
    { typeof(RemoteAdminCommandHandler), CommandProcessor.RemoteAdminCommandHandler },
    { typeof(GameConsoleCommandHandler), GameCore.Console.singleton.ConsoleCommandHandler },
    { typeof(ClientCommandHandler), QueryProcessor.DotCommandHandler }
    };

    private readonly List<ICommand> Commands = new();

    public virtual void LoadCommands()
    {
        Commands.Clear();

        foreach (Type type in Assembly.GetTypes())
        {
            if (type.GetInterface("ICommand") != typeof(ICommand) || !Attribute.IsDefined(type, typeof(CommandHandlerAttribute)))
                continue;

            Type commandHandlerType = null;
            foreach (CustomAttributeData customAttributeData in type.CustomAttributes)
            {
                try
                {
                    if (customAttributeData.AttributeType != typeof(CommandHandlerAttribute))
                        continue;

                    commandHandlerType = (Type)customAttributeData.ConstructorArguments?[0].Value;

                    if (CommandHandlers.TryGetValue(commandHandlerType, out ICommandHandler commandHandler))
                    {
                        ICommand command = (ICommand)Activator.CreateInstance(type);
                        commandHandler.RegisterCommand(command);
                        Commands.Add(command);
                    }
                }
                catch (Exception exception)
                {
                    Log.Error($"An error occurred while registering a command: {exception}");
                }
            }
        }
    }

    public void UnLoadCommands()
    {
        foreach (ICommand command in Commands)
        {
            CommandHandlerAttribute attribute = (CommandHandlerAttribute)Attribute.GetCustomAttribute(command.GetType(), typeof(CommandHandlerAttribute));

            PropertyInfo handlerTypeProperty = attribute.GetType().GetProperty("HandlerType");
            if (handlerTypeProperty != null && handlerTypeProperty.PropertyType == typeof(Type))
            {
                Type commandHandlerType = (Type)handlerTypeProperty.GetValue(attribute);

                if (commandHandlerType != null && CommandHandlers.TryGetValue(commandHandlerType, out ICommandHandler commandHandler))
                {
                    try
                    {
                        commandHandler.UnregisterCommand(command);
                    }
                    catch (ArgumentException e)
                    {
                        Log.Error($"Error occurred while unregistering a command: {e}");
                    }
                }
                else
                {
                    Log.Error($"Unknown command type or command handler type: {command.GetType().Name}");
                }
            }
            else
            {
                Log.Error($"Command {command.GetType().Name} does not have a valid command handler attribute.");
            }
        }

        Commands.Clear();
        CommandHandlers.Clear();
    }
}
