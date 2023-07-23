using PlayerRoles;
using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using PlayerRoles.PlayableScps.Scp079.Pinging;
using PlayerRoles.PlayableScps.Scp079.Rewards;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerRoles.Voice;
using System;

namespace Nebuli.API.Features.Roles
{
    public class Scp079PlayerRole : Role
    {
        /// <summary>
        /// Gets the <see cref="Scp079Role"/> base.
        /// </summary>
        public new Scp079Role Base { get; }
        internal Scp079PlayerRole(Scp079Role role) : base(role)
        {
            Base = role;
            SetupSubroutines();          
        }

        /// <summary>
        /// Gets if the blackout room ability is ready or not.
        /// </summary>
        public bool RoomBlackoutReady => BlackoutRoomAbility.IsReady;

        /// <summary>
        /// Gets if the blackout zone ability is ready or not.
        /// </summary>
        public bool ZoneBlackoutReady => BlackoutZoneAbility.IsReady;

        /// <summary>
        /// Forces SCP-079 to lose singal for the specified duration.
        /// </summary>
        /// <param name="duration">The duration of the signal loss.</param>
        public void LoseSignal(float duration) => AbilityBase.LostSignalHandler.ServerLoseSignal(duration);

        /// <summary>
        /// Gets the roles <see cref="PlayerRoles.RoleTypeId"/>.
        /// </summary>
        public override RoleTypeId RoleTypeId => Base.RoleTypeId;

        /// <summary>
        /// Gets if the role is AFK.
        /// </summary>
        public bool IsAFK => Base.IsAFK;

        /// <summary>
        /// Gets if the role is currently being spectated.
        /// </summary>
        public bool IsSpectated => Base.IsSpectated;

        /// <summary>
        /// Gets if the current role is within the idle range of a Tesla Gate.
        /// </summary>
        /// <param name="teslaGate">The <see cref="TeslaGate"/> to check.</param>
        /// <returns></returns>
        public bool IsInIdleRange(TeslaGate teslaGate) => Base.IsInIdleRange(teslaGate);

        /// <summary>
        /// Gets the roles current camera horizontal rotation.
        /// </summary>
        public float HorizontalRotation => Base.HorizontalRotation;

        /// <summary>
        /// Gets the roles current vertical rotation.
        /// </summary>
        public float VerticalRotation => Base.VerticalRotation;

        /// <summary>
        /// Gets if the role can activate a Tesla Gate shock.
        /// </summary>
        public bool CanActivateTeslaShock => Base.CanActivateShock;

        /// <summary>
        /// Gets the current camera the role is on.
        /// </summary>
        public Scp079Camera Camera => Base.CurrentCamera;

        /// <summary>
        /// Gets the roles <see cref="VoiceModuleBase"/>.
        /// </summary>
        public VoiceModuleBase VoiceModule => Base.VoiceModule;

        /// <summary>
        /// Gets the SCP-079 SubroutineManagerModule.
        /// </summary>
        public SubroutineManagerModule ManagerModule { get; internal set; }

        /// <summary>
        /// Gets the SCP-079 AbilityBase.
        /// </summary>
        public Scp079AbilityBase AbilityBase { get; internal set; }

        /// <summary>
        /// Gets the SCP-079 AuxManager.
        /// </summary>
        public Scp079AuxManager AuxManager { get; internal set; }

        /// <summary>
        /// Gets the SCP-079 BlackoutRoomAbility.
        /// </summary>
        public Scp079BlackoutRoomAbility BlackoutRoomAbility { get; internal set; }

        /// <summary>
        /// Gets the SCP-079 BlackoutZoneAbility.
        /// </summary>
        public Scp079BlackoutZoneAbility BlackoutZoneAbility { get; internal set; }

        /// <summary>
        /// Gets the SCP-079 DoorAbility.
        /// </summary>
        public Scp079DoorAbility Scp079DoorAbility { get; internal set; }

        /// <summary>
        /// Gets the SCP-079 LostSignalHandler.
        /// </summary>
        public Scp079LostSignalHandler LostSignalHandler { get; internal set; }

        /// <summary>
        /// Gets the SCP-079 DoorLockChanger.
        /// </summary>
        public Scp079DoorLockChanger DoorLockChanger { get; internal set; }

        /// <summary>
        /// Gets the SCP-079 TierManager.
        /// </summary>
        public Scp079TierManager TierManager { get; internal set; }

        /// <summary>
        /// Gets the SCP-079 RewardManager.
        /// </summary>
        public Scp079RewardManager RewardManager { get; internal set; }

        /// <summary>
        /// Gets the SCP-079 LockdownRoomAbility.
        /// </summary>
        public Scp079LockdownRoomAbility LockdownRoomAbility { get; internal set; }

        /// <summary>
        /// Gets the SCP-079 CurrentCameraSync.
        /// </summary>
        public Scp079CurrentCameraSync CurrentCameraSync { get; internal set; }

        /// <summary>
        /// Gets the SCP-079 PingAbility.
        /// </summary>
        public Scp079PingAbility Scp079PingAbility { get; internal set; }

        /// <summary>
        /// Gets the SCP-079 TeslaAbility.
        /// </summary>
        public Scp079TeslaAbility TeslaAbility { get; internal set; }

        internal void SetupSubroutines()
        {
            try
            {
                if (ManagerModule.TryGetSubroutine(out Scp079DoorLockChanger scp079DoorLockChanger))
                    DoorLockChanger = scp079DoorLockChanger;

                if (ManagerModule.TryGetSubroutine(out Scp079AuxManager scp079AuxManager))
                    AuxManager = scp079AuxManager;

                if (ManagerModule.TryGetSubroutine(out Scp079TierManager scp079TierManager))
                    TierManager = scp079TierManager;

                if (ManagerModule.TryGetSubroutine(out Scp079RewardManager scp079RewardManager))
                    RewardManager = scp079RewardManager;

                if (ManagerModule.TryGetSubroutine(out Scp079LockdownRoomAbility scp079LockdownRoomAbility))
                    LockdownRoomAbility = scp079LockdownRoomAbility;

                if (ManagerModule.TryGetSubroutine(out Scp079BlackoutRoomAbility scp079BlackoutRoomAbility))
                    BlackoutRoomAbility = scp079BlackoutRoomAbility;

                if (ManagerModule.TryGetSubroutine(out Scp079BlackoutZoneAbility scp079BlackoutZoneAbility))
                    BlackoutZoneAbility = scp079BlackoutZoneAbility;

                if (ManagerModule.TryGetSubroutine(out Scp079LostSignalHandler scp079LostSignalHandler))
                    LostSignalHandler = scp079LostSignalHandler;

                if (ManagerModule.TryGetSubroutine(out Scp079CurrentCameraSync scp079CameraSync))
                    CurrentCameraSync = scp079CameraSync;

                if (ManagerModule.TryGetSubroutine(out Scp079PingAbility scp079PingAbility))
                    Scp079PingAbility = scp079PingAbility;

                if (ManagerModule.TryGetSubroutine(out Scp079TeslaAbility scp079TeslaAbility))
                    TeslaAbility = scp079TeslaAbility;
            }
            catch (Exception e)
            {
                Log.Error("An error occurred setting up SCP-079 subroutines! Full error --> \n" + e);
            }
        }


    }


}
