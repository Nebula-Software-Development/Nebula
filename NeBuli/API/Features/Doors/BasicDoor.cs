// -----------------------------------------------------------------------
// <copyright file=BasicDoor.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using BasicDoorBase = Interactables.Interobjects.BasicDoor;

namespace Nebuli.API.Features.Doors;

public class BasicDoor : Door
{
    /// <summary>
    /// Gets the <see cref="BasicDoorBase"/> base.
    /// </summary>
    public new BasicDoorBase Base { get; }

    internal BasicDoor(BasicDoorBase basicDoor) : base(basicDoor)
    {
        Base = basicDoor;
    }

    /// <summary>
    /// Forces the door to play a beep sound.
    /// </summary>
    /// <param name="deniedSound">If the beep sound should be a denied sound.</param>
    public void PlayBeepSound(bool deniedSound) => Base.RpcPlayBeepSound(deniedSound);
}