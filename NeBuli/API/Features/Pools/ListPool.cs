// -----------------------------------------------------------------------
// <copyright file=ListPool.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using NorthwoodLib.Pools;

namespace Nebula.API.Features.Pools
{
    /// <summary>
    ///     Provides pooling functionality for <see cref="List{T}" /> objects.
    /// </summary>
    /// <typeparam name="T">The type of objects stored in the lists.</typeparam>
    public class ListPool<T> : IPool<List<T>>
    {
        private ListPool()
        {
        }

        /// <summary>
        ///     Gets a <see cref="ListPool{T}" /> instance for storing lists.
        /// </summary>
        public static ListPool<T> Instance { get; } = new();

        /// <summary>
        ///     Rents a <see cref="List{T}" /> from the pool.
        /// </summary>
        /// <returns>The rented <see cref="List{T}" />.</returns>
        public List<T> Rent()
        {
            return Get();
        }

        /// <summary>
        ///     Returns a pooled <see cref="List{T}" /> instance to the pool.
        /// </summary>
        /// <param name="obj">The list to return to the pool.</param>
        public void Return(List<T> obj)
        {
            NorthwoodLib.Pools.ListPool<T>.Shared.Return(obj);
        }

        /// <inheritdoc />
        public List<T> Get()
        {
            return NorthwoodLib.Pools.ListPool<T>.Shared.Rent();
        }

        /// <summary>
        ///     Retrieves a pooled <see cref="List{T}" /> instance with a specified initial capacity.
        /// </summary>
        /// <param name="capacity">The initial capacity of the list.</param>
        /// <returns>The pooled list instance.</returns>
        public List<T> Get(int capacity)
        {
            return NorthwoodLib.Pools.ListPool<T>.Shared.Rent(capacity);
        }

        /// <summary>
        ///     Retrieves a pooled <see cref="List{T}" /> instance filled with provided items.
        /// </summary>
        /// <param name="items">The items to populate the list with.</param>
        /// <returns>The pooled list instance.</returns>
        public List<T> Get(IEnumerable<T> items)
        {
            return NorthwoodLib.Pools.ListPool<T>.Shared.Rent(items);
        }

        /// <summary>
        ///     Returns a pooled <see cref="List{T}" /> instance to the pool and extracts its contents as an array.
        /// </summary>
        /// <param name="obj">The list to return to the pool.</param>
        /// <returns>An array containing the contents of the returned list.</returns>
        public T[] ToArrayReturn(List<T> obj)
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
}