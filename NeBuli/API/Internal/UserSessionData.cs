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
    public Dictionary<object, object> SessionDictionary { get; }

    /// <summary>
    /// Gets the session list for storing generic items.
    /// </summary>
    public List<object> SessionList { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserSessionData"/> class.
    /// </summary>
    public UserSessionData()
    {
        SessionDictionary = DictionaryPool<object, object>.Instance.Get();
        SessionList = ListPool<object>.Instance.Get();      
    }

    ~UserSessionData() 
    {
        DictionaryPool<object, object>.Instance.Return(SessionDictionary);
        ListPool<object>.Instance.Return(SessionList);
    }
}