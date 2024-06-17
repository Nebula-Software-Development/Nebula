// -----------------------------------------------------------------------
// <copyright file=FirearmType.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

namespace Nebula.API.Features.Enum
{
    /// <summary>
    ///     Represents different types of firearms.
    /// </summary>
    public enum FirearmType
    {
        /// <summary>
        ///     Not a firearm.
        /// </summary>
        None = ItemType.None,

        /// <summary>
        ///     The COM-15 firearm.
        /// </summary>
        COM15 = ItemType.GunCOM15,

        /// <summary>
        ///     The COM-18 firearm.
        /// </summary>
        COM18 = ItemType.GunCOM18,

        /// <summary>
        ///     The E11-SR firearm.
        /// </summary>
        E11SR = ItemType.GunE11SR,

        /// <summary>
        ///     The Crossvec firearm.
        /// </summary>
        Crossvec = ItemType.GunCrossvec,

        /// <summary>
        ///     The FSP9 firearm.
        /// </summary>
        FSP9 = ItemType.GunFSP9,

        /// <summary>
        ///     The Logicer firearm.
        /// </summary>
        Logicer = ItemType.GunLogicer,

        /// <summary>
        ///     The Revolver firearm.
        /// </summary>
        Revolver = ItemType.GunRevolver,

        /// <summary>
        ///     The AK firearm.
        /// </summary>
        AK = ItemType.GunAK,

        /// <summary>
        ///     The Shotgun firearm.
        /// </summary>
        Shotgun = ItemType.GunShotgun,

        /// <summary>
        ///     The COM-45 firearm.
        /// </summary>
        COM45 = ItemType.GunCom45,

        /// <summary>
        ///     The FRMGO firearm.
        /// </summary>
        FRMGO = ItemType.GunFRMG0,

        /// <summary>
        ///     The A7 firearm.
        /// </summary>
        A7 = ItemType.GunA7,

        /// <summary>
        ///     The Micro-HID firearm.
        /// </summary>
        MircoHID = ItemType.MicroHID,

        /// <summary>
        ///     The Particle Disruptor firearm.
        /// </summary>
        ParticleDisruptor = ItemType.ParticleDisruptor
    }
}