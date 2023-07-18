using System.Collections.Generic;
using System.Linq;
using CustomPlayerEffects;
using Hints;
using Mirror;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerStatsSystem;
using UnityEngine;
using VoiceChat;

namespace Nebuli.API.Features.Player;

public class NebuliPlayer
{
    public static readonly Dictionary<ReferenceHub, NebuliPlayer> Dictionary = new ();
    
    internal NebuliPlayer(ReferenceHub hub)
    {
        ReferenceHub = hub;
        GameObject = ReferenceHub.gameObject;
        Transform = ReferenceHub.transform;

        if (hub == ReferenceHub.HostHub)
            return;
        
        Create();
        
        Dictionary.Add(hub, this);
    }
    
    public static IEnumerable<NebuliPlayer> Collection => Dictionary.Values;
    
    public static List<NebuliPlayer> List => Collection.ToList();
   
    public static int PlayerCount => Dictionary.Count;
    
    public ReferenceHub ReferenceHub { get; }
    
    public GameObject GameObject { get; }
    
    public Transform Transform { get; }
    
    public string RawUserId { get; private set; }

    public bool IsBypassEnabled
    {
        get => ReferenceHub.serverRoles.BypassMode;
        set => ReferenceHub.serverRoles.BypassMode = value;
    }

    public Vector3 Position
    {
        get => Transform.position;
        set => ReferenceHub.TryOverridePosition(value, Vector3.zero);
    }

    public Vector3 Rotation
    {
        get => Transform.eulerAngles;
        set => ReferenceHub.TryOverridePosition(Position, value);
    }
    
    public int Id
    {
        get => ReferenceHub.PlayerId;
        set => ReferenceHub.Network_playerId = new RecyclablePlayerId(value);
    }
    
    public string UserId
    {
        get => ReferenceHub.characterClassManager.UserId;
        set => ReferenceHub.characterClassManager.UserId = value;
    }
    
    public string SyncedUserId
    {
        get => ReferenceHub.characterClassManager.SyncedUserId;
        set => ReferenceHub.characterClassManager.NetworkSyncedUserId = value;
    }

    public uint NetId => ReferenceHub.networkIdentity.netId;
    
    public float Health
    {
        get => ReferenceHub.playerStats.GetModule<HealthStat>().CurValue;
        set => ReferenceHub.playerStats.GetModule<HealthStat>().CurValue = value;
    }

    public float MaxHealth => ReferenceHub.playerStats.GetModule<HealthStat>().MaxValue;

    public float MinHealth => ReferenceHub.playerStats.GetModule<HealthStat>().MinValue;
    
    public float HumeShield
    {
        get => ReferenceHub.playerStats.GetModule<HumeShieldStat>().CurValue;
        set => ReferenceHub.playerStats.GetModule<HumeShieldStat>().CurValue = value;
    }
    
    public float MaxHumeShield => ReferenceHub.playerStats.GetModule<HumeShieldStat>().MaxValue;

    public float MinHumeShield => ReferenceHub.playerStats.GetModule<HumeShieldStat>().MinValue;
    
    public float HumeShieldRegeneration
    {
        get
        {
            if (ReferenceHub.playerStats.GetModule<HumeShieldStat>().TryGetHsModule(out var hsModuleBase))
                return hsModuleBase.HsRegeneration;

            return 0;
        }
    }
    
    public bool IsOverwatchEnabled
    {
        get => ReferenceHub.serverRoles.IsInOverwatch;
        set => ReferenceHub.serverRoles.IsInOverwatch = value;
    }
    
    public UserGroup Group
    {
        get => ReferenceHub.serverRoles.Group;
        set => ReferenceHub.serverRoles.SetGroup(value, false);
    }

    public string RankColor
    {
        get => ReferenceHub.serverRoles._myColor;
        set => ReferenceHub.serverRoles.SetColor(value);
    }
    
    public string RankName
    {
        get => ReferenceHub.serverRoles._myText;
        set => ReferenceHub.serverRoles.SetText(value);
    }

    public bool DoNotTrack
    {
        get => ReferenceHub.serverRoles.DoNotTrack;
        set => ReferenceHub.serverRoles.DoNotTrack = value;
    }
    
    public bool IsGodmodeEnabled
    {
        get => ReferenceHub.characterClassManager.GodMode;
        set => ReferenceHub.characterClassManager.GodMode = value;
    }
    
    public bool IsNorthWoodStaff => ReferenceHub.serverRoles.Staff;

    public bool IsGlobalModerator => ReferenceHub.serverRoles.RaEverywhere;
    
    public string CustomInfo
    {
        get => ReferenceHub.nicknameSync._customPlayerInfoString;
        set => ReferenceHub.nicknameSync.Network_customPlayerInfoString = value;
    }

    public string DisplayNickname
    {
        get => ReferenceHub.nicknameSync.DisplayName;
        set => ReferenceHub.nicknameSync.Network_displayName = value;
    }

    public string RealNickname
    {
        get => ReferenceHub.nicknameSync._myNickSync;
        set => ReferenceHub.nicknameSync._myNickSync = value;
    }

    public string Address => ReferenceHub.connectionToClient.address;
    
    public string GlobalBadge => ReferenceHub.serverRoles.GlobalBadge;

    public ulong Permissions
    {
        get => ReferenceHub.serverRoles.Permissions;
        set => ReferenceHub.serverRoles.Permissions = value;
    }
    
    public static bool TryGet(ReferenceHub hub, out NebuliPlayer player)
    {
        if (hub is not null && Dictionary.TryGetValue(hub, out NebuliPlayer value))
        {
            player = value;
            return true;
        }

        player = null;
        return false;
    }

