// -----------------------------------------------------------------------
// <copyright file=Player.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Achievements;
using CommandSystem;
using CustomPlayerEffects;
using Footprinting;
using GameCore;
using Hints;
using Interactables.Interobjects.DoorUtils;
using InventorySystem;
using InventorySystem.Disarming;
using InventorySystem.Items;
using MapGeneration;
using Mirror;
using Nebula.API.Extensions;
using Nebula.API.Features.Enum;
using Nebula.API.Features.Items;
using Nebula.API.Features.Map;
using Nebula.API.Features.Pools;
using Nebula.API.Features.Roles;
using Nebula.API.Features.Structs;
using Nebula.API.Internal;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.RoleAssign;
using PlayerRoles.Voice;
using PlayerStatsSystem;
using RelativePositioning;
using RemoteAdmin;
using UnityEngine;
using Utils;
using Utils.Networking;
using VoiceChat;
using static Broadcast;

namespace Nebula.API.Features
{
    /// <summary>
    ///     Represents a player in the Nebula framework.
    /// </summary>
    public class Player
    {
        private static int _healthStatIndex = -1;

        private readonly CustomHealthManager _customHealthManager;

        private Role _currentRole;

        internal Player(ReferenceHub hub)
        {
            ReferenceHub = hub;

            Create();

            if (ReferenceHub == ReferenceHub.HostHub)
            {
                return;
            }

            if (_healthStatIndex == -1)
            {
                _healthStatIndex = Array.FindIndex(ReferenceHub.playerStats.StatModules,
                    module => module.GetType() == typeof(HealthStat));
            }

            ReferenceHub.playerStats._dictionarizedTypes[typeof(HealthStat)] =
                ReferenceHub.playerStats.StatModules[_healthStatIndex] =
                    _customHealthManager = new CustomHealthManager { Hub = ReferenceHub };

            Dictionary.AddIfMissing(hub, this);
        }

        internal Player(GameObject gameObject)
        {
            ReferenceHub hub = ReferenceHub.GetHub(gameObject);
            if (hub is null)
            {
                Log.Error(gameObject.name +
                          "does not have a ReferenceHub attached to it and therefor a Player cannot be made!");
                return;
            }

            new Player(hub);
        }

        /// <summary>
        ///     Gets the dictionary that maps ReferenceHub to Player instances.
        /// </summary>
        public static Dictionary<ReferenceHub, Player> Dictionary { get; internal set; } = new(Server.MaxPlayerCount);

        /// <summary>
        ///     Gets a collection of <see cref="Player" /> instances.
        /// </summary>
        public static IEnumerable<Player> Collection => Dictionary.Values;

        /// <summary>
        ///     Gets a list of all the player's on the server.
        /// </summary>
        public static List<Player> List => Collection.ToList();

        /// <summary>
        ///     Gets a list of all online staff. This is determined by if the have Remote Admin access or not.
        /// </summary>
        public static List<Player> OnlineStaff => List.Where(ply => ply.ReferenceHub.serverRoles.RemoteAdmin).ToList();

        /// <summary>
        ///     Gets the players <see cref="API.Internal.UserSessionData" />.
        /// </summary>
        public UserSessionData UserSessionData { get; } = new();

        /// <summary>
        ///     The player count of the server.
        /// </summary>
        public static int PlayerCount => Server.PlayerCount;

        /// <summary>
        ///     The player's ReferenceHub.
        /// </summary>
        public ReferenceHub ReferenceHub { get; }

        /// <summary>
        ///     Gets or sets if the player is a NPC.
        /// </summary>
        public bool IsNpc { get; set; } = false;

        /// <summary>
        ///     Gets the players <see cref="Mirror.NetworkIdentity" />.
        /// </summary>
        public NetworkIdentity NetworkIdentity => ReferenceHub.netIdentity;

        /// <summary>
        ///     Gets or sets the players current ammo.
        /// </summary>
        public Dictionary<ItemType, ushort> Ammo
        {
            get => Inventory.UserInventory.ReserveAmmo;
            set => Inventory.UserInventory.ReserveAmmo = value;
        }

        /// <summary>
        ///     The player's GameObject.
        /// </summary>
        public GameObject GameObject => ReferenceHub.gameObject;

        /// <summary>
        ///     The player' Transform.
        /// </summary>
        public Transform Transform => ReferenceHub.transform;

        /// <summary>
        ///     Gets if the player is cuffed.
        /// </summary>
        public bool IsCuffed => Inventory.IsDisarmed();

        /// <summary>
        ///     Gets or sets the players <see cref="global::PlayerInfoArea" />.
        /// </summary>
        public PlayerInfoArea PlayerInfoArea
        {
            get => ReferenceHub.nicknameSync.Network_playerInfoToShow;
            set => ReferenceHub.nicknameSync.Network_playerInfoToShow = value;
        }

        /// <summary>
        ///     Gets or sets a disarmer of the player, or null if none.
        /// </summary>
        public Player Disarmer
        {
            get => Get(DisarmedPlayers.Entries.FirstOrDefault(e => e.DisarmedPlayer == NetId).Disarmer);
            set
            {
                DisarmedPlayers.DisarmedEntry entryToRemove =
                    DisarmedPlayers.Entries.FirstOrDefault(e => e.DisarmedPlayer == Inventory.netId);

                if (DisarmedPlayers.Entries.Contains(entryToRemove))
                {
                    DisarmedPlayers.Entries.Remove(entryToRemove);
                }

                if (value is null)
                {
                    return;
                }

                Inventory.SetDisarmedStatus(value.Inventory);
                new DisarmedPlayersListMessage(DisarmedPlayers.Entries).SendToAuthenticated();
            }
        }

        /// <summary>
        ///     Gets the players <see cref="VoiceModuleBase" />, or null if the current role isnt a <see cref="IVoiceRole" />.
        /// </summary>
        public VoiceModuleBase VoiceModule =>
            RoleManager.CurrentRole is IVoiceRole voiceRole ? voiceRole.VoiceModule : null;

        /// <summary>
        ///     The players RawUserId, or the <see cref="UserId" /> without the '@'.
        /// </summary>
        public string RawUserId { get; private set; }

        /// <summary>
        ///     Gets a <see cref="Player" /> footprint.
        /// </summary>
        public Footprint Footprint => new(ReferenceHub);

        /// <summary>
        ///     Gets the <see cref="Player" /> <see cref="PlayerCommandSender" />.
        /// </summary>
        public PlayerCommandSender Sender => ReferenceHub.queryProcessor._sender;

