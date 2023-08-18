namespace Nebuli.API.Features.Enum;

/// <summary>
/// Represents the types of escape possibilities in-game.
/// </summary>
public enum EscapeType
{
    /// <summary>
    /// No Escape Scenario.
    /// </summary>
    None,

    /// <summary>
    /// Class-D Escape Scenario.
    /// </summary>
    ClassDEscape,

    /// <summary>
    /// Cuffed ClassD escape.
    /// </summary>
    CuffedClassD,

    /// <summary>
    /// Scientist escape.
    /// </summary>
    ScientistEscape,
    /// <summary>
    /// Cuffed Scientist escape.
    /// </summary>
    CuffedScientist,

    /// <summary>
    /// Unspecified or custom escape.
    /// </summary>
    PluginEscape,
}
