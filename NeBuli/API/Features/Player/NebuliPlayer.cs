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

    /// <summary>
    /// Gets or sets whether or not the player has bypass or not.
    /// </summary>
    public bool IsBypassEnabled
    {
        get => ReferenceHub.serverRoles.BypassMode;
        set => ReferenceHub.serverRoles.BypassMode = value;
    }

    /// <summary>
    /// Gets or sets the players current position.
    /// </summary>
    public Vector3 Position
    {
        get => Transform.position;
        set => ReferenceHub.TryOverridePosition(value, Vector3.zero);
    }

    /// <summary>
    /// Gets or sets the players current rotation.
    /// </summary>
    public Vector3 Rotation
    {
        get => Transform.eulerAngles;
        set => ReferenceHub.TryOverridePosition(Position, value);
    }
    
    /// <summary>
    /// Gets or sets the players current <see cref="RecyclablePlayerId"/>
    /// </summary>
    public int Id
    {
        get => ReferenceHub.PlayerId;
        set => ReferenceHub.Network_playerId = new RecyclablePlayerId(value);
    }
    
    /// <summary>
    /// Gets or sets the players current UserId.
    /// </summary>
    public string UserId
    {
        get => ReferenceHub.characterClassManager.UserId;
        set => ReferenceHub.characterClassManager.UserId = value;
    }
    
    /// <summary>
    /// Gets or sets the players current SyncedUserId.
    /// </summary>
    public string SyncedUserId
    {
        get => ReferenceHub.characterClassManager.SyncedUserId;
        set => ReferenceHub.characterClassManager.NetworkSyncedUserId = value;
    }

    /// <summary>
    /// Gets the players NetId.
    /// </summary>
    public uint NetId => ReferenceHub.networkIdentity.netId;
    
    /// <summary>
    /// Gets or sets the players health.
    /// </summary>
    public float Health
    {
        get => ReferenceHub.playerStats.GetModule<HealthStat>().CurValue;
        set => ReferenceHub.playerStats.GetModule<HealthStat>().CurValue = value;
    }

    /// <summary>
    /// Gets the players maximum possible health value.
    /// </summary>
    public float MaxHealth => ReferenceHub.playerStats.GetModule<HealthStat>().MaxValue;

    /// <summary>
    /// Gets the players minimum possible health value.
    /// </summary>
    public float MinHealth => ReferenceHub.playerStats.GetModule<HealthStat>().MinValue;
    
    /// <summary>
    /// Gets or sets the players current HumeShield value.
    /// </summary>
    public float HumeShield
    {
        get => ReferenceHub.playerStats.GetModule<HumeShieldStat>().CurValue;
        set => ReferenceHub.playerStats.GetModule<HumeShieldStat>().CurValue = value;
    }
    
    /// <summary>
    /// Gets the players maximum HumeShield value.
    /// </summary>
    public float MaxHumeShield => ReferenceHub.playerStats.GetModule<HumeShieldStat>().MaxValue;

    /// <summary>
    /// Gets the players minimum HumeShield value.
    /// </summary>
    public float MinHumeShield => ReferenceHub.playerStats.GetModule<HumeShieldStat>().MinValue;
    
    /// <summary>
    /// Gets the players HumeShieldRegeneration value.
    /// </summary>
    public float HumeShieldRegeneration
    {
        get
        {
            if (ReferenceHub.playerStats.GetModule<HumeShieldStat>().TryGetHsModule(out var hsModuleBase))
                return hsModuleBase.HsRegeneration;

            return 0;
        }
    }
    
    /// <summary>
    /// Gets or sets if the player is in Overwatch.
    /// </summary>
    public bool IsOverwatchEnabled
    {
        get => ReferenceHub.serverRoles.IsInOverwatch;
        set => ReferenceHub.serverRoles.IsInOverwatch = value;
    }
    
    /// <summary>
    /// Gets or sets the players usergroups.
    /// </summary>
    public UserGroup Group
    {
        get => ReferenceHub.serverRoles.Group;
        set => ReferenceHub.serverRoles.SetGroup(value, false);
    }

    /// <summary>
    /// Gets or sets the players rank color.
    /// </summary>
    public string RankColor
    {
        get => ReferenceHub.serverRoles._myColor;
        set => ReferenceHub.serverRoles.SetColor(value);
    }
    
    /// <summary>
    /// Gets or sets the players rank name.
    /// </summary>
    public string RankName
    {
        get => ReferenceHub.serverRoles._myText;
        set => ReferenceHub.serverRoles.SetText(value);
    }

    /// <summary>
    /// Gets or sets whether the player has DoNotTrack enabled or not.
    /// </summary>
    public bool DoNotTrack
    {
        get => ReferenceHub.serverRoles.DoNotTrack;
        set => ReferenceHub.serverRoles.DoNotTrack = value;
    }
    
    /// <summary>
    /// Gets or sets a value if god mode is enabled or not.
    /// </summary>
    public bool IsGodmodeEnabled
    {
        get => ReferenceHub.characterClassManager.GodMode;
        set => ReferenceHub.characterClassManager.GodMode = value;
    }
    
    /// <summary>
    /// Gets a boolean determining if the player is a Northwood Studios staff member.
    /// </summary>
    public bool IsNorthWoodStaff => ReferenceHub.serverRoles.Staff;

    /// <summary>
    /// Gets a boolean determining if the player is a Global Moderator.
    /// </summary>
    public bool IsGlobalModerator => ReferenceHub.serverRoles.RaEverywhere;
    
    /// <summary>
    /// Gets or sets the players custom info string.
    /// </summary>
    public string CustomInfo
    {
        get => ReferenceHub.nicknameSync._customPlayerInfoString;
        set => ReferenceHub.nicknameSync.Network_customPlayerInfoString = value;
    }

    /// <summary>
    /// Gets or sets the players display nickname.
    /// </summary>
    public string DisplayNickname
    {
        get => ReferenceHub.nicknameSync.DisplayName;
        set => ReferenceHub.nicknameSync.Network_displayName = value;
    }

    /// <summary>
    /// Gets or sets the players real nickname.
    /// </summary>
    public string RealNickname
    {
        get => ReferenceHub.nicknameSync._myNickSync;
        set => ReferenceHub.nicknameSync._myNickSync = value;
    }

    /// <summary>
    /// Gets the players IP adress.
    /// </summary>
    public string Address => ReferenceHub.connectionToClient.address;
    
    /// <summary>
    /// Gets the players global badge.
    /// </summary>
    public string GlobalBadge => ReferenceHub.serverRoles.GlobalBadge;

    /// <summary>
    /// Gets or sets the players permissions.
    /// </summary>
    public ulong Permissions
    {
        get => ReferenceHub.serverRoles.Permissions;
        set => ReferenceHub.serverRoles.Permissions = value;
    }
    
    /// <summary>
    /// Trys to get a <see cref="NebuliPlayer"/> with the provided Referencehub.
    /// </summary>
    /// <param name="hub">The players referencehub.</param>
    /// <param name="player">The player that will be returned, if found.</param>
    /// <returns>The <see cref="NebuliPlayer"/>, otherwise null.</returns>
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

    /// <summary>
    /// Trys to get a <see cref="NebuliPlayer"/> with the provided GameObject.
    /// </summary>
    /// <param name="gameobject">The players GameObject.</param>
    /// <param name="player">The player that will be returned, if found.</param>
    /// <returns>The <see cref="NebuliPlayer"/>, otherwise null.</returns>
    public static bool TryGet(GameObject gameobject, out NebuliPlayer player)
    {
        foreach (var ply in Collection)
        {
            if (ply.GameObject != gameobject)
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