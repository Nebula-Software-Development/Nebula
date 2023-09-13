namespace Nebuli.API.Features.Enum;

/// <summary>
/// Represents different types of possible escapes in-game.
/// </summary>
public enum EscapeType
{
    /// <summary>
    /// None.
    /// </summary>
    None,

    /// <summary>
    /// Class-D escape type.
    /// </summary>
    ClassD,

    /// <summary>
    /// Cuffed Class-D escape type.
    /// </summary>
    CuffedClassD,

    /// <summary>
    /// Scientist escape type.
    /// </summary>
    Scientist,

    /// <summary>
    /// Cuffed scientist escape type.
    /// </summary>
    CuffedScientist,

    /// <summary>
    /// A custom/plugin escape type.
    /// </summary>
    PluginEscape,
}