    public static bool TryGet(GameObject go, out NebuliPlayer player)
    {
        foreach (var ply in Collection)
        {
            if (ply.GameObject != go)
                continue;

            player = ply;
            return true;
        }

        player = null;
        return false;
    }
    
    public static bool TryGet(MonoBehaviour component, out NebuliPlayer player) => TryGet(component.gameObject, out player);

    public static bool TryGet(NetworkIdentity identity, out NebuliPlayer player) => TryGet(identity.gameObject, out player);
    
    public static bool TryGet(int id, out NebuliPlayer player)
    {
        foreach (ReferenceHub hub in ReferenceHub.AllHubs)
        {
            if (hub.PlayerId == id)
                return TryGet(hub, out player);
        }

        player = null;
        return false;
    }
    
    public static bool TryGet(uint netId, out NebuliPlayer player)
    {
        if (ReferenceHub.TryGetHubNetID(netId, out ReferenceHub hub))
            return TryGet(hub, out player);
        
        player = null;
        return false;
    }
    
    public static NebuliPlayer Get(ReferenceHub hub) => TryGet(hub, out NebuliPlayer player) ? player : null;
   
    public static NebuliPlayer Get(GameObject go) => TryGet(go, out NebuliPlayer player) ? player : null;
   
    public static NebuliPlayer Get(MonoBehaviour component) => TryGet(component, out NebuliPlayer player) ? player : null;
  
    public static NebuliPlayer Get(NetworkIdentity identity) => TryGet(identity, out NebuliPlayer player) ? player : null;
   
    public static NebuliPlayer Get(int id) => TryGet(id, out NebuliPlayer player) ? player : null;
    
    public static NebuliPlayer Get(uint netId) => TryGet(netId, out NebuliPlayer player) ? player : null;
    
    public void Kill(string text) => Kill(new CustomReasonDamageHandler(text, float.MaxValue));

    public void Kill(DamageHandlerBase damageHandlerBase) => ReferenceHub.playerStats.KillPlayer(damageHandlerBase);

    public void Damage(float amount, string reason = "") => ReferenceHub.playerStats.DealDamage(new CustomReasonDamageHandler(reason, amount));
    
    public void Damage(DamageHandlerBase damageHandlerBase) => ReferenceHub.playerStats.DealDamage(damageHandlerBase);
    
    public bool TryGetEffect(string effectName, out StatusEffectBase effect) => ReferenceHub.playerEffectsController.TryGetEffect(effectName, out effect);
    
    public bool TryGetEffect<T>(out T effect) 
        where T : StatusEffectBase => ReferenceHub.playerEffectsController.TryGetEffect(out effect);
    
    public T EnableEffect<T>(float duration = 0f, bool addDuration = false) 
        where T : StatusEffectBase =>
        ReferenceHub.playerEffectsController.EnableEffect<T>(duration, addDuration);
    
    public T DisableEffect<T>() 
        where T : StatusEffectBase => ReferenceHub.playerEffectsController.DisableEffect<T>();

    public void DisableAllEffects() => ReferenceHub.playerEffectsController.DisableAllEffects();
    
    public void ShowTag(bool global = false) => ReferenceHub.characterClassManager.UserCode_CmdRequestShowTag__Boolean(global);
    
    public void HideTag() => ReferenceHub.characterClassManager.UserCode_CmdRequestHideTag();
    
    public void ShowHint(string content, int time = 5) => ShowHint(new TextHint(content, new HintParameter[] { new StringHintParameter(string.Empty) }, null, 2));
    
    public void ShowHint(Hint hint) => ReferenceHub.hints.Show(hint);
    
    public void SetRole(RoleTypeId role, RoleChangeReason reason = RoleChangeReason.RemoteAdmin, RoleSpawnFlags flags = RoleSpawnFlags.All) => ReferenceHub.roleManager.ServerSetRole(role, reason, flags);
    
    public T AddComponent<T>() 
        where T : MonoBehaviour => GameObject.AddComponent<T>();
   
    public T GetComponent<T>()
        where T : MonoBehaviour => GameObject.GetComponent<T>();
    
    public bool Kick(string reason) => BanPlayer.KickUser(ReferenceHub, reason);

    public bool Ban(string reason, long duration) => BanPlayer.BanUser(ReferenceHub, reason, duration);
    
    public void Mute(bool isIntercom = false, bool isTemporary = true)
    {
        if (isTemporary)
        {
            VoiceChatMutes.SetFlags(ReferenceHub, VcMuteFlags.GlobalRegular);
            return;
        }
        
        VoiceChatMutes.IssueLocalMute(UserId, isIntercom);
    }
    
    public void UnMute(bool removeMute = true)
    {
        if (removeMute)
        {
            VoiceChatMutes.RevokeLocalMute(UserId);
            return;
        }
        
        VoiceChatMutes.SetFlags(ReferenceHub, VcMuteFlags.None);
    }
    
    public void SendHitMarker(float size = 2.55f) => Hitmarker.SendHitmarker(ReferenceHub, size);
    
    public bool HasPermission(PlayerPermissions permission) => PermissionsHandler.IsPermitted(Permissions, permission);
    
    public bool HasPermissions(PlayerPermissions[] permissions) => PermissionsHandler.IsPermitted(Permissions, permissions);

    private void Create()
    {
        int index = UserId.LastIndexOf('@');

        if (index == -1)
        {
            RawUserId = UserId;
            return;
        }
        
        RawUserId = UserId.Substring(0, index);
    }
}