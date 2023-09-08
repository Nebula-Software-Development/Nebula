using SLPrimitiveToy = AdminToys.PrimitiveObjectToy;
using Nebuli.API.Features.Enum;
using UnityEngine;
using AdminToys;
using Nebuli.API.Features.Map;
using System.Linq;

namespace Nebuli.API.Features.AdminToys;

/// <summary>
/// Wrapper class for <see cref="global::AdminToys.PrimitiveObjectToy"/>.
/// </summary>
public class PrimitiveToy : AdminToy
{
    private bool hascollision;

    /// <summary>
    /// Gets the <see cref="SLPrimitiveToy"/> (base) of the <see cref="PrimitiveToy"/>.
    /// </summary>
    public new SLPrimitiveToy Base { get; }
    internal PrimitiveToy(SLPrimitiveToy adminToyBase) : base(adminToyBase, ToyType.Primitive)
    {
        Base = adminToyBase;
    }

    /// <summary>
    /// Gets or sets the <see cref="UnityEngine.Color"/> of the <see cref="PrimitiveToy"/>.
    /// </summary>
    public Color Color
    {
        get => Base.NetworkMaterialColor;
        set => Base.NetworkMaterialColor = value;
    }

    /// <summary>
    /// Gets or sets the <see cref="UnityEngine.PrimitiveType"/> of the <see cref="PrimitiveToy"/>.
    /// </summary>
    public PrimitiveType PrimitiveType
    {
        get => Base.NetworkPrimitiveType;
        set => Base.NetworkPrimitiveType = value;
    }

    /// <summary>
    /// Gets or sets if the primitive has collision.
    /// </summary>
    public bool HasCollision
    {
        get => hascollision;
        set => RefreshCollidable(value);
    }

    /// <summary>
    /// Creates a <see cref="PrimitiveToy"/>.
    /// </summary>
    public static PrimitiveToy Create(PrimitiveType primitiveType = PrimitiveType.Sphere, Vector3 position = default, Quaternion rotation = default, Vector3 scale = default, bool spawn = true)
    {
        PrimitiveToy primitive = new(Object.Instantiate(ToyUtilities.PrimitiveBase))
        {
            Position = position,
            Rotation = rotation,
            Scale = scale,
            PrimitiveType = primitiveType
        };
        if (spawn) primitive.SpawnToy();
        return primitive;
    }

    /// <summary>
    /// Gets a <see cref="PrimitiveToy"/> with the matching <see cref="SLPrimitiveToy"/> base.
    /// </summary>
    public static PrimitiveToy Get(SLPrimitiveToy primitiveObjectToy) => Utilites.AdminToys
        .FirstOrDefault(x => x.Base == primitiveObjectToy) as PrimitiveToy ?? new PrimitiveToy(primitiveObjectToy);


    private void RefreshCollidable(bool value)
    {
        UnspawnToy();
        hascollision = value;
        Vector3 scale = Scale;
        Vector3 newScale = value ? scale : new Vector3(-scale.x, -scale.y, -scale.z);
        Base.transform.localScale = newScale;
        SpawnToy();
    }

}
