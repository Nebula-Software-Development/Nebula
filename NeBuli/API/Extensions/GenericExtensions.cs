// -----------------------------------------------------------------------
// <copyright file=GenericExtensions.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Nebuli.Loader;
using RemoteAdmin;
using Console = GameCore.Console;

namespace Nebuli.API.Extensions
{
    /// <summary>
    ///     Provides extension methods for useful but hard to categorize extensions.
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        ///     A dictionary that maps command handler types to their corresponding <see cref="ICommandHandler" />.
        /// </summary>
        /// <remarks>
        ///     Valid types are <see cref="RemoteAdminCommandHandler" />, <see cref="GameConsoleCommandHandler" />, and
        ///     <see cref="ClientCommandHandler" />.
        /// </remarks>
        public static readonly Dictionary<Type, ICommandHandler> CommandHandlers = new()
        {
            { typeof(RemoteAdminCommandHandler), CommandProcessor.RemoteAdminCommandHandler },
            { typeof(GameConsoleCommandHandler), Console.singleton.ConsoleCommandHandler },
            { typeof(ClientCommandHandler), QueryProcessor.DotCommandHandler }
        };

        /// <summary>
        ///     Gets the substring before the first occurrence of a specified character.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="character">The character to search for.</param>
        /// <returns>
        ///     The substring before the first occurrence of the specified character, or the original string if the character is
        ///     not found.
        /// </returns>
        public static string GetSubstringBeforeCharacter(this string input, char character)
        {
            int index = input.IndexOf(character);
            return index != -1 ? input.Substring(0, index) : input;
        }

        /// <summary>
        ///     Selects a random element from a <see cref="IEnumerable{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable{T}" />.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}" /> from which to select a random element.</param>
        /// <returns>The randomly selected element from the <see cref="IEnumerable{T}" />.</returns>
        public static T SelectRandom<T>(this IEnumerable<T> source)
        {
            IEnumerable<T> enumerable = source as T[] ?? source.ToArray();
            return !enumerable.Any() ? default : enumerable.ElementAt(LoaderClass.Random.Next(0, enumerable.Count()));
        }

        /// <summary>
        ///     Adds a key-value pair to the dictionary if the key is not already present.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to add the key-value pair to.</param>
        /// <param name="key">The key to add.</param>
        /// <param name="value">The value associated with the key.</param>
        /// <returns>
        ///     <c>True</c> if the key is already present in the dictionary; otherwise, <c>false</c>.
        /// </returns>
        public static bool AddIfMissing<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                return true;
            }

            dictionary.Add(key, value);
            return false;

        }

        /// <summary>
        ///     Updates the value for an existing key in the dictionary or adds the key-value pair if the key doesn't exist.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to update or add the key-value pair to.</param>
        /// <param name="key">The key to update or add.</param>
        /// <param name="value">The new value associated with the key.</param>
        /// <returns>
        ///     <c>True</c> if the key was updated (it already existed), <c>false</c> if the key-value pair was added (it didn't
        ///     exist).
        /// </returns>
        public static bool UpdateOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
                return true;
            }

            dictionary.Add(key, value);
            return false;
        }

        /// <summary>
        ///     Removes a key and its associated value from the dictionary if the key is present.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
        /// <param name="dictionary">The dictionary to remove the key-value pair from.</param>
        /// <param name="key">The key to remove.</param>
        /// <returns>
        ///     <c>True</c> if the key was found and removed; otherwise, <c>false</c>.
        /// </returns>
        public static bool RemoveIfContains<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            if (!dictionary.ContainsKey(key))
            {
                return false;
            }

            dictionary.Remove(key);
            return true;

        }
    }
}