        /// <summary>
        ///     Gets the servers connection to the client.
        /// </summary>
        public NetworkConnection NetworkConnection => ReferenceHub.connectionToClient;

        /// <summary>
        ///     Gets the players current <see cref="RoleTypeId" />.
        /// </summary>
        public RoleTypeId RoleType => ReferenceHub.GetRoleId();

        /// <summary>
        ///     Gets the players current faction
        /// </summary>
        public Faction Faction => RoleType.GetFaction();

        /// <summary>
        ///     Gets the players <see cref="PlayerRoleManager" />.
        /// </summary>
        public PlayerRoleManager RoleManager => ReferenceHub.roleManager;

        /// <summary>
        ///     Gets the players current <see cref="Roles.Role" />.
        /// </summary>
        public Role Role
        {
            get
            {
                if (_currentRole is null || _currentRole.RoleTypeId != RoleManager.CurrentRole.RoleTypeId)
                {
                    _currentRole = Role.CreateNew(RoleManager.CurrentRole);
                }

                return _currentRole;
            }
            set => _currentRole = value;
        }

        /// <summary>
        ///     Gets or sets whether or not the player has bypass or not.
        /// </summary>
        public bool IsBypassEnabled
        {
            get => ReferenceHub.serverRoles.BypassMode;
            set => ReferenceHub.serverRoles.BypassMode = value;
        }

        /// <summary>
        ///     Gets or sets if the players badge is hidden or not.
        /// </summary>
        public bool IsBadgeHidden
        {
            get => !string.IsNullOrEmpty(ReferenceHub.serverRoles.HiddenBadge);
            set
            {
                if (value)
                {
                    HideTag();
                }
                else
                {
                    ShowTag();
                }
            }
        }

        /// <summary>
        ///     Gets or sets if the player is muted.
        /// </summary>
        public bool IsMuted
        {
            get => VoiceChatMutes.IsMuted(ReferenceHub);
            set
            {
                if (value)
                {
                    VoiceChatMutes.IssueLocalMute(UserId);
                }
                else
                {
                    VoiceChatMutes.RevokeLocalMute(UserId);
                }
            }
        }

        /// <summary>
        ///     Gets or sets if the player should ignore base-game friendly fire rules.
        /// </summary>
        public bool IgnoreFFRules { get; set; } = false;

        /// <summary>
        ///     Gets or sets the players <see cref="ScpSpawnPreferences.SpawnPreferences" />.
        /// </summary>
        public ScpSpawnPreferences.SpawnPreferences ScpSpawnPreferences
        {
            get
            {
                if (PlayerRoles.RoleAssign.ScpSpawnPreferences.Preferences.TryGetValue(NetworkConnection.connectionId,
                        out ScpSpawnPreferences.SpawnPreferences value))
                {
                    return value;
                }

                return default;
            }
            set => PlayerRoles.RoleAssign.ScpSpawnPreferences.Preferences[NetworkConnection.connectionId] = value;
        }

        /// <summary>
        ///     Gets or sets the players current position.
        /// </summary>
        public Vector3 Position
        {
            get => Transform.position;
            set => ReferenceHub.TryOverridePosition(value, Vector3.zero);
        }

        /// <summary>
        ///     Gets the players camera transform.
        /// </summary>
        public Transform PlayerCamera => ReferenceHub.PlayerCameraReference;

        /// <summary>
        ///     Gets or sets the players <see cref="RelativePositioning.RelativePosition" />.
        /// </summary>
        public RelativePosition RelativePosition
        {
            get => new(Position);
            set => Position = value.Position;
        }

        /// <summary>
        ///     Gets or sets the players current rotation.
        /// </summary>
        public Vector3 Rotation
        {
            get => Transform.eulerAngles;
            set => LookAtDirection(value);
        }

        /// <summary>
        ///     Gets or sets the players current scale.
        /// </summary>
        public Vector3 Scale
        {
            get => ReferenceHub.transform.localScale;
            set
            {
                if (value == ReferenceHub.transform.localScale)
                {
                    return;
                }

                try
                {
                    ReferenceHub.transform.localScale = value;
                    foreach (Player player in List)
                    {
                        Server.SendSpawnMessage?.Invoke(null,
                            new object[] { NetworkIdentity, player.NetworkConnection });
                    }
                }
                catch (Exception exception)
                {
                    Log.Error("Error setting player scale! Full error -->\n" + exception);
                }
            }
        }

        /// <summary>
        ///     Gets or sets if the player has a reserved slot.
        /// </summary>
        public bool HasReservedSlot
        {
            get => ReservedSlot.HasReservedSlot(UserId, out _);
            set
            {
                string path = ConfigSharing.Paths[3] + "UserIDReservedSlots.txt";

                if (value)
                {
                    //Taken from NW's reload method.
                    ReservedSlot.Reload();
                    List<string> lines = ListPool<string>.Instance.Get();
                    using (StreamReader streamReader = new(path))
                    {
                        while (streamReader.ReadLine() is { } text)
                        {
                            if (!string.IsNullOrWhiteSpace(text) &&
                                !text.TrimStart().StartsWith("#", StringComparison.Ordinal) && text.Contains("@"))
                            {
                                lines.Add(text.Trim());
                            }
                        }
                    }

                    lines.Add(UserId);
                    File.WriteAllLines(path, lines);
                    ReservedSlot.Users.Add(UserId);
                    ListPool<string>.Instance.Return(lines);
                }
                else
                {
                    ReservedSlot.Reload();
                    List<string> lines = ListPool<string>.Instance.Get();
                    using (StreamReader streamReader = new(path))
                    {
                        while (streamReader.ReadLine() is { } text)
                        {
                            if (!string.IsNullOrWhiteSpace(text) &&
                                !text.TrimStart().StartsWith("#", StringComparison.Ordinal) && text.Contains("@") &&
                                !text.Trim().Equals(UserId, StringComparison.OrdinalIgnoreCase))
                            {
                                lines.Add(text.Trim());
                            }
                        }
                    }

                    File.WriteAllLines(path, lines);
                    ListPool<string>.Instance.Return(lines);
                }
            }
        }

        /// <summary>
        ///     Gets or sets the players current <see cref="RecyclablePlayerId" />
        /// </summary>
        public int Id
        {
            get => ReferenceHub.PlayerId;
            set
            {
                if (RecyclablePlayerId.FreeIds.Contains(value))
                {
                    ReferenceHub.Network_playerId = new RecyclablePlayerId(value);
                }
                else
                {
                    Log.Warning(
                        $"{Assembly.GetCallingAssembly().GetName().Name} tried to set a PlayerId to a ID that was already taken!");
                }
            }
        }

