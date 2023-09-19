namespace Nebuli.API.Extensions;

/// <summary>
/// Provides extension methods for working with strings.
/// </summary>
public static class StringExtensions
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
}