// -----------------------------------------------------------------------
// <copyright file=AmesticCloudHazard.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using PlayerRoles.PlayableScps.Scp939;

namespace Nebuli.API.Features.Hazards
{
    public class AmesticCloudHazard : TemporaryHazard
    {
        internal AmesticCloudHazard(Scp939AmnesticCloudInstance hazardBase) : base(hazardBase)
        {
            Base = hazardBase;
            Owner = Player.Get(Base._cloud.Owner);
        }

        /// <summary>
        ///     Gets the <see cref="Scp939AmnesticCloudInstance" /> base.
        /// </summary>
        public new Scp939AmnesticCloudInstance Base { get; }

        /// <summary>
        ///     Gets the owner of the <see cref="AmesticCloudHazard" />.
        /// </summary>
        public Player Owner { get; }

        /// <summary>
        ///     Gets if the <see cref="AmesticCloudHazard" /> is already created.
        /// </summary>
        public bool AlreadyCreated => Base._alreadyCreated;

        /// <summary>
        ///     Gets or sets the amnesia duration of the <see cref="AmesticCloudHazard" />.
        /// </summary>
        public float AmnesiaDuration
        {
            get => Base._amnesiaDuration;
            set => Base._amnesiaDuration = value;
        }

        /// <summary>
        ///     Gets or sets the <see cref="Scp939AmnesticCloudInstance.CloudState" />.
        /// </summary>
        public Scp939AmnesticCloudInstance.CloudState State
        {
            get => Base.State;
            set => Base.State = value;
        }

        /// <summary>
        ///     Gets or sets the target duration of the <see cref="AmesticCloudHazard" />.
        /// </summary>
        public float TargetDuration
        {
            get => Base._targetDuration;
            set => Base._targetDuration = value;
        }

        /// <summary>
        ///     Gets or sets the minimum time needed to press the place key down.
        /// </summary>
        public float MinKeyHoldTime
        {
            get => Base._minHoldTime;
            set => Base._minHoldTime = value;
        }

        /// <summary>
        ///     Gets or sets the last hold time.
        /// </summary>
        public float LasKeytHoldTime
        {
            get => Base._lastHoldTime;
            set => Base._lastHoldTime = value;
        }

        /// <summary>
        ///     Gets or sets the maximum time needed to press the place key down.
        /// </summary>
        public float MaxKeyHoldTime
        {
            get => Base._maxHoldTime;
            set => Base._maxHoldTime = value;
        }

        /// <summary>
        ///     Gets or sets the pause duration.
        /// </summary>
        public float PauseDuration
        {
            get => Base._pauseDuration;
            set => Base._pauseDuration = value;
        }
    }
}