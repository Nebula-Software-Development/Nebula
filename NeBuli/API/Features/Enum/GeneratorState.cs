using MapGeneration.Distributors;

namespace Nebuli.API.Features.Enum;

public enum GeneratorState
{
    /// <summary>
    /// The generator state at idle.
    /// </summary>
    None = Scp079Generator.GeneratorFlags.None,

    /// <summary>
    /// The generator state while unlocked.
    /// </summary>
    UnLocked = Scp079Generator.GeneratorFlags.Unlocked,

    /// <summary>
    /// The generator state while opened.
    /// </summary>
    Open = Scp079Generator.GeneratorFlags.Open,

    /// <summary>
    /// The generator state while activating.
    /// </summary>
    Activating = Scp079Generator.GeneratorFlags.Activating,

    /// <summary>
    /// The generator state while engaged.
    /// </summary>
    Engaged = Scp079Generator.GeneratorFlags.Engaged,
}