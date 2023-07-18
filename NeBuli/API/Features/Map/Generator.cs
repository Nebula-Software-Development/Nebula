using System.Collections.Generic;
using System.Linq;
using MapGeneration.Distributors;
using Nebuli.API.Features.Player;

namespace Nebuli.API.Features.Map;

public class Generator
{
    public static readonly Dictionary<Scp079Generator, Generator> Dictionary = new();
    
    internal Generator(Scp079Generator generator)
    {
        Base = generator;
        Dictionary.Add(generator, this);
    }
    
    public Scp079Generator Base { get; }
    
    public static IEnumerable<Generator> Collection => Dictionary.Values;
    
    /// <summary>
    /// Gets a list of all the generators on the server.
    /// </summary>
    public static List<Generator> List => Collection.ToList();

    public float DropdownSpeed => Base.DropdownSpeed;

    public bool IsReady => Base.ActivationReady;

    public bool IsBeingActivated
    {
        get => Base.Activating;
        set => Base.Activating = value;
    }

    public bool IsOpen
    {
        get => Base.IsOpen;
        set => Base.IsOpen = value;
    }

    public bool IsUnlocked
    {
        get => Base.IsUnlocked;
        set => Base.IsUnlocked = value;
    }
    
    public bool IsEngaged
    {
        get => Base.Engaged;
        set => Base.Engaged = value;
    }
    
    public int RemainingTime => Base.RemainingTime;
    
    public static Generator Get(Scp079Generator generator)
    {
        return Dictionary.TryGetValue(generator, out var gen) ? gen : new Generator(generator);
    }

    public void Interact(NebuliPlayer player, byte colliderId) => Base.ServerInteract(player.ReferenceHub, colliderId);
    
    public void HasFlag(byte flags, Scp079Generator.GeneratorFlags flag) => Base.HasFlag(flags, flag);
    
    public void SetFlag(Scp079Generator.GeneratorFlags flag, bool state) => Base.ServerSetFlag(flag, state);
}