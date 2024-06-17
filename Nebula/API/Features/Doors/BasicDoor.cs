// -----------------------------------------------------------------------
// <copyright file=BasicDoor.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using BasicDoorBase = Interactables.Interobjects.BasicDoor;

namespace Nebula.API.Features.Doors
{
    public class BasicDoor : Door
    {
        internal BasicDoor(BasicDoorBase basicDoor) : base(basicDoor)
        {
            Base = basicDoor;
        }

        /// <summary>
        ///     Gets the <see cref="BasicDoorBase" /> base.
        /// </summary>
        public new BasicDoorBase Base { get; }

        /// <summary>
        ///     Forces the door to play a beep sound.
        /// </summary>
        /// <param name="deniedSound">If the beep sound should be a denied sound.</param>
        public void PlayBeepSound(bool deniedSound)
        {
            Base.RpcPlayBeepSound(deniedSound);
        }
    }
}