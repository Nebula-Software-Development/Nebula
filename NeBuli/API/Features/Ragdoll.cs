using Nebuli.API.Features.Player;
using PlayerRoles;
using PlayerStatsSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Nebuli.API.Features
{
    /// <summary>
    /// Allows easier use of in-game ragdolls by wrapping the <see cref="BasicRagdoll"/> class.
    /// </summary>
    public class Ragdoll
    {
        /// <summary>
        /// Gets a Dictionary of <see cref="BasicRagdoll"/>, and their wrapper class <see cref="Ragdoll"/>. 
        /// </summary>
        public Dictionary<BasicRagdoll, Ragdoll> Ragdolls { get; } = new();

        /// <summary>
        /// Gets the <see cref="BasicRagdoll"/> that this class is wrapping.
        /// </summary>
        public BasicRagdoll Basicragdoll { get; private set; }

        /// <summary>
        /// Gets the RagdollData from the <see cref="BasicRagdoll"/>.
        /// </summary>
        public RagdollData RagdollData => Basicragdoll.Info;

        /// <summary>
        /// Tries to get the player assosiated with the ragdoll.
        /// </summary>
        public NebuliPlayer OwnerPlayer => NebuliPlayer.Get(ReferenceHub);

        internal Ragdoll(BasicRagdoll basicRagdoll)
        {
            Basicragdoll = basicRagdoll;
            Ragdolls.Add(basicRagdoll, this);
        }

        /// <summary>
        /// Gets the owner's ReferenceHub of the ragdoll.
        /// </summary>
        public ReferenceHub ReferenceHub
        {
            get => Basicragdoll.NetworkInfo.OwnerHub;
        }

        /// <summary>
        /// Determines if the Ragdoll is frozen or not.
        /// </summary>
        public bool IsFrozen
        {
            get => Basicragdoll._frozen;
            set => Basicragdoll._frozen = value; 
        }

        /// <summary>
        /// Gets the ragdolls transform.
        /// </summary>
        public Transform Transform
        {
            get => Basicragdoll.transform;
        }

        /// <summary>
        /// Gets the ragdolls GameObject.
        /// </summary>
        public GameObject GameObject
        { 
            get => Basicragdoll.gameObject;
        }

        /// <summary>
        /// Gets or sets the ragdolls origin point.
        /// </summary>
        public Transform OriginPoint
        {
            get => Basicragdoll._originPoint;
            set => Basicragdoll._originPoint = value;
        }

        /// <summary>
        /// Gets the ragdolls starting position.
        /// </summary>
        public Vector3 StartPosition
        {
            get => Basicragdoll.NetworkInfo.StartPosition;
        }

        /// <summary>
        /// Gets the <see cref="PlayerRoles.RoleTypeId"/> of the ragdoll.
        /// </summary>
        public RoleTypeId RoleTypeId
        {
            get => Basicragdoll.NetworkInfo.RoleType;
        }

        /// <summary>
        /// Gets the <see cref="PlayerStatsSystem.DamageHandlerBase"/> of the ragdoll.
        /// </summary>
        public DamageHandlerBase DamageHandlerBase
        {
            get => Basicragdoll.NetworkInfo.Handler;
        }

        /// <summary>
        /// Gets the Nickname of the ragdoll.
        /// </summary>
        public string RagdollName
        {
            get => Basicragdoll.NetworkInfo.Nickname;
        }

        /// <summary>
        /// Gets the ragdolls start rotation.
        /// </summary>
        public Quaternion RagdollRotation
        {
            get => Basicragdoll.NetworkInfo.StartRotation;
        }
       
    }
}
