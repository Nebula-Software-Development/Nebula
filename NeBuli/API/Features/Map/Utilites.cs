using Hazards;
using PlayerRoles.PlayableScps.Scp173;
using PlayerRoles;
using Mirror;
using RelativePositioning;
using UnityEngine;
using Object = UnityEngine.Object;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.API.Features.Items;
using InventorySystem;
using Decals;
using InventorySystem.Items.Firearms.BasicMessages;
using Utils.Networking;
using Nebuli.API.Features.Player;

namespace Nebuli.API.Features.Map
{
    public static class Utilites
    {
        private static TantrumEnvironmentalHazard tantrumPrefab = null;

        //Credit to EXILED for method of spawning SCP-173 tantrums
        public static TantrumEnvironmentalHazard TantrumPrefab
        {
            get
            {
                if (tantrumPrefab == null)
                {
                    if (PlayerRoleLoader.TryGetRoleTemplate(RoleTypeId.Scp173, out PlayerRoleBase role))
                    {
                        Scp173Role scp173Role = role as Scp173Role;
                        if (scp173Role.SubroutineModule.TryGetSubroutine(out Scp173TantrumAbility scp173TantrumAbility))
                            tantrumPrefab = scp173TantrumAbility._tantrumPrefab;
                    }
                }
                return tantrumPrefab;
            }
        }

        //Credit to EXILED for method of spawning SCP-173 tantrums
        /// <summary>
        /// Places a SCP-173 tantrum.
        /// </summary>
        /// <param name="position">The positon to place it.</param>
        /// <returns></returns>
        public static GameObject PlaceTantrum(Vector3 position)
        {
            TantrumEnvironmentalHazard tantrum = Object.Instantiate(TantrumPrefab);
            tantrum.SynchronizedPosition = new RelativePosition(position + (Vector3.up * 0.25f));
            NetworkServer.Spawn(tantrum.gameObject);
            return tantrum.gameObject;
        }

        /// <summary>
        /// Destroys all pickups in the Pickup List.
        /// </summary>
        public static void CleanAllPickups()
        {
            foreach (Pickup pickup in Pickup.List)
                pickup.Destroy();
        }

        /// <summary>
        /// Destroys all ragdolls in the Ragdoll List.
        /// </summary>
        public static void CleanAllRagdolls()
        {
            foreach(Ragdoll ragdoll in Ragdoll.List)
                ragdoll.Destroy();
        }

        /// <summary>
        /// Places a decal on the map.
        /// </summary>
        /// <param name="decalPosition"></param>
        /// <param name="decalDirection"></param>
        /// <param name="decalType"></param>
        /// <returns></returns>
        public static GunDecalMessage PlaceDecal(Vector3 decalPosition, Vector3 decalDirection, DecalPoolType decalType)
        {
            GunDecalMessage gunDecalMessage = new(decalPosition, decalDirection, decalType);
            gunDecalMessage.SendToAuthenticated();
            return gunDecalMessage;
        }
    }
}
