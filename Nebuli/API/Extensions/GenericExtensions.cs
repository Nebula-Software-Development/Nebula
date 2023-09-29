using Nebuli.Loader;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nebuli.API.Extensions;

/// <summary>
/// Provides extension methods for working with strings.
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

    /// <summary>
    /// Selects a random element from the list.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="source">The list from which to select a random element.</param>
    /// <returns>The randomly selected element from the list.</returns>
    public static T SelectRandom<T>(this List<T> source) => source[LoaderClass.Random.Next(source.Count)];

    /// <summary>
    /// Selects a random element from the IEnumerable.
    /// </summary>
    /// <typeparam name="T">The type of elements in the IEnumerable.</typeparam>
    /// <param name="source">The IEnumerable from which to select a random element.</param>
    /// <returns>The randomly selected element from the IEnumerable.</returns>
    public static T SelectRandom<T>(this IEnumerable<T> source) => source.ToList().SelectRandom();
}