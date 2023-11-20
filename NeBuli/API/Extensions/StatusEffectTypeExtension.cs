// -----------------------------------------------------------------------
// <copyright file=StatusEffectTypeExtension.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using CustomPlayerEffects;
using InventorySystem.Items.Usables.Scp244.Hypothermia;
using Nebuli.API.Features.Enum;
using System;

namespace Nebuli.API.Extensions;

/// <summary>
/// Extension methods for <see cref="StatusEffect"/>.
/// </summary>
public static class StatusEffectTypeExtension
{
    /// <summary>
    /// Converts a <see cref="StatusEffectBase"/> to a <see cref="StatusEffect"/>.
    /// </summary>
    /// <param name="statusEffectBase">The StatusEffectBase to convert.</param>
    public static StatusEffect GetStatusEffectType(this StatusEffectBase statusEffectBase) => statusEffectBase switch
    {
        AmnesiaItems => StatusEffect.AmnesiaItems,
        AmnesiaVision => StatusEffect.AmnesiaVision,
        Asphyxiated => StatusEffect.Asphyxiated,
        Bleeding => StatusEffect.Bleeding,
        Blinded => StatusEffect.Blinded,
        Burned => StatusEffect.Burned,
        Concussed => StatusEffect.Concussed,
        Corroding => StatusEffect.Corroding,
        Deafened => StatusEffect.Deafened,
        Decontaminating => StatusEffect.Decontaminating,
        Disabled => StatusEffect.Disabled,
        Ensnared => StatusEffect.Ensnared,
        Exhausted => StatusEffect.Exhausted,
        Flashed => StatusEffect.Flashed,
        Hemorrhage => StatusEffect.Hemorrhage,
        Invigorated => StatusEffect.Invigorated,
        BodyshotReduction => StatusEffect.BodyshotReduction,
        Poisoned => StatusEffect.Poisoned,
        Scp207 => StatusEffect.Scp207,
        Invisible => StatusEffect.Invisible,
        Sinkhole => StatusEffect.SinkHole,
        DamageReduction => StatusEffect.DamageReduction,
        MovementBoost => StatusEffect.MovementBoost,
        RainbowTaste => StatusEffect.RainbowTaste,
        SeveredHands => StatusEffect.SeveredHands,
        Stained => StatusEffect.Stained,
        Vitality => StatusEffect.Vitality,
        Hypothermia => StatusEffect.Hypothermia,
        Scp1853 => StatusEffect.Scp1853,
        CardiacArrest => StatusEffect.CardiacArrest,
        InsufficientLighting => StatusEffect.InsufficientLighting,
        SoundtrackMute => StatusEffect.SoundtrackMute,
        SpawnProtected => StatusEffect.SpawnProtected,
        Traumatized => StatusEffect.Traumatized,
        AntiScp207 => StatusEffect.AntiScp207,
        Scanned => StatusEffect.Scanned,
        PocketCorroding => StatusEffect.PocketCorroding,
        _ => StatusEffect.Unknown,
    };

    /// <summary>
    /// Converts a <see cref="StatusEffect"/> to a <see cref="StatusEffectBase"/>.
    /// </summary>
    /// <param name="effect"></param>
    public static Type EffectToType(this StatusEffect effect) => effect switch
    {
        StatusEffect.AmnesiaItems => typeof(AmnesiaItems),
        StatusEffect.AmnesiaVision => typeof(AmnesiaVision),
        StatusEffect.Asphyxiated => typeof(Asphyxiated),
        StatusEffect.Bleeding => typeof(Bleeding),
        StatusEffect.Blinded => typeof(Blinded),
        StatusEffect.Burned => typeof(Burned),
        StatusEffect.Concussed => typeof(Concussed),
        StatusEffect.Corroding => typeof(Corroding),
        StatusEffect.Deafened => typeof(Deafened),
        StatusEffect.Decontaminating => typeof(Decontaminating),
        StatusEffect.Disabled => typeof(Disabled),
        StatusEffect.Ensnared => typeof(Ensnared),
        StatusEffect.Exhausted => typeof(Exhausted),
        StatusEffect.Flashed => typeof(Flashed),
        StatusEffect.Hemorrhage => typeof(Hemorrhage),
        StatusEffect.Invigorated => typeof(Invigorated),
        StatusEffect.BodyshotReduction => typeof(BodyshotReduction),
        StatusEffect.Poisoned => typeof(Poisoned),
        StatusEffect.Scp207 => typeof(Scp207),
        StatusEffect.Invisible => typeof(Invisible),
        StatusEffect.SinkHole => typeof(Sinkhole),
        StatusEffect.DamageReduction => typeof(DamageReduction),
        StatusEffect.MovementBoost => typeof(MovementBoost),
        StatusEffect.RainbowTaste => typeof(RainbowTaste),
        StatusEffect.SeveredHands => typeof(SeveredHands),
        StatusEffect.Stained => typeof(Stained),
        StatusEffect.Vitality => typeof(Vitality),
        StatusEffect.Hypothermia => typeof(Hypothermia),
        StatusEffect.Scp1853 => typeof(Scp1853),
        StatusEffect.CardiacArrest => typeof(CardiacArrest),
        StatusEffect.InsufficientLighting => typeof(InsufficientLighting),
        StatusEffect.SoundtrackMute => typeof(SoundtrackMute),
        StatusEffect.SpawnProtected => typeof(SpawnProtected),
        StatusEffect.Traumatized => typeof(Traumatized),
        StatusEffect.AntiScp207 => typeof(AntiScp207),
        StatusEffect.Scanned => typeof(Scanned),
        StatusEffect.PocketCorroding => typeof(PocketCorroding),
        _ => typeof(StatusEffectBase),
    };

    /// <summary>
    /// Gets if the <see cref="StatusEffect"/> is a harmful effect.
    /// </summary>
    public static bool IsHarmfulEffect(this StatusEffect statusEffect)
    {
        return statusEffect switch
        {
            StatusEffect.Asphyxiated => true,
            StatusEffect.Bleeding => true,
            StatusEffect.Corroding => true,
            StatusEffect.Decontaminating => true,
            StatusEffect.Hemorrhage => true,
            StatusEffect.Hypothermia => true,
            StatusEffect.Poisoned => true,
            StatusEffect.Scp207 => true,
            StatusEffect.SeveredHands => true,
            _ => false,
        };
    }

    /// <summary>
    /// Gets if the <see cref="StatusEffect"/> is a healing effect.
    /// </summary>
    public static bool IsHealing(this StatusEffect effect) => typeof(IHealablePlayerEffect).IsAssignableFrom(effect.EffectToType());
}