// -----------------------------------------------------------------------
// <copyright file=StatusEffectType.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

namespace Nebula.API.Features.Enum
{
    /// <summary>
    ///     Various status effects that can affect players.
    /// </summary>
    public enum StatusEffect
    {
        /// <summary>
        ///     A unknown effect.
        /// </summary>
        Unknown,

        /// <summary>
        ///     Effect given to the player when they are unable to open their inventory or reload a weapon.
        /// </summary>
        AmnesiaItems,

        /// <summary>
        ///     Effect given to the player when they are unable to see properly.
        /// </summary>
        AmnesiaVision,

        /// <summary>
        ///     Effect given to the player when their stamina is drained and subsequently their health.
        /// </summary>
        Asphyxiated,

        /// <summary>
        ///     Effect given to the player when they suffer continuous damage over time.
        /// </summary>
        Bleeding,

        /// <summary>
        ///     Effect given to the player when their screen is blurred, impairing their vision.
        /// </summary>
        Blinded,

        /// <summary>
        ///     Effect given to the player when they receive increased damage but does not apply standalone damage.
        /// </summary>
        Burned,

        /// <summary>
        ///     Effect given to the player when their screen is blurred and rotation is induced.
        /// </summary>
        Concussed,

        /// <summary>
        ///     Effect given to the player after being harmed by SCP-106.
        /// </summary>
        Corroding,

        /// <summary>
        ///     Effect given to the player when they are deafened, rendering them unable to hear.
        /// </summary>
        Deafened,

        /// <summary>
        ///     Effect given to the player when 10% of their health is removed per second.
        /// </summary>
        Decontaminating,

        /// <summary>
        ///     Effect given to the player when their movement is slowed down.
        /// </summary>
        Disabled,

        /// <summary>
        ///     Effect given to the player when they are prevented from moving.
        /// </summary>
        Ensnared,

        /// <summary>
        ///     Effect given to the player when their maximum stamina and stamina regeneration rate are halved.
        /// </summary>
        Exhausted,

        /// <summary>
        ///     Effect given to the player when their screen is flashed.
        /// </summary>
        Flashed,

        /// <summary>
        ///     Effect given to the player when their health is drained while sprinting.
        /// </summary>
        Hemorrhage,

        /// <summary>
        ///     Effect given to the player when their Field of View (FOV) is reduced, they receive infinite stamina, and underwater
        ///     sound is simulated.
        /// </summary>
        Invigorated,

        /// <summary>
        ///     Effect given to the player when they receive reduced damage from body shots.
        /// </summary>
        BodyshotReduction,

        /// <summary>
        ///     Effect given to the player when they are inflicted with damage every 5 seconds, starting with low damage and
        ///     increasing over time.
        /// </summary>
        Poisoned,

        /// <summary>
        ///     Effect given to the player when their movement speed is increased while their health is drained.
        /// </summary>
        Scp207,

        /// <summary>
        ///     Effect given to the player when they become invisible.
        /// </summary>
        Invisible,

        /// <summary>
        ///     Effect given to the player when they walk on a sinkhole.
        /// </summary>
        SinkHole,

        /// <summary>
        ///     Effect given to the player when they experience reduced overall damage taken.
        /// </summary>
        DamageReduction,

        /// <summary>
        ///     Effect given to the player when their movement speed is increased.
        /// </summary>
        MovementBoost,

        /// <summary>
        ///     Effect given to the player when they experience severely reduced damage taken.
        /// </summary>
        RainbowTaste,

        /// <summary>
        ///     Effect given to the player when they take more than two candies from SCP-330.
        /// </summary>
        SeveredHands,

        /// <summary>
        ///     Effect given to the player when they are prevented from sprinting and their movement speed is reduced by 20%.
        /// </summary>
        Stained,

        /// <summary>
        ///     Effect given to the player when their health gradually regenerates over time.
        /// </summary>
        Vitality,

        /// <summary>
        ///     Effect given to the player when they suffer from hypothermia.
        /// </summary>
        Hypothermia,

        /// <summary>
        ///     Effect given to the player to enhance their combat effectiveness.
        /// </summary>
        Scp1853,

        /// <summary>
        ///     Effect given to the player after being harmed by SCP-049.
        /// </summary>
        CardiacArrest,

        /// <summary>
        ///     Effect given to players in an area with insufficient light.
        /// </summary>
        InsufficientLighting,

        /// <summary>
        ///     Effect that disables ambient sound.
        /// </summary>
        SoundtrackMute,

        /// <summary>
        ///     Effect provided to the player that offers protection from enemies when enabled.
        /// </summary>
        SpawnProtected,

        /// <summary>
        ///     Effect given to players that allows SCP-106 to see them when he is in the ground.
        /// </summary>
        Traumatized,

        /// <summary>
        ///     Effect similar to Scp207 but heals instead of causing harm.
        /// </summary>
        AntiScp207,

        /// <summary>
        ///     Effect that SCP-079 applies to the scanned player with the Breach Scanner.
        /// </summary>
        Scanned,

        /// <summary>
        ///     Effect applied when SCP-106 teleports the player to the pocket dimension.
        /// </summary>
        PocketCorroding
    }
}