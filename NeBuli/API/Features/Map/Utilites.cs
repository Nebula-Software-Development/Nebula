using Hazards;
using PlayerRoles.PlayableScps.Scp173;
using PlayerRoles;
using Mirror;
using RelativePositioning;
using UnityEngine;
using Object = UnityEngine.Object;
using Decals;
using InventorySystem.Items.Firearms.BasicMessages;
using Utils.Networking;
using Nebuli.API.Features.Items.Pickups;

namespace Nebuli.API.Features.Map;

public static class Utilites
{
    private static TantrumEnvironmentalHazard cachedTantrumPrefab;

    /// <summary>
    /// Gets the SCP-173 Tantrum Prefab GameObject.
    /// </summary>
    public static TantrumEnvironmentalHazard TantrumPrefab
    {
        get
        {
            if (cachedTantrumPrefab is not null)
                return cachedTantrumPrefab;
            if (PlayerRoleLoader.TryGetRoleTemplate(RoleTypeId.Scp173, out PlayerRoleBase scp173RoleBase) && scp173RoleBase is Scp173Role scp173Role)
                if (scp173Role.SubroutineModule.TryGetSubroutine(out Scp173TantrumAbility tantrumAbility)) return cachedTantrumPrefab = tantrumAbility._tantrumPrefab;
            return null;
        }
    }

    /// <summary>
    /// Places a SCP-173 tantrum.
    /// </summary>
    /// <param name="position">The positon to place it.</param>
    /// <returns></returns>
    public static TantrumEnvironmentalHazard PlaceTantrum(Vector3 position)
    {
        TantrumEnvironmentalHazard tantrum = Object.Instantiate(TantrumPrefab);
        tantrum.SynchronizedPosition = new RelativePosition(position + (Vector3.up * 0.25f));
        NetworkServer.Spawn(tantrum.gameObject);
        return tantrum;
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
