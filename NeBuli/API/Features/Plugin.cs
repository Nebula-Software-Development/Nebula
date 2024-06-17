// -----------------------------------------------------------------------
// <copyright file=Plugin.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandSystem;
using Nebula.API.Extensions;
using Nebula.API.Features.Enum;
using Nebula.API.Features.Pools;
using Nebula.API.Interfaces;

namespace Nebula.API.Features
{
    /// <summary>
    ///     Represents a base class for plugins in the Nebula framework.
    /// </summary>
    /// <typeparam name="TConfig">The configuration type for the plugin.</typeparam>
    public abstract class Plugin<TConfig> : IPlugin<TConfig> where TConfig : IConfiguration, new()
    {
        internal readonly Dictionary<ICommandHandler, List<ICommand>> CommandDictionary =
            DictionaryPool<ICommandHandler, List<ICommand>>.Instance.Get();

        /// <summary>
        ///     Gets a list of registered commands for the plugin.
        /// </summary>
        public List<ICommand> RegisteredCommands => CommandDictionary.Values.SelectMany(list => list).ToList();

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public Assembly Assembly { get; set; }

        /// <summary>
        ///     Gets the plugins name.
        /// </summary>
        public virtual string Name { get; } = "Undefined";

        /// <summary>
        ///     Gets the plugin's creator.
        /// </summary>
        public virtual string Creator { get; } = "Undefined";

        /// <summary>
        ///     Gets the plugins current version.
        /// </summary>
        public virtual Version Version { get; } = new(0, 0, 0);

        /// <summary>
        ///     Gets the plugins current Nebulas version.
        /// </summary>
        public virtual Version NebulaVersion { get; } = new(0, 0, 0);

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public virtual LoadOrderType LoadOrder { get; } = LoadOrderType.NormalLoad;

        /// <summary>
        ///     If true, skips checking if the plugins current Nebulas version lines up with the Nebulas version loading the
        ///     plugin.
        /// </summary>
        public virtual bool SkipVersionCheck { get; } = false;

        /// <summary>
        ///     Gets the plugins configuration file path.
        /// </summary>
        public virtual string ConfigPath { get; set; } = string.Empty;

        /// <summary>
        ///     The plugins config.
        /// </summary>
        public TConfig Config { get; set; } = new();

        /// <summary>
        ///     Called after loading the plugin succesfully.
        /// </summary>
        public virtual void OnEnabled()
        {
        }

        /// <summary>
        ///     Called after disabling the plugin.
        /// </summary>
        public virtual void OnDisabled()
        {
        }

        /// <summary>
        ///     Reloads the plugin's config.
        /// </summary>
        public void ReloadConfig(IConfiguration config)
        {
            Config = (TConfig)config;
        }

        public void LoadCommands()
        {
            CommandDictionary.Clear();

            foreach (Type type in Assembly.GetTypes())
            {
                if (type.GetInterface("ICommand") != typeof(ICommand) ||
                    !Attribute.IsDefined(type, typeof(CommandHandlerAttribute)))
                {
                    continue;
                }

                Type commandHandlerType = null;
                foreach (CustomAttributeData customAttributeData in type.CustomAttributes)
                {
                    try
                    {
                        if (customAttributeData.AttributeType != typeof(CommandHandlerAttribute))
                        {
                            continue;
                        }

                        commandHandlerType = (Type)customAttributeData.ConstructorArguments?[0].Value;

                        if (GenericExtensions.CommandHandlers.TryGetValue(commandHandlerType,
                                out ICommandHandler commandHandler))
                        {
                            ICommand command = null;
                            try
                            {
                                command = (ICommand)Activator.CreateInstance(type);
                                commandHandler.RegisterCommand(command);
                                if (!CommandDictionary.ContainsKey(commandHandler))
                                {
                                    CommandDictionary[commandHandler] = new List<ICommand>();
                                }

                                CommandDictionary[commandHandler].Add(command);
                            }
                            catch (ArgumentException)
                            {
                                Log.Error(
                                    $"{Name} tried to register the command '{command?.Command}', which has already been registered!");
                            }
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
            foreach (KeyValuePair<ICommandHandler, List<ICommand>> commandPair in CommandDictionary)
            {
                ICommandHandler handler = commandPair.Key;
                List<ICommand> commands = commandPair.Value;

                foreach (ICommand command in commands)
                {
                    handler.UnregisterCommand(command);
                }
            }

            CommandDictionary.Clear();
            DictionaryPool<ICommandHandler, List<ICommand>>.Instance.Return(CommandDictionary);
        }
    }
}