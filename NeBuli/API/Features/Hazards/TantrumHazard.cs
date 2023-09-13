using RelativePositioning;
using UnityEngine;
using TantrumHazardBase = Hazards.TantrumEnvironmentalHazard;

namespace Nebuli.API.Features.Hazards;

public class TantrumHazard : TemporaryHazard
{
    /// <summary>
    /// Gets the <see cref="TantrumHazardBase"/> base.
    /// </summary>
    public new TantrumHazardBase Base { get; }

    internal TantrumHazard(TantrumHazardBase tantrumHazardBase) : base(tantrumHazardBase)
    {
        Base = tantrumHazardBase;
    }

    /// <summary>
    /// Gets or sets the SynchronizedPosition of the tantrum.
    /// </summary>
    public RelativePosition SynchronizedPosition
    {
        get => Base.SynchronizedPosition;
        set => Base.SynchronizedPosition = value;
    }

    /// <summary>
    /// Gets or sets if the tantrum will play a sizzle sound effect.
    /// </summary>
    public bool PlaySizzle
    {
        get => Base.PlaySizzle;
        set => Base.PlaySizzle = value;
    }

    /// <summary>
    /// Gets or sets the correct position of the tantrum.
    /// </summary>
    public Transform CorrectPosition
    {
        get => Base._correctPosition;
        set => Base._correctPosition = value;
    }
}