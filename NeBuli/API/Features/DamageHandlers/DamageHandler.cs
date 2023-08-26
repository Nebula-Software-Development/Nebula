using CustomPlayerEffects;
using Footprinting;
using Nebuli.API.Features.Player;
using PlayerStatsSystem;
using UnityEngine;

namespace Nebuli.API.Features.DamageHandlers;

/// <summary>
/// Base abstract class for handling damage.
/// </summary>
public abstract class DamageHandler : DamageHandlerBase
{
    /// <summary>
    /// Gets or sets the target player who is receiving the damage.
    /// </summary>
    public NebuliPlayer Target { get; protected set; }

    /// <summary>
    /// Gets or sets the attacker player causing the damage. Can be null.
    /// </summary>
    public NebuliPlayer Attacker { get; set; }

    /// <summary>
    /// Gets or sets the target's footprint.
    /// </summary>
    public Footprint TargetFootprint { get; protected set; }

    /// <summary>
    /// Gets or sets the attacker's footprint.
    /// </summary>
    public Footprint AttackerFootprint { get; protected set; } 

    /// <summary>
    /// Gets or sets the direct damage dealt to the target's health.
    /// </summary>
    public float DealtHealthDamage
    {
        get => TryGetHandler(out var handler) ? handler.DealtHealthDamage : 0f;
        set => TrySetHandler(handler => handler.DealtHealthDamage = value);
    }

    /// <summary>
    /// Gets or sets the amount of damage to be dealt.
    /// </summary>
    public virtual float Damage
    {
        get => TryGetHandler(out var handler) ? handler.Damage : 0f;
        set => TrySetHandler(handler => handler.Damage = value);
    }

    /// <summary>
    /// Gets or sets the start velocity.
    /// </summary>
    public Vector3 StartVelocity
    {
        get => TryGetHandler(out var handler) ? handler.StartVelocity : Vector3.zero;
        set => TrySetHandler(handler => handler.StartVelocity = value);
    }

    /// <summary>
    /// Gets or sets the damage absorbed by AHP (Anomalous Health Points) processes.
    /// </summary>
    public float AbsorbedAhpDamage
    {
        get => TryGetHandler(out var handler) ? handler.AbsorbedAhpDamage : 0f;
        set => TrySetHandler(handler => handler.AbsorbedAhpDamage = value);
    }

    /// <summary>
    /// Tries to retrieve the handler for dealing standard damage.
    /// </summary>
    /// <param name="handler">The retrieved handler if successful.</param>
    /// <returns>True if the handler was retrieved successfully, false otherwise.</returns>
    protected bool TryGetHandler(out StandardDamageHandler handler)
    {
        if (Is(out StandardDamageHandler standardDamageHandler))
        {
            handler = standardDamageHandler;
            return true;
        }
        handler = default;
        return false;
    }

    /// <summary>
    /// Tries to set properties of the standard damage handler.
    /// </summary>
    /// <param name="setter">The action to perform on the handler.</param>
    protected void TrySetHandler(System.Action<StandardDamageHandler> setter)
    {
        if (TryGetHandler(out var handler))
            setter(handler);
    }

    /// <summary>
    /// Processes damage for the player.
    /// </summary>
    /// <param name="player">The player for which damage is being processed.</param>
    protected Action ProcessDamage(NebuliPlayer player)
    {
        if (TryGetHandler(out StandardDamageHandler damageHandler))
        {
            if (Damage > 0f)
            {
                damageHandler.ApplyDamage(player.ReferenceHub);

                StartVelocity = player.Velocity;
                damageHandler.StartVelocity.y = Mathf.Max(damageHandler.StartVelocity.y, 0f);

                AhpStat ahpModule = player.ReferenceHub.playerStats.GetModule<AhpStat>();
                HealthStat healthModule = player.ReferenceHub.playerStats.GetModule<HealthStat>();

                if (Damage <= StandardDamageHandler.KillValue)
                {
                    ahpModule.CurValue = 0f;
                    healthModule.CurValue = 0f;
                    return Action.Death;
                }

                ProcessDamage(player);

                foreach (StatusEffectBase effect in player.ActiveEffects)
                {
                    if (effect is IDamageModifierEffect damageModifierEffect)
                    {
                        Damage *= damageModifierEffect.GetDamageModifier(Damage, damageHandler, damageHandler.Hitbox);
                    }
                }

                AbsorbedAhpDamage = Damage - DealtHealthDamage;
                return player.ReferenceHub.playerStats.GetModule<HealthStat>().CurValue > 0f ? Action.Damage : Action.Death;
            }
        }
        else
        {
            HealthStat healthStat = player.ReferenceHub.playerStats.GetModule<HealthStat>();
            return healthStat.CurValue > 0f ? Action.Damage : Action.Death;
        }

        return Action.None;
    }

    /// <summary>
    /// Default constructor.
    /// </summary>
    protected DamageHandler()
    {
    }

    /// <summary>
    /// Constructor with target and attacker initialization.
    /// </summary>
    /// <param name="target">The target player receiving damage.</param>
    /// <param name="attacker">The attacker player causing the damage.</param>
    protected DamageHandler(NebuliPlayer target, NebuliPlayer attacker)
    {
        Target = target;
        Attacker = attacker;
        AttackerFootprint = attacker.Footprint;
        TargetFootprint = target.Footprint;
    }
}