        /// <summary>
        ///     Gets or sets if the player has noclip.
        /// </summary>
        public bool HasNoClip
        {
            get => ReferenceHub.playerStats.GetModule<AdminFlagsStat>().HasFlag(AdminFlags.Noclip);
            set => ReferenceHub.playerStats.GetModule<AdminFlagsStat>().SetFlag(AdminFlags.Noclip, value);
        }

        /// <summary>
        ///     Gets or sets the players current UserId.
        /// </summary>
        public string UserId
        {
            get => ReferenceHub.authManager.UserId;
            set => ReferenceHub.authManager.UserId = value;
        }

        /// <summary>
        ///     Gets or sets the players current SyncedUserId.
        /// </summary>
        public string SyncedUserId
        {
            get => ReferenceHub.authManager.SyncedUserId;
            set => ReferenceHub.authManager.NetworkSyncedUserId = value;
        }

        /// <summary>
        ///     Gets the players NetId.
        /// </summary>
        public uint NetId => ReferenceHub.networkIdentity.netId;

        /// <summary>
        ///     Gets or sets the players health.
        /// </summary>
        public float Health
        {
            get => ReferenceHub.playerStats.GetModule<HealthStat>().CurValue;
            set => ReferenceHub.playerStats.GetModule<HealthStat>().CurValue = value;
        }

        /// <summary>
        ///     Gets or sets the players maximum possible health value.
        /// </summary>
        public float MaxHealth
        {
            get => _customHealthManager.MaxValue;
            set => _customHealthManager.MaxHealth = value;
        }

        /// <summary>
        ///     Gets or sets the players minimum possible health value.
        /// </summary>
        public float MinHealth
        {
            get => _customHealthManager.MinValue;
            set => _customHealthManager.MinHealth = value;
        }

        /// <summary>
        ///     Gets or sets the players current HumeShield value.
        /// </summary>
        public float HumeShield
        {
            get => ReferenceHub.playerStats.GetModule<HumeShieldStat>().CurValue;
            set => ReferenceHub.playerStats.GetModule<HumeShieldStat>().CurValue = value;
        }

        /// <summary>
        ///     Gets the players maximum HumeShield value.
        /// </summary>
        public float MaxHumeShield => ReferenceHub.playerStats.GetModule<HumeShieldStat>().MaxValue;

        /// <summary>
        ///     Gets the players minimum HumeShield value.
        /// </summary>
        public float MinHumeShield => ReferenceHub.playerStats.GetModule<HumeShieldStat>().MinValue;

        /// <summary>
        ///     Gets the players HumeShieldRegeneration value.
        /// </summary>
        public float HumeShieldRegeneration
        {
            get
            {
                if (ReferenceHub.playerStats.GetModule<HumeShieldStat>()
                    .TryGetHsModule(out HumeShieldModuleBase hsModuleBase))
                {
                    return hsModuleBase.HsRegeneration;
                }

                return 0;
            }
        }

        /// <summary>
        ///     Gets or sets if the player is in Overwatch.
        /// </summary>
        public bool IsOverwatchEnabled
        {
            get => ReferenceHub.serverRoles.IsInOverwatch;
            set => ReferenceHub.serverRoles.IsInOverwatch = value;
        }

        /// <summary>
        ///     Gets or sets the players <see cref="UserGroup" />.
        /// </summary>
        public UserGroup Group
        {
            get => ReferenceHub.serverRoles.Group;
            set => ReferenceHub.serverRoles.SetGroup(value);
        }

        /// <summary>
        ///     Gets or sets the players <see cref="UserGroup" /> name.
        /// </summary>
        public string GroupName
        {
            get => ServerStatic.PermissionsHandler._members.TryGetValue(UserId, out string name) ? name : null;
            set => ServerStatic.PermissionsHandler._members[UserId] = value;
        }

        /// <summary>
        ///     Gets or sets the players rank color.
        /// </summary>
        public string RankColor
        {
            get => ReferenceHub.serverRoles._myColor;
            set => ReferenceHub.serverRoles.SetColor(value);
        }

        /// <summary>
        ///     Gets or sets the players rank color using <see cref="Features.Enum.RankColorType" />.
        /// </summary>
        public RankColorType RankColorType
        {
            get => ReferenceHub.serverRoles._myColor.ToRankColorType();
            set => ReferenceHub.serverRoles.SetColor(value.ToColorString());
        }

        /// <summary>
        ///     Gets or sets the players rank name.
        /// </summary>
        public string RankName
        {
            get => ReferenceHub.serverRoles._myText;
            set => ReferenceHub.serverRoles.SetText(value);
        }

        /// <summary>
        ///     Gets the players current velocity.
        /// </summary>
        public Vector3 Velocity => ReferenceHub.GetVelocity();

        /// <summary>
        ///     Gets whether the player has DoNotTrack enabled or not.
        /// </summary>
        public bool DoNotTrack => ReferenceHub.authManager.DoNotTrack;

        /// <summary>
        ///     Gets or sets a value if god mode is enabled or not.
        /// </summary>
        public bool IsGodmodeEnabled
        {
            get => ReferenceHub.characterClassManager.GodMode;
            set => ReferenceHub.characterClassManager.GodMode = value;
        }

        /// <summary>
        ///     Gets a boolean determining if the player is a Northwood Studios staff member.
        /// </summary>
        public bool IsNorthWoodStaff => ReferenceHub.authManager.NorthwoodStaff;

        /// <summary>
        ///     Gets a boolean determining if the player is a Global Moderator.
        /// </summary>
        public bool IsGlobalModerator => ReferenceHub.authManager.RemoteAdminGlobalAccess;

        /// <summary>
        ///     Gets or sets the players custom info string.
        /// </summary>
        public string CustomInfo
        {
            get => ReferenceHub.nicknameSync._customPlayerInfoString;
            set => ReferenceHub.nicknameSync.Network_customPlayerInfoString = value;
        }

        /// <summary>
        ///     Gets or sets the players display nickname.
        /// </summary>
        public string DisplayNickname
        {
            get => ReferenceHub.nicknameSync.DisplayName;
            set => ReferenceHub.nicknameSync.Network_displayName = value;
        }

        /// <summary>
        ///     Gets or sets the players nickname.
        /// </summary>
        public string Nickname
        {
            get => ReferenceHub.nicknameSync.Network_myNickSync;
            set => ReferenceHub.nicknameSync.Network_myNickSync = value;
        }

