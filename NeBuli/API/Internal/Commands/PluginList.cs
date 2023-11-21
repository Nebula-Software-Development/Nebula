// -----------------------------------------------------------------------
// <copyright file=PluginList.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Text;
using CommandSystem;
using Nebuli.API.Features;
using Nebuli.API.Interfaces;
using Nebuli.Loader;
using NorthwoodLib.Pools;

namespace Nebuli.API.Internal.Commands
{
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class PluginList : ICommand
    {
        public string Command { get; } = "pluginlist";

        public string[] Aliases { get; } = { "plugin-list", "plugin list" };

        public string Description { get; } = "Prints a list of enabled plugins and their details.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            int maxCreatorWidth = 16;
            int maxNameWidth = 16;
            int maxVersionWidth = 8;
            int maxNebuliVersionWidth = 8;

            string tableHeader = "\n| Creator".PadRight(maxCreatorWidth) + " | Name".PadRight(maxNameWidth) +
                                 " | Version".PadRight(maxVersionWidth) +
                                 " | Req. Nebuli Version".PadRight(maxNebuliVersionWidth) + " | SkipsVersionCheck |\n";

            string separator = new string('-', tableHeader.Length - 1) + "\n";

            StringBuilder pluginList = StringBuilderPool.Shared.Rent();

            foreach (IPlugin<IConfiguration> plugin in LoaderClass.EnabledPlugins)
            {
                string creator = plugin.Creator.Length > maxCreatorWidth
                    ? plugin.Creator.Substring(0, maxCreatorWidth - 3) + "..."
                    : plugin.Creator;
                string name = plugin.Name.Length > maxNameWidth
                    ? plugin.Name.Substring(0, maxNameWidth - 3) + "..."
                    : plugin.Name;
                string version = plugin.Version.ToString().Length > maxVersionWidth
                    ? plugin.Version.ToString().Substring(0, maxVersionWidth - 3) + "..."
                    : plugin.Version.ToString();
                string nebuliVersion = plugin.NebuliVersion.ToString().Length > maxNebuliVersionWidth
                    ? plugin.NebuliVersion.ToString().Substring(0, maxNebuliVersionWidth - 3) + "..."
                    : plugin.NebuliVersion.ToString();

                string formattedRow = $"| {creator}".PadRight(maxCreatorWidth) + $" | {name}".PadRight(maxNameWidth) +
                                      $" | {version}".PadRight(maxVersionWidth) +
                                      $" | {nebuliVersion}".PadRight(maxNebuliVersionWidth) +
                                      $" | {plugin.SkipVersionCheck} |\n";

                pluginList.Append(formattedRow);
            }

            response = $"{tableHeader}{separator}{pluginList}";

            StringBuilderPool.Shared.Return(pluginList);

            return true;
        }
    }
}