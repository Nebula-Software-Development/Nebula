﻿// -----------------------------------------------------------------------
// <copyright file=Locker.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Interactables.Interobjects.DoorUtils;
using MapGeneration.Distributors;
using UnityEngine;
using LockerBase = MapGeneration.Distributors.Locker;

namespace Nebula.API.Features.Map
{
    public class Locker
    {
        /// <summary>
        ///     Gets the <see cref="LockerBase" /> and <see cref="Locker" /> dictionary.
        /// </summary>
        public static readonly Dictionary<LockerBase, Locker> Dictionary = new();

        public Locker(LockerBase lockerBase)
        {
            Base = lockerBase;
        }

        /// <summary>
        ///     Gets the <see cref="LockerBase" /> base.
        /// </summary>
        public LockerBase Base { get; }

        public static IEnumerable<Locker> Collection => Dictionary.Values;

        /// <summary>
        ///     Gets a list of all the lockers on the server.
        /// </summary>
        public static List<Locker> List => Collection.ToList();

        /// <summary>
        ///     Gets the lockers <see cref="UnityEngine.GameObject" />.
        /// </summary>
        public GameObject GameObject => Base.gameObject;

        /// <summary>
        ///     Gets the lockers <see cref="LockerChamber" />.
        /// </summary>
        public LockerChamber[] LockerChambers => Base.Chambers;

        /// <summary>
        ///     Gets the lockers <see cref="MapGeneration.Distributors.StructureType" />.
        /// </summary>
        public StructureType StructureType => Base.StructureType;

        /// <summary>
        ///     Checks if the permission and player can open the locker.
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool CheckLockerPerms(KeycardPermissions permissions, Player player)
        {
            return Base.CheckPerms(permissions, player.ReferenceHub);
        }

        /// <summary>
        ///     Fills the specified <see cref="LockerChamber" />.
        /// </summary>
        /// <param name="chamber"></param>
        public void FillChamber(LockerChamber chamber)
        {
            Base.FillChamber(chamber);
        }

        /// <summary>
        ///     Gets a <see cref="Locker" /> with the specified <see cref="LockerBase" />.
        /// </summary>
        public static Locker Get(LockerBase locker)
        {
            return Dictionary.TryGetValue(locker, out Locker locker1) ? locker1 : new Locker(locker);
        }
    }
}