        /// <summary>
        ///     Gets if the player has a custom name.
        /// </summary>
        public bool HasCustomName => ReferenceHub.nicknameSync.HasCustomName;

        /// <summary>
        ///     Gets the players IP address.
        /// </summary>
        public string Address => ReferenceHub.connectionToClient.address;

        /// <summary>
        ///     Gets the players global badge.
        /// </summary>
        public string GlobalBadge => ReferenceHub.serverRoles.GlobalBadge;

        /// <summary>
        ///     Gets or sets the players permissions.
        /// </summary>
        public ulong Permissions
        {
            get => ReferenceHub.serverRoles.Permissions;
            set => ReferenceHub.serverRoles.Permissions = value;
        }

        /// <summary>
        ///     Gets the players <see cref="Features.Enum.AuthenticationType" />.
        /// </summary>
        public AuthenticationType AuthenticationType
        {
            get
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    return AuthenticationType.Unknown;
                }

                return UserId.Substring(UserId.LastIndexOf('@') + 1) switch
                {
                    "steam" => AuthenticationType.Steam,
                    "discord" => AuthenticationType.Discord,
                    "northwood" => AuthenticationType.Northwood,
                    "localhost" => AuthenticationType.LocalHost,
                    "ID_Dedicated" => AuthenticationType.DedicatedServer,
                    _ => AuthenticationType.Unknown
                };
            }
        }

        /// <summary>
        ///     Gets all the players current <see cref="StatusEffectBase" />.
        /// </summary>
        public IEnumerable<StatusEffectBase> ActiveEffects =>
            ReferenceHub.playerEffectsController.AllEffects.Where(e => e.Intensity > 0);

        /// <summary>
        ///     Gets the players firearm preferences.
        /// </summary>
        public Dictionary<FirearmType, AttachmentIdentity[]> Preferences
        {
            get
            {
                if (Firearm.PlayerPreferences.TryGetValue(this,
                        out Dictionary<FirearmType, AttachmentIdentity[]> prefs))
                {
                    return prefs;
                }

                return new Dictionary<FirearmType, AttachmentIdentity[]>();
            }
        }

        /// <summary>
        ///     Gets the players current room.
        /// </summary>
        public Room CurrentRoom => Room.Get(Position);

        /// <summary>
        ///     Gets the players current zone.
        /// </summary>
        public FacilityZone CurrentZone => Room.Get(Position).Zone;

        /// <summary>
        ///     Gets the players <see cref="PlayerRoles.Team" />.
        /// </summary>
        public Team Team => Role?.Team ?? Team.Dead;

        /// <summary>
        ///     Gets if the player is alive or not.
        /// </summary>
        public bool IsAlive => !IsDead;

        /// <summary>
        ///     Gets if the player is dead or not.
        /// </summary>
        public bool IsDead => Role?.IsDead ?? false;

        /// <summary>
        ///     Gets or sets the current item held by the player.
        /// </summary>
        public Item CurrentItem
        {
            get => Item.Get(Inventory.CurInstance);
            set => Inventory.ServerSelectItem(value.Serial);
        }

        /// <summary>
        ///     Gets a list of all the players items.
        /// </summary>
        public List<Item> Items => Inventory.UserInventory.Items.Values
            .Where(itemBase => itemBase != null)
            .Select(Item.Get)
            .ToList();

        /// <summary>
        ///     Gets the players <see cref="InventorySystem.Inventory" />.
        /// </summary>
        public Inventory Inventory => ReferenceHub.inventory;

        /// <summary>
        ///     Gets if the player has a full inventory.
        /// </summary>
        public bool HasFullInventory => Inventory.UserInventory.Items.Count >= Inventory.MaxSlots;

        /// <summary>
        ///     Gets if the player has a empty inventory.
        /// </summary>
        public bool EmptyInventory => !Inventory.UserInventory.Items.Any();

        /// <summary>
        ///     Gets or sets if the player has noclip permissions.
        /// </summary>
        public bool HasNoClipPermissions
        {
            get => FpcNoclip.IsPermitted(ReferenceHub);
            set
            {
                if (value)
                {
                    FpcNoclip.PermitPlayer(ReferenceHub);
                }
                else
                {
                    FpcNoclip.UnpermitPlayer(ReferenceHub);
                }
            }
        }

        ~Player()
        {
        }

        /// <summary>
        ///     Gives the player the specified <see cref="AchievementName" />.
        /// </summary>
        /// <param name="achievement">The <see cref="AchievementName" /> to give.</param>
        public void GiveAchievement(AchievementName achievement)
        {
            AchievementHandlerBase.ServerAchieve(NetworkConnection, achievement);
        }

        /// <summary>
        ///     Disarms the player.
        /// </summary>
        /// <param name="disarmer">The player disarming.</param>
        public void Disarm(Player disarmer)
        {
            Inventory.SetDisarmedStatus(disarmer.Inventory);
        }

        /// <summary>
        ///     Disconnects the player from the server.
        /// </summary>
        public void Disconnect(string reason = null)
        {
            ServerConsole.Disconnect(NetworkConnection, reason ?? string.Empty);
        }

        /// <summary>
        ///     Forces the <see cref="Player" /> to look at the position of the <see cref="Vector3" />.
        /// </summary>
        /// <param name="position">The <see cref="Vector3" /> position to look at.</param>
        /// <param name="lerp">The lerping speed of the camera.</param>
        /// <returns>True if the players role is a <see cref="FpcRoleBase" />, otherwise false.</returns>
        public bool LookAtPosition(Vector3 position, float lerp = 1)
        {
            if (Role is not FpcRoleBase fpc)
            {
                return false;
            }

            fpc.LookAtPoint(position, lerp);
            return true;

        }

        /// <summary>
        ///     Forces the <see cref="Player" /> to look at the direction of the <see cref="Vector3" />.
        /// </summary>
        /// <param name="direction">The <see cref="Vector3" /> direction to look at.</param>
        /// <param name="lerp">The lerping speed of the camera.</param>
        /// <returns>True if the players role is a <see cref="FpcRoleBase" />, otherwise false.</returns>
        public bool LookAtDirection(Vector3 direction, float lerp = 1)
        {
            if (Role is not FpcRoleBase fpc)
            {
                return false;
            }

            fpc.LookAtDirection(direction, lerp);
            return true;
        }

        /// <summary>
        ///     Trys to get a <see cref="Player" /> with the provided Referencehub.
        /// </summary>
        /// <param name="hub">The players referencehub.</param>
        /// <param name="player">The player that will be returned, if found.</param>
        /// <returns>True if found, otherwise false.</returns>
        public static bool TryGet(ReferenceHub hub, out Player player)
        {
            if (hub is not null && Dictionary.TryGetValue(hub, out Player value))
            {
                player = value;
                return true;
            }

            player = null;
            return false;
        }

        /// <summary>
        ///     Trys to get a <see cref="Player" /> with the provided GameObject.
        /// </summary>
        /// <param name="gameobject">The players GameObject.</param>
        /// <param name="player">The player that will be returned, if found.</param>
        /// <returns>True if found, otherwise false.</returns>
        public static bool TryGet(GameObject gameobject, out Player player)
        {
            foreach (Player ply in Collection)
            {
                if (ply.GameObject != gameobject)
                {
                    continue;
                }

                player = ply;
                return true;
            }

            player = null;
            return false;
        }

        /// <summary>
        ///     Trys to get a <see cref="Player" /> with the provided MonoBehavior component.
        /// </summary>
        /// <param name="component">The players Monobehavior component.</param>
        /// <param name="player">The player that will be returned, if found.</param>
        /// <returns>True if found, otherwise false.</returns>
        public static bool TryGet(MonoBehaviour component, out Player player)
        {
            return TryGet(component.gameObject, out player);
        }

        /// <summary>
        ///     Trys to get a <see cref="Player" /> with the provided <see cref="NetworkIdentity" /> component.
        /// </summary>
        /// <param name="identity">The players <see cref="NetworkIdentity" /> component.</param>
        /// <param name="player">The player that will be returned, if found.</param>
        /// <returns>True if found, otherwise false.</returns>
        public static bool TryGet(NetworkIdentity identity, out Player player)
        {
            return TryGet(identity.gameObject, out player);
        }

        /// <summary>
        ///     Tries to get a Player instance based on their PlayerId.
        /// </summary>
        /// <param name="id">The PlayerId of the player.</param>
        /// <param name="player">When this method returns, contains the Player instance if found; otherwise, null.</param>
        /// <returns>True if the Player instance was found; otherwise, false.</returns>
        public static bool TryGet(int id, out Player player)
        {
            foreach (ReferenceHub hub in ReferenceHub.AllHubs.Where(hub => hub.PlayerId == id))
            {
                return TryGet(hub, out player);
            }

            player = null;
            return false;
        }

        /// <summary>
        ///     Tries to get a Player instance based on their NetworkId.
        /// </summary>
        /// <param name="netId">The NetworkId of the player.</param>
        /// <param name="player">When this method returns, contains the Player instance if found; otherwise, null.</param>
        /// <returns>True if the Player instance was found; otherwise, false.</returns>
        public static bool TryGet(uint netId, out Player player)
        {
            if (ReferenceHub.TryGetHubNetID(netId, out ReferenceHub hub))
            {
                return TryGet(hub, out player);
            }

            player = null;
            return false;
        }


        /// <summary>
        ///     Tries to get a Player instance based on their <see cref="ICommandSender" />.
        /// </summary>
        /// <param name="sender">The <see cref="ICommandSender" /> of the player.</param>
        /// <param name="player">When this method returns, contains the Player instance if found; otherwise, null.</param>
        /// <returns>True if the Player instance was found; otherwise, false.</returns>
        public static bool TryGet(ICommandSender sender, out Player player)
        {
            foreach (Player ply in Collection)
            {
                if (ply.Sender != sender)
                {
                    continue;
                }

                player = ply;
                return true;
            }

            player = null;
            return false;
        }

        /// <summary>
        ///     Gets a Player instance based on a ReferenceHub.
        /// </summary>
        /// <param name="hub">The ReferenceHub of the player.</param>
        /// <returns>The Player instance if found; otherwise, null.</returns>
        public static Player Get(ReferenceHub hub)
        {
            return TryGet(hub, out Player player) ? player : null;
        }

        /// <summary>
        ///     Gets a Player instance based on a GameObject.
        /// </summary>
        /// <param name="go">The GameObject of the player.</param>
        /// <returns>The Player instance if found; otherwise, null.</returns>
        public static Player Get(GameObject go)
        {
            return TryGet(go, out Player player) ? player : null;
        }

        /// <summary>
        ///     Gets a Player instance based on a MonoBehaviour component.
        /// </summary>
        /// <param name="component">The MonoBehaviour component of the player.</param>
        /// <returns>The Player instance if found; otherwise, null.</returns>
        public static Player Get(MonoBehaviour component)
        {
            return TryGet(component, out Player player) ? player : null;
        }

        /// <summary>
        ///     Gets a Player instance based on a NetworkIdentity.
        /// </summary>
        /// <param name="identity">The NetworkIdentity of the player.</param>
        /// <returns>The Player instance if found; otherwise, null.</returns>
        public static Player Get(NetworkIdentity identity)
        {
            return TryGet(identity, out Player player) ? player : null;
        }

        /// <summary>
        ///     Gets a Player instance based on their PlayerId.
        /// </summary>
        /// <param name="id">The PlayerId of the player.</param>
        /// <returns>The Player instance if found; otherwise, null.</returns>
        public static Player Get(int id)
        {
            return TryGet(id, out Player player) ? player : null;
        }

        /// <summary>
        ///     Gets a Player instance based on their NetworkId.
        /// </summary>
        /// <param name="netId">The NetworkId of the player.</param>
        /// <returns>The Player instance if found; otherwise, null.</returns>
        public static Player Get(uint netId)
        {
            return TryGet(netId, out Player player) ? player : null;
        }

        /// <summary>
        ///     Gets a <see cref="Player" /> with the <see cref="ICommandSender" />.
        /// </summary>
        /// <param name="sender">The <see cref="ICommandSender" /> to get the <see cref="Player" /> with.</param>
        /// <returns></returns>
        public static Player Get(ICommandSender sender)
        {
            return TryGet(sender, out Player player) ? player : null;
        }

        /// <summary>
        ///     Gets a <see cref="Player" /> with the specified <see cref="Footprinting.Footprint" />.
        /// </summary>
        /// <param name="footprint">The <see cref="Footprinting.Footprint" /> to use to find the <see cref="Player" />.</param>
        /// <returns></returns>
        public static Player Get(Footprint footprint)
        {
            return Get(footprint.Hub);
        }

        /// <summary>
        ///     Gets a <see cref="Player" /> by a variable.
        /// </summary>
        /// <remarks>
        ///     You can pass a <see cref="RawUserId" />, a <see cref="UserId" />, a <see cref="Id" />, a <see cref="NetId" />,
        ///     and the players <see cref="Nickname" />. Otherwise, null.
        /// </remarks>
        public static Player Get(string variable)
        {
            return TryGet(variable, out Player player) ? player : null;
        }

        /// <summary>
        ///     Tries to get a <see cref="Player" /> by a variable.
        /// </summary>
        /// <remarks>
        ///     You can pass a <see cref="RawUserId" />, a <see cref="UserId" />, a <see cref="Id" />, a <see cref="NetId" />,
        ///     and the players <see cref="Nickname" />. Otherwise, null.
        /// </remarks>
        public static bool TryGet(string variable, out Player player)
        {
            player = null;
            if (string.IsNullOrEmpty(variable))
            {
                return false;
            }

            Player first = List.FirstOrDefault(ply1 => ply1.RawUserId == variable || ply1.UserId == variable);

            if (first != null)
            {
                player = first;
                return true;
            }

            if (int.TryParse(variable, out int id) && TryGet(id, out Player plyID))
            {
                player = plyID;
                return true;
            }

            if (uint.TryParse(variable, result: out uint nid) && TryGet(nid, out Player plyNID))
            {
                player = plyNID;
                return true;
            }

            if (List.FirstOrDefault(ply => ply.Nickname.Equals(variable, StringComparison.OrdinalIgnoreCase)) is not { } plyByUsername)
            {
                return false;
            }

            player = plyByUsername;
            return true;

        }

        /// <summary>
        ///     Kills the player with a custom reason.
        /// </summary>
        /// <param name="text">The reason for the kill.</param>
        public void Kill(string text)
        {
            Kill(new CustomReasonDamageHandler(text, float.MaxValue));
        }

        /// <summary>
        ///     Kills the player with a custom damage handler.
        /// </summary>
        /// <param name="damageHandlerBase">The custom damage handler for the kill.</param>
        public void Kill(DamageHandlerBase damageHandlerBase)
        {
            ReferenceHub.playerStats.KillPlayer(damageHandlerBase);
        }

        /// <summary>
        ///     Deals damage to the player with a custom amount and reason.
        /// </summary>
        /// <param name="amount">The amount of damage.</param>
        /// <param name="reason">The reason for the damage. (Optional)</param>
        public void Damage(float amount, string reason = "")
        {
            ReferenceHub.playerStats.DealDamage(new CustomReasonDamageHandler(reason, amount));
        }

        /// <summary>
        ///     Deals damage to the player with a custom damage handler.
        /// </summary>
        /// <param name="damageHandlerBase">The custom damage handler for the damage.</param>
        public void Damage(DamageHandlerBase damageHandlerBase)
        {
            ReferenceHub.playerStats.DealDamage(damageHandlerBase);
        }

        /// <summary>
        ///     Heals the player.
        /// </summary>
        /// <param name="amount">The amount of HP to heal.</param>
        /// <param name="exceedMaxHealth">If max limit should be exceeded while healing the player.</param>
        public void Heal(float amount, bool exceedMaxHealth = false)
        {
            if (exceedMaxHealth)
            {
                Health += amount;
            }
            else
            {
                _customHealthManager.ServerHeal(amount);
            }
        }

        /// <summary>
        ///     Tries to get a specific status effect on the player based on its name.
        /// </summary>
        /// <param name="effectName">The name of the status effect.</param>
        /// <param name="effect">When this method returns, contains the status effect instance if found; otherwise, null.</param>
        /// <returns>True if the status effect was found; otherwise, false.</returns>
        public bool TryGetEffect(string effectName, out StatusEffectBase effect)
        {
            return ReferenceHub.playerEffectsController.TryGetEffect(effectName, out effect);
        }

        /// <summary>
        ///     Tries to get a specific status effect on the player based on its type.
        /// </summary>
        /// <typeparam name="T">The type of the status effect.</typeparam>
        /// <param name="effect">When this method returns, contains the status effect instance if found; otherwise, null.</param>
        /// <returns>True if the status effect was found; otherwise, false.</returns>
        public bool TryGetEffect<T>(out T effect) where T : StatusEffectBase
        {
            return ReferenceHub.playerEffectsController.TryGetEffect(out effect);
        }

        /// <summary>
        ///     Enables a specific status effect on the player.
        /// </summary>
        /// <typeparam name="T">The type of the status effect.</typeparam>
        /// <param name="duration">The duration of the status effect. (Optional)</param>
        /// <param name="addDuration">Whether to add the duration to the existing effect if already active. (Optional)</param>
        /// <returns>The enabled status effect instance.</returns>
        public T EnableEffect<T>(float duration = 0f, bool addDuration = false) where T : StatusEffectBase
        {
            return ReferenceHub.playerEffectsController.EnableEffect<T>(duration, addDuration);
        }

        /// <summary>
        ///     Enables a specific <see cref="StatusEffect" /> on the player.
        /// </summary>
        public void EnableEffect(StatusEffect statusEffect, float duration = 0f, bool addDuration = false)
        {
            EnableEffect(statusEffect.EffectToType().Name, duration: duration, addDuration: addDuration);
        }

        /// <summary>
        ///     Enables a specific status effect on the player.
        /// </summary>
        /// <param name="effectName">The name of the effect.</param>
        /// <param name="intensity">The intensity of the effect.</param>
        /// <param name="duration">The duration of the status effect. (Optional)</param>
        /// <param name="addDuration">Whether to add the duration to the existing effect if already active. (Optional)</param>
        public StatusEffectBase EnableEffect(string effectName, byte intensity = 1, float duration = 0f,
            bool addDuration = false)
        {
            return ReferenceHub.playerEffectsController.ChangeState(effectName, intensity, duration, addDuration);
        }

        /// <summary>
        ///     Disables a specific status effect on the player.
        /// </summary>
        /// <typeparam name="T">The type of the status effect.</typeparam>
        /// <returns>The disabled status effect instance.</returns>
        public T DisableEffect<T>() where T : StatusEffectBase
        {
            return ReferenceHub.playerEffectsController.DisableEffect<T>();
        }

        /// <summary>
        ///     Disables all status effects on the player.
        /// </summary>
        public void DisableAllEffects()
        {
            ReferenceHub.playerEffectsController.DisableAllEffects();
        }

        /// <summary>
        ///     Shows the player's tag.
        /// </summary>
        public void ShowTag()
        {
            switch (ReferenceHub.serverRoles.UserBadgePreferences)
            {
                case ServerRoles.BadgePreferences.PreferLocal:
                    ReferenceHub.serverRoles.RefreshLocalTag();
                    break;
                case ServerRoles.BadgePreferences.PreferGlobal:
                    ReferenceHub.serverRoles.RefreshGlobalTag();
                    break;
                case ServerRoles.BadgePreferences.NoPreference:
                    ReferenceHub.serverRoles.RefreshLocalTag();
                    break;
            }
        }

        /// <summary>
        ///     Hides the player's tag.
        /// </summary>
        public void HideTag()
        {
            //Fuck NW way
            ReferenceHub.serverRoles.GlobalHidden = ReferenceHub.serverRoles.GlobalSet;
            ReferenceHub.serverRoles.HiddenBadge = ReferenceHub.serverRoles.MyText;
            ReferenceHub.serverRoles.NetworkGlobalBadge = null;
            ReferenceHub.serverRoles.SetText(null);
            ReferenceHub.serverRoles.SetColor(null);
            ReferenceHub.serverRoles.RefreshHiddenTag();
        }

        /// <summary>
        ///     Shows a hint to the player with the specified content and duration.
        /// </summary>
        /// <param name="content">The content of the hint.</param>
        /// <param name="time">The duration of the hint in seconds.</param>
        public void ShowHint(string content, float time = 5)
        {
            ShowHint(new TextHint(content, new HintParameter[] { new StringHintParameter(content) }, null, time));
        }

        /// <summary>
        ///     Shows a <see cref="Hint" /> to the player.
        /// </summary>
        /// <param name="hint">The <see cref="Hint" /> to show.</param>
        public void ShowHint(Hint hint)
        {
            ReferenceHub.hints.Show(hint);
        }

        /// <summary>
        ///     Sets the role of the player.
        /// </summary>
        /// <param name="role">The role to set.</param>
        /// <param name="reason">The reason for the role change. (Optional)</param>
        /// <param name="flags">The flags for the role spawn. (Optional)</param>
        public void SetRole(RoleTypeId role, RoleChangeReason reason = RoleChangeReason.RemoteAdmin,
            RoleSpawnFlags flags = RoleSpawnFlags.All)
        {
            RoleManager.ServerSetRole(role, reason, flags);
        }

        /// <summary>
        ///     Adds a component of the specified type to the player's GameObject.
        /// </summary>
        /// <typeparam name="T">The type of the component to add.</typeparam>
        /// <returns>The added component instance.</returns>
        public T AddComponent<T>() where T : MonoBehaviour
        {
            return GameObject.AddComponent<T>();
        }

        /// <summary>
        ///     Gets the component of the specified type from the player's GameObject.
        /// </summary>
        /// <typeparam name="T">The type of the component to get.</typeparam>
        /// <returns>The component instance if found; otherwise, null.</returns>
        public T GetComponent<T>() where T : MonoBehaviour
        {
            return GameObject.GetComponent<T>();
        }

        /// <summary>
        ///     Kicks the player from the server with the specified reason.
        /// </summary>
        /// <param name="reason">The reason for the kick.</param>
        /// <returns>True if the kick was successful; otherwise, false.</returns>
        public bool Kick(string reason = null)
        {
            return BanPlayer.KickUser(ReferenceHub, reason);
        }

        /// <summary>
        ///     Bans the player from the server with the specified reason and duration.
        /// </summary>
        /// <param name="reason">The reason for the ban.</param>
        /// <param name="duration">The duration of the ban in seconds.</param>
        /// <returns>True if the ban was successful; otherwise, false.</returns>
        public bool Ban(string reason, long duration)
        {
            return BanPlayer.BanUser(ReferenceHub, reason, duration);
        }

        /// <summary>
        ///     Mutes the player's voice chat.
        /// </summary>
        /// <param name="isIntercom">Whether to mute the player globally or only on intercom. (Optional)</param>
        /// <param name="isTemporary">Whether to apply a temporary mute or a regular global mute. (Optional)</param>
        public void Mute(bool isIntercom = false, bool isTemporary = true)
        {
            if (isTemporary)
            {
                VoiceChatMutes.SetFlags(ReferenceHub, VcMuteFlags.GlobalRegular);
                return;
            }

            VoiceChatMutes.IssueLocalMute(UserId, isIntercom);
        }

        /// <summary>
        ///     Unmutes the player's voice chat.
        /// </summary>
        /// <param name="removeMute">Whether to remove the player's local mute or the global mute. (Optional)</param>
        public void UnMute(bool removeMute = true)
        {
            if (removeMute)
            {
                VoiceChatMutes.RevokeLocalMute(UserId);
                return;
            }

            VoiceChatMutes.SetFlags(ReferenceHub, VcMuteFlags.None);
        }

        /// <summary>
        ///     Sends a hit marker effect to the player.
        /// </summary>
        /// <param name="size">The size of the hit marker. (Optional)</param>
        public void SendHitMarker(float size = 2.55f)
        {
            Hitmarker.SendHitmarkerDirectly(ReferenceHub, size);
        }

        /// <summary>
        ///     Checks if the player has the specified permission.
        /// </summary>
        /// <param name="permission">The permission to check.</param>
        /// <returns>True if the player has the permission; otherwise, false.</returns>
        public bool HasPlayerPermission(PlayerPermissions permission)
        {
            return PermissionsHandler.IsPermitted(Permissions, permission);
        }

        /// <summary>
        ///     Checks if the player has any of the specified permissions.
        /// </summary>
        /// <param name="permissions">The array of permissions to check.</param>
        /// <returns>True if the player has any of the permissions; otherwise, false.</returns>
        public bool HasPlayerPermissions(PlayerPermissions[] permissions)
        {
            return PermissionsHandler.IsPermitted(Permissions, permissions);
        }

        /// <summary>
        ///     Sends a broadcast to the player.
        /// </summary>
        /// <param name="message">The message that will be shown.</param>
        /// <param name="duration">The duration of the broadcast.</param>
        /// <param name="broadcastFlags">The <see cref="BroadcastFlags" /> of the broadcast.</param>
        /// <param name="clearCurrent">Determines if the players current broadcasts should be cleared.</param>
        public void Broadcast(string message, ushort duration = 5,
            BroadcastFlags broadcastFlags = BroadcastFlags.Normal, bool clearCurrent = true)
        {
            if (clearCurrent)
            {
                ClearBroadcasts();
            }

            Server.Broadcast.TargetAddElement(NetworkConnection, message, duration, broadcastFlags);
        }

        /// <summary>
        ///     Sends a broadcast to the player.
        /// </summary>
        /// <param name="broadcast">The <see cref="Features.Broadcast" /> to show the player.</param>
        /// <param name="clearCurrent">Determines if the players current broadcasts should be cleared.</param>
        public void Broadcast(Broadcast broadcast, bool clearCurrent = true)
        {
            Broadcast(broadcast.Message, broadcast.Duration, broadcast.BroadcastFlags, clearCurrent);
        }

        /// <summary>
        ///     Clears all of the player's current broadcasts.
        /// </summary>
        public void ClearBroadcasts()
        {
            Server.Broadcast.TargetClearElements(NetworkConnection);
        }

        /// <summary>
        ///     Parses the UserId to extract the RawUserId without the discriminator.
        /// </summary>
        private void Create()
        {
            if (UserId is null)
            {
                RawUserId = $"NebulaUserId-{PlayerCount}";
                return;
            }

            int index = UserId.LastIndexOf('@');

            if (index == -1)
            {
                RawUserId = UserId;
                return;
            }

            RawUserId = UserId.Substring(0, index);
        }

        /// <summary>
        ///     Gets if the player is part of a Mobile Task Force Unit.
        /// </summary>
        /// <param name="includeGuards">If being a <see cref="RoleTypeId.FacilityGuard" /> counts as MTF.</param>
        public bool IsMTF(bool includeGuards = true)
        {
            return RoleType.IsMTF(includeGuards);
        }

        /// <summary>
        ///     Gets if the player is part of the Chaos Insurgency.
        /// </summary>
        public bool IsCI()
        {
            return RoleType.IsCI();
        }

        /// <summary>
        ///     Clears the players inventory.
        /// </summary>
        /// <param name="includeAmmo">If ammo should also be cleared.</param>
        public void ClearInventory(bool includeAmmo = true)
        {
            if (includeAmmo)
            {
                Ammo.Clear();
            }

            foreach (ItemBase item in Inventory.UserInventory.Items.Values.ToList())
            {
                Inventory.ServerRemoveItem(item.ItemSerial, item.PickupDropModel);
            }
        }

        /// <summary>
        ///     Makes the player drop everything in their inventory.
        /// </summary>
        public void DropEverything()
        {
            Inventory.ServerDropEverything();
        }

        /// <summary>
        ///     Drops a specified amount of ammo of a given type.
        /// </summary>
        /// <param name="ammotype">The type of ammo to drop.</param>
        /// <param name="amount">The amount of ammo to drop.</param>
        /// <param name="checkMinimals">
        ///     Optional. Specifies whether to check minimal conditions for dropping ammo. Defaults to
        ///     false.
        /// </param>
        public void DropAmmo(AmmoType ammotype, ushort amount, bool checkMinimals = false)
        {
            Inventory.ServerDropAmmo(ammotype.ConvertToItemType(), amount, checkMinimals);
        }

        /// <summary>
        ///     Adds ammo to the players inventory.
        /// </summary>
        /// <param name="ammoType">The type of ammo to add.</param>
        /// <param name="amount">The ammount of ammo to add.</param>
        public void AddAmmo(AmmoType ammoType, int amount)
        {
            Inventory.ServerAddAmmo(ammoType.ConvertToItemType(), amount);
        }

        /// <summary>
        ///     Adds a item to the players inventory.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public Item AddItem(ItemType item)
        {
            if (item.IsFirearmType())
            {
                Firearm firearm = Item.Get(Inventory.ServerAddItem(item)) as Firearm;
                return firearm.AddPlayerAttachments(this);
            }

            return Item.Get(Inventory.ServerAddItem(item));
        }

        /// <summary>
        ///     Adds a item to the players inventory.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public Item AddItem(Item item)
        {
            return AddItem(item.ItemType);
        }

        /// <summary>
        ///     Removes a <see cref="Item" /> from the players inventory.
        /// </summary>
        public bool RemoveItem(Item item, bool destroyItem = true)
        {
            if (!Items.Contains(item))
            {
                return false;
            }

            if (destroyItem)
            {
                Inventory.ServerRemoveItem(item.Serial, null);
            }
            else
            {
                item.Base.OnRemoved(null);
                item.Base.Owner = Server.Host.ReferenceHub;
                item.Base.OnAdded(null);
                if (CurrentItem is not null && CurrentItem.Serial == item.Serial)
                {
                    Inventory.NetworkCurItem = ItemIdentifier.None;
                }

                Inventory.UserInventory.Items.Remove(item.Serial);
                Inventory.SendItemsNextFrame = true;
            }

            return true;
        }

        /// <summary>
        ///     Adds a list of items to the players inventory.
        /// </summary>
        /// <param name="items">The items to add.</param>
        public void AddItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                AddItem(item);
            }
        }

        /// <summary>
        ///     Gets if the player has a specified <see cref="KeycardPermissions" />.
        /// </summary>
        public bool HasKeycardPermission(KeycardPermissions keycardPermissions, bool IncludeInventory = false)
        {
            bool value = false;

            if (CurrentItem != null && CurrentItem is Keycard keycard)
            {
                value = keycard.Permissions >= keycardPermissions;
            }

            if (IncludeInventory && !value)
            {
                value = Items.Any(x =>
                    x is not null && x is Keycard keyperm && keyperm.Permissions >= keycardPermissions);
            }

            return value;
        }

        /// <summary>
        ///     Sends a Console Message to the player.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="color">The color to send it in.</param>
        public void SendConsoleMessage(string message, string color)
        {
            ReferenceHub.gameConsoleTransmission.SendToClient(message, color);
        }

        /// <summary>
        ///     Explodes the player.
        /// </summary>
        public void Explode()
        {
            ExplosionUtils.ServerExplode(ReferenceHub);
        }

        /// <summary>
        ///     Vaporizes the player with the <see cref="DisruptorDamageHandler" />.
        /// </summary>
        /// <param name="damageHandler">The <see cref="DisruptorDamageHandler" /> to use for vaporizing the player.</param>
        public void Vaporize(DisruptorDamageHandler damageHandler = default)
        {
            Kill(damageHandler);
        }

        /// <summary>
        ///     Checks if the player has any permission in <see cref="PlayerPermissions" />.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <returns></returns>
        public static bool HasAnyPermission(Player player)
        {
            foreach (PlayerPermissions perm in PermissionsHandler.PermissionCodes.Keys)
            {
                if (player.HasPlayerPermission(perm))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Converts the player to a string.
        /// </summary>
        public override string ToString()
        {
            return
                $"ID: {Id} - Nickname : {Nickname} - UserID: {UserId} - Role: {(Role is null ? "No role" : Role.RoleName)} Team - {Team}";
        }
    }
}