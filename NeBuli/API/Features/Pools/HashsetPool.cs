namespace Nebuli.API.Features.Pools;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Defines a system used to store and retrieve <see cref="HashSet{T}"/> objects.
/// </summary>
/// <typeparam name="T">The type of the objects in the hash set.</typeparam>
public class HashSetPool<T>
{
    private HashSetPool()
    {
    }

    /// <summary>
    /// Gets a <see cref="HashSetPool{T}"/> that stores hash sets.
    /// </summary>
    public static HashSetPool<T> Pool { get; } = new HashSetPool<T>();

    /// <summary>
    /// Retrieves a rented <see cref="HashSet{T}"/> from the pool.
    /// </summary>
    /// <returns>A rented <see cref="HashSet{T}"/> from the pool.</returns>
    public HashSet<T> Get() => NorthwoodLib.Pools.HashSetPool<T>.Shared.Rent();

    /// <summary>
    /// Returns a rented <see cref="HashSet{T}"/> back to the pool.
    /// </summary>
    /// <param name="obj">The <see cref="HashSet{T}"/> to return.</param>
    public void Return(HashSet<T> obj) => NorthwoodLib.Pools.HashSetPool<T>.Shared.Return(obj);

    /// <summary>
    /// Returns the <see cref="HashSet{T}"/> to the pool and returns its contents as an array.
    /// </summary>
    /// <param name="obj">The <see cref="HashSet{T}"/> to return.</param>
    /// <returns>The contents of the returned hashset as an array.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="obj"/> is null.</exception>
    public T[] ToArrayReturn(HashSet<T> obj)
    {
        if (obj is null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        T[] array = obj.ToArray();
        Return(obj);
        return array;
    }
}


