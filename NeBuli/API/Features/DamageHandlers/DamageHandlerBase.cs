using PlayerRoles.PlayableScps.Scp939;
using PlayerStatsSystem;
using OldDamageHandler = PlayerStatsSystem.DamageHandlerBase;

namespace Nebuli.API.Features.DamageHandlers;

/// <summary>
/// Base abstract class for damage handler implementations.
/// </summary>
public abstract class DamageHandlerBase
{
    private CassieAnnouncement announcement;

    /// <summary>
    /// Default constructor.
    /// </summary>
    protected DamageHandlerBase()
    {
    }

    /// <summary>
    /// Gets the base damage handler.
    /// </summary>
    public OldDamageHandler Base { get; }

    /// <summary>
    /// Constructor that initializes the base damage handler.
    /// </summary>
    /// <param name="damageHandler">The base damage handler.</param>
    protected DamageHandlerBase(DamageHandler damageHandler)
    {
        Base = damageHandler;
    }

    /// <summary>
    /// Gets the server logs text for the damage handler.
    /// </summary>
    public virtual string ServerLogsText => Base.ServerLogsText;

    /// <summary>
    /// Gets or sets the Cassie announcement for the damage handler.
    /// </summary>
    public virtual CassieAnnouncement CassieAnnouncement
    {
        get => announcement ?? Base.CassieDeathAnnouncement;
        protected set => announcement = value;
    }   

    /// <summary>
    /// Checks if the damage handler is of type T.
    /// </summary>
    /// <typeparam name="T">The type to check.</typeparam>
    /// <param name="param">The parameter to output.</param>
    /// <returns>True if the damage handler is of type T, false otherwise.</returns>
    public bool Is<T>(out T param)
        where T : OldDamageHandler
    {
        param = default;

        if (Base is not T cast)
            return false;

        param = cast;
        return true;
    }

    /// <summary>
    /// Gets the <see cref="DamageType"/> for the damage handler.
    /// </summary>
    public virtual DamageType Type
    {
        get
        {
            return Base switch
            {
                CustomReasonDamageHandler => DamageType.Custom,
                WarheadDamageHandler => DamageType.Warhead,
                ExplosionDamageHandler => DamageType.Explosion,
                Scp018DamageHandler => DamageType.Scp018,
                RecontainmentDamageHandler => DamageType.Recontainment,
                MicroHidDamageHandler => DamageType.MicroHID,
                DisruptorDamageHandler => DamageType.ParticleDisruptor,
                Scp939DamageHandler => DamageType.Scp939,
                JailbirdDamageHandler => DamageType.Jailbird,
                Scp049DamageHandler scp049DamageHandler => scp049DamageHandler.DamageSubType switch
                {
                    Scp049DamageHandler.AttackType.CardiacArrest => DamageType.CardiacArrest,
                    Scp049DamageHandler.AttackType.Instakill => DamageType.Scp049,
                    Scp049DamageHandler.AttackType.Scp0492 => DamageType.Scp0492,
                    _ => DamageType.Universal,
                },
                _ => DamageType.Universal,
            };
        }
    }

    /// <summary>
    /// Implicitly converts the <see cref="DamageHandlerBase"/> to an <see cref="OldDamageHandler"/>.
    /// </summary>
    /// <param name="damageHandlerBase">The damage handler base.</param>
    public static implicit operator OldDamageHandler(DamageHandlerBase damageHandlerBase) => damageHandlerBase.Base;

    /// <summary>
    /// All the available <see cref="DamageHandler"/> actions.
    /// </summary>
    public enum Action : byte
    {
        /// <summary>
        /// No action at all.
        /// </summary>
        None,

        /// <summary>
        /// Action only resulting in damage.
        /// </summary>
        Damage,

        /// <summary>
        /// Action resulting in death.
        /// </summary>
        Death,
    }
}

