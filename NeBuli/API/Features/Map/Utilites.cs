using Hazards;
using PlayerRoles.PlayableScps.Scp173;
using PlayerRoles;
using Mirror;
using RelativePositioning;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Nebuli.API.Features.Map
{
    public static class Utilites
    {
        private static TantrumEnvironmentalHazard tantrumPrefab;

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

    }
}
