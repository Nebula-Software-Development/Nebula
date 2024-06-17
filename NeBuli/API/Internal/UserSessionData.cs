// -----------------------------------------------------------------------
// <copyright file=UserSessionData.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Nebula.API.Features.Pools;

namespace Nebula.API.Internal
{
    /// <summary>
    ///     Represents user session data storage.
    /// </summary>
    public class UserSessionData
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserSessionData" /> class.
        /// </summary>
        public UserSessionData()
        {
            SessionDictionary = DictionaryPool<object, object>.Instance.Get();
            SessionList = ListPool<object>.Instance.Get();
        }

        /// <summary>
        ///     Gets the session dictionary for storing key-value data.
        /// </summary>
        public Dictionary<object, object> SessionDictionary { get; }

        /// <summary>
        ///     Gets the session list for storing generic items.
        /// </summary>
        public List<object> SessionList { get; }

        ~UserSessionData()
        {
            DictionaryPool<object, object>.Instance.Return(SessionDictionary);
            ListPool<object>.Instance.Return(SessionList);
        }
    }
}