using MapGeneration.Distributors;

namespace Nebuli.API.Features.Enum;

public enum GeneratorState
{
    None = Scp079Generator.GeneratorFlags.None,
    UnLocked = Scp079Generator.GeneratorFlags.Unlocked,
    Open = Scp079Generator.GeneratorFlags.Open,
    Activating = Scp079Generator.GeneratorFlags.Activating,
    Engaged = Scp079Generator.GeneratorFlags.Engaged,
}