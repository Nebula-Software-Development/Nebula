// -----------------------------------------------------------------------
// <copyright file=TimedExplosiveProjectile.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem.Items.ThrowableProjectiles;

namespace Nebula.API.Features.Items.Projectiles
{
    public class TimedExplosiveProjectile : Projectile
    {
        internal TimedExplosiveProjectile(TimeGrenade timeGrenade) : base(timeGrenade)
        {
            Base = timeGrenade;
        }

        /// <summary>
        ///     Gets the <see cref="TimeGrenade" /> base.
        /// </summary>
        public new TimeGrenade Base { get; }

        /// <summary>
        ///     Gets if the grenade is detonated.
        /// </summary>
        public bool Detonated => Base._alreadyDetonated;

        /// <summary>
        ///     Gets or sets if the owner was the server.
        /// </summary>
        public bool WasServer
        {
            get => Base._wasServer;
            set => Base._wasServer = value;
        }

        /// <summary>
        ///     Gets or sets the fuze time of the grenade.
        /// </summary>
        public float FuzeTime
        {
            get => Base._fuseTime;
            set => Base._fuseTime = value;
        }

        /// <summary>
        ///     Detonates the grenade.
        /// </summary>
        public void Detonate()
        {
            Base.ServerFuseEnd();
            Base._alreadyDetonated = true;
        }
    }
}