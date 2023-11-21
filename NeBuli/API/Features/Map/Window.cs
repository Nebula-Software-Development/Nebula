// -----------------------------------------------------------------------
// <copyright file=Window.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using PlayerRoles;
using PlayerStatsSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WindowBase = BreakableWindow;

namespace Nebuli.API.Features.Map;

public class Window
{
    /// <summary>
    /// Gets the <see cref="WindowBase"/> base.
    /// </summary>
    public WindowBase Base { get; }

    /// <summary>
    /// Gets the <see cref="WindowBase"/>, and its wrapper class, <see cref="Window"/>.
    /// </summary>
    public static Dictionary<WindowBase, Window> Dictionary = new();

    internal Window(WindowBase window)
    {
        Base = window;
        Dictionary.Add(window, this);
    }

    /// <summary>
    /// Gets a collection of all the windows.
    /// </summary>
    public static IEnumerable<Window> Collection => Dictionary.Values;

    /// <summary>
    /// Gets a list of all the windows.
    /// </summary>
    public static List<Window> List => Collection.ToList();

    /// <summary>
    /// Gets the center of mass of the window.
    /// </summary>
    public Vector3 CenterOfMass => Base.CenterOfMass;

    /// <summary>
    /// Checks if the specified role has permission to cause damage.
    /// </summary>
    /// <param name="role">The RoleTypeId to check for damage permissions.</param>
    /// <returns><c>true</c> if the specified role has damage permissions, otherwise <c>false</c>.</returns>
    public bool CheckDamagePermissions(RoleTypeId role) => Base.CheckDamagePerms(role);

    /// <summary>
    /// Inflicts damage to the GameObject.
    /// </summary>
    /// <param name="damageAmount">The amount of damage to be inflicted.</param>
    /// <param name="damageHandler">The <see cref="DamageHandlerBase"/> responsible for handling the damage.</param>
    /// <param name="position">The position at which the damage is inflicted.</param>
    public void Damage(float damageAmount, DamageHandlerBase damageHandler, Vector3 position) => Base.Damage(damageAmount, damageHandler, position);

    /// <summary>
    /// Gets the underlying GameObject of this instance.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets the Transform component of the underlying GameObject.
    /// </summary>
    public Transform Transform => Base.transform;

    /// <summary>
    /// Gets the last attacker of this GameObject as a <see cref="Player"/>.
    /// </summary>
    public Player LastAttacker => Player.Get(Base.LastAttacker);

    /// <summary>
    /// Gets a value indicating whether the GameObject is currently broken.
    /// </summary>
    public bool IsBroken => Base.NetworksyncStatus.broken;

    /// <summary>
    /// Gets or sets a value indicating whether SCPs should be prevented from damaging this GameObject.
    /// </summary>
    public bool PreventSCPDamange
    {
        get => Base._preventScpDamage;
        set => Base._preventScpDamage = value;
    }

    /// <summary>
    /// Gets or creates a new window with the <see cref="WindowBase"/>.
    /// </summary>
    /// <param name="breakableWindow"></param>
    /// <returns></returns>
    public static Window Get(WindowBase breakableWindow) => Dictionary.TryGetValue(breakableWindow, out Window window) ? window : new Window(breakableWindow);
}