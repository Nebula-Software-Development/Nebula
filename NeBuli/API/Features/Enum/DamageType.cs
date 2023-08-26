namespace Nebuli.API.Features;

/// <summary>
/// Represents the types of damage in the game.
/// </summary>
public enum DamageType
{
    /// <summary>
    /// Unknown damage source.
    /// </summary>
    Unknown,

    /// <summary>
    /// Damage caused by SCP-049's cardiac arrest attack.
    /// </summary>
    CardiacArrest,

    /// <summary>
    /// Damage caused by the Micro-HID.
    /// </summary>
    MicroHID,

    /// <summary>
    /// Damage caused by the particle disruptor.
    /// </summary>
    ParticleDisruptor,

    /// <summary>
    /// Universal damage type.
    /// </summary>
    Universal,

    /// <summary>
    /// Damage caused by recontainment.
    /// </summary>
    Recontainment,

    /// <summary>
    /// Damage caused by a fall.
    /// </summary>
    Falldown,

    /// <summary>
    /// Damage dealt by a firearm when the ItemType used is not available.
    /// </summary>
    Firearm,

    /// <summary>
    /// Damage dealt by a MicroHID.
    /// </summary>
    MicroHid,

    /// <summary>
    /// Damage caused by a Tesla Gate.
    /// </summary>
    Tesla,

    /// <summary>
    /// Alpha Warhead detonation.
    /// </summary>
    Warhead,

    /// <summary>
    /// LCZ Decontamination.
    /// </summary>
    Decontamination,

    /// <summary>
    /// Asphyxiation damage.
    /// </summary>
    Asphyxiation,

    /// <summary>
    /// Poison damage.
    /// </summary>
    Poison,

    /// <summary>
    /// Bleeding damage.
    /// </summary>
    Bleeding,

    /// <summary>
    /// Damage caused by frag grenades.
    /// </summary>
    Explosion,

    /// <summary>
    /// Damage caused by a Jailbird.
    /// </summary>
    Jailbird,

    /// <summary>
    /// Damage caused by a custom source.
    /// </summary>
    Custom,

    /// <summary>
    /// Damage caused by a Logicer.
    /// </summary>
    Logicer,

    /// <summary>
    /// Damage caused by a Revolver.
    /// </summary>
    Revolver,

    /// <summary>
    /// Damage caused by a Com45.
    /// </summary>
    Com45,

    /// <summary>
    /// Damage dealt by SCP-018.
    /// </summary>
    Scp018,

    /// <summary>
    /// Damage caused by SCP-049's attack.
    /// </summary>
    Scp049,

    /// <summary>
    /// Damage caused by SCP-096's attack.
    /// </summary>
    Scp096,

    /// <summary>
    /// Damage caused by SCP-106's attack.
    /// </summary>
    Scp106,

    /// <summary>
    /// Damage caused by SCP-173's attack.
    /// </summary>
    Scp173,

    /// <summary>
    /// Damage caused by SCP-939's attack.
    /// </summary>
    Scp939,

    /// <summary>
    /// Damage caused by SCP-049-2's attack.
    /// </summary>
    Scp0492,


    /// <summary>
    /// Damage caused by the pocket dimension.
    /// </summary>
    PocketDimension,

    /// <summary>
    /// Damage caused by severed hands.
    /// </summary>
    SeveredHands,
}

