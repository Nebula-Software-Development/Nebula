using CommandSystem;
using Nebuli.Loader;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nebuli.API.Extensions;

/// <summary>
/// Provides extension methods for useful but hard to categorize extensions.
/// </summary>
public static class GenericExtensions
{
    /// <summary>
    /// Gets the substring before the first occurrence of a specified character.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="character">The character to search for.</param>
    /// <returns>
    /// The substring before the first occurrence of the specified character, or the original string if the character is not found.
    /// </returns>
    public static string GetSubstringBeforeCharacter(this string input, char character)
    {
        int index = input.IndexOf(character);
        if (index != -1)
            return input.Substring(0, index);
        return input;
    }

    ///<summary>
    /// A dictionary that maps command handler types to their corresponding <see cref="ICommandHandler"/>.
    ///</summary>
    /// <remarks>Valid types are <see cref="RemoteAdminCommandHandler"/>, <see cref="GameConsoleCommandHandler"/>, and <see cref="ClientCommandHandler"/>.</remarks>
    public static readonly Dictionary<Type, ICommandHandler> CommandHandlers = new()
    {
    { typeof(RemoteAdminCommandHandler), CommandProcessor.RemoteAdminCommandHandler },
    { typeof(GameConsoleCommandHandler), GameCore.Console.singleton.ConsoleCommandHandler },
    { typeof(ClientCommandHandler), QueryProcessor.DotCommandHandler }
    };

    /// <summary>
    /// Selects a random element from a <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="IEnumerable{T}"/>.</typeparam>
    /// <param name="source">The <see cref="IEnumerable{T}"/> from which to select a random element.</param>
    /// <returns>The randomly selected element from the <see cref="IEnumerable{T}"/>.</returns>
    public static T SelectRandom<T>(this IEnumerable<T> source)
    {
        if (source is null || source.Count() == 0)
            return default;

        return source.ElementAt(LoaderClass.Random.Next(0, source.Count()));
    }
}