using Nebuli.API.Features.Pools;
using System.Collections.Generic;

namespace Nebuli.API.Internal;

/// <summary>
/// Represents user session data storage.
/// </summary>
public class UserSessionData
{
    /// <summary>
    /// Gets the session dictionary for storing key-value data.
    /// </summary>
    public Dictionary<object, object> SessionDictionary { get; } = DictionaryPool<object, object>.Instance.Get();

    /// <summary>
    /// Gets the session list for storing generic items.
    /// </summary>
    public List<object> SessionList { get; } = ListPool<object>.Instance.Get();

    /// <summary>
    /// Initializes a new instance of the <see cref="UserSessionData"/> class.
    /// </summary>
    public UserSessionData()
    {
    }
}