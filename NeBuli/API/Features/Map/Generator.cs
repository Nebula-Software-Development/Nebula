using MapGeneration.Distributors;
using Nebuli.API.Features.Enum;
using Nebuli.API.Features.Player;
using System.Collections.Generic;
using System.Linq;

namespace Nebuli.API.Features.Map;

public class Generator
{
    /// <summary>
    /// Gets the <see cref="Scp079Generator"/> and <see cref="Generator"/> dictionary.
    /// </summary>
    public static readonly Dictionary<Scp079Generator, Generator> Dictionary = new();

    internal Generator(Scp079Generator generator)
    {
        Base = generator;
        Dictionary.Add(generator, this);
    }

    /// <summary>
    /// Gets the <see cref="Scp079Generator"/> base.
    /// </summary>
    public Scp079Generator Base { get; }

    public static IEnumerable<Generator> Collection => Dictionary.Values;

    /// <summary>
    /// Gets a list of all the generators on the server.
    /// </summary>
    public static List<Generator> List => Collection.ToList();

    /// <summary>
    /// Gets the DropdownSpeed of the generator.
    /// </summary>
    public float DropdownSpeed => Base.DropdownSpeed;

    /// <summary>
    /// Gets whether or not the generator is ready to be activated.
    /// </summary>
    public bool IsReady => Base.ActivationReady;

    /// <summary>
    /// Gets or sets whether or not the generator is being activated.
    /// </summary>
    public bool IsBeingActivated
    {
        get => Base.Activating;
        set => Base.Activating = value;
    }

    /// <summary>
    /// Gets or sets whether or not the generator is opened.
    /// </summary>
    public bool IsOpen
    {
        get => Base.IsOpen;
        set => Base.IsOpen = value;
    }

    /// <summary>
    /// Gets or sets whether or not the generator is unlocked.
    /// </summary>
    public bool IsUnlocked
    {
        get => Base.IsUnlocked;
        set => Base.IsUnlocked = value;
    }

    /// <summary>
    /// Gets or sets whether or not the generator is engaged.
    /// </summary>
    public bool IsEngaged
    {
        get => Base.Engaged;
        set => Base.Engaged = value;
    }

    /// <summary>
    /// Gets the current <see cref="Enum.GeneratorState"/>
    /// </summary>
    public GeneratorState GeneratorState
    {
        get => (GeneratorState)Base._flags;
    }

    /// <summary>
    /// Gets the generators remaining time.
    /// </summary>
    public int RemainingTime => Base.RemainingTime;

    /// <summary>
    /// Gets a Nebuli <see cref="Generator"/> using <see cref="Scp079Generator"/>.
    /// </summary>
    /// <param name="generator"></param>
    /// <returns></returns>
    public static Generator Get(Scp079Generator generator)
    {
        return Dictionary.TryGetValue(generator, out var gen) ? gen : new Generator(generator);
    }

    /// <summary>
    /// Forces a generator to become interacted with.
    /// </summary>
    /// <param name="player">The player that will interact.</param>
    /// <param name="colliderId">The collider ID.</param>
    public void Interact(NebuliPlayer player, byte colliderId) => Base.ServerInteract(player.ReferenceHub, colliderId);

    public void HasFlag(byte flags, Scp079Generator.GeneratorFlags flag) => Base.HasFlag(flags, flag);

    public void HasFlag(byte flags, GeneratorState flag) => Base.HasFlag(flags, (Scp079Generator.GeneratorFlags)flag);

    /// <summary>
    /// Sets flags to the generator.
    /// </summary>
    /// <param name="flag">The flag to set.</param>
    /// <param name="state">The state to set.</param>
    public void SetFlag(Scp079Generator.GeneratorFlags flag, bool state) => Base.ServerSetFlag(flag, state);

    /// <summary>
    /// Sets flags to the generator.
    /// </summary>
    /// <param name="flag">The flag to set.</param>
    /// <param name="state">The state to set.</param>
    public void SetFlag(GeneratorState flag, bool state) => Base.ServerSetFlag((Scp079Generator.GeneratorFlags)flag, state);
}