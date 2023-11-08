// -----------------------------------------------------------------------
// <copyright file=DictionaryPool.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Nebuli.API.Features.Pools;

/// <summary>
/// Represents a pool of dictionaries that can be reused.
/// </summary>
/// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
/// <typeparam name="TValue">The type of the dictionary values.</typeparam>
public class DictionaryPool<TKey, TValue>
{
    private static readonly ConcurrentQueue<Dictionary<TKey, TValue>> Pool = new();
    private static readonly int DefaultInitialCapacity = 10;

    private DictionaryPool()
    {
    }

    /// <summary>
    /// Gets the singleton instance of the <see cref="DictionaryPool{TKey, TValue}"/>.
    /// </summary>
    public static DictionaryPool<TKey, TValue> Instance { get; } = new();

    /// <summary>
    /// Gets a dictionary from the pool or creates a new one if needed.
    /// </summary>
    /// <returns>A dictionary instance.</returns>
    public Dictionary<TKey, TValue> Get()
    {
        if (Pool.TryDequeue(out Dictionary<TKey, TValue> result))
        {
            result.Clear();
            return result;
        }

        return CreateDictionary();
    }

    /// <summary>
    /// Gets a dictionary from the pool, fills it with provided pairs, and returns it.
    /// </summary>
    /// <param name="pairs">Pairs to fill the dictionary with.</param>
    /// <returns>A dictionary instance.</returns>
    public Dictionary<TKey, TValue> Get(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
    {
        Dictionary<TKey, TValue> dictionary = Get();

        foreach (var pair in pairs)
        {
            dictionary.Add(pair.Key, pair.Value);
        }

        return dictionary;
    }

    /// <summary>
    /// Returns a dictionary to the pool for reuse.
    /// </summary>
    /// <param name="dictionary">The dictionary to return.</param>
    public void Return(Dictionary<TKey, TValue> dictionary)
    {
        if (dictionary != null)
        {
            dictionary.Clear();
            Pool.Enqueue(dictionary);
        }
    }

    private static Dictionary<TKey, TValue> CreateDictionary() => new(DefaultInitialCapacity);
}