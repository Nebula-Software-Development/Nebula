using UnityEngine;
using FilmmakerRoleBase = PlayerRoles.Filmmaker.FilmmakerRole;

namespace Nebuli.API.Features.Roles;

public class FilmmakerRole : Role
{
    /// <summary>
    /// Gets the <see cref="FilmmakerRoleBase"/> base.
    /// </summary>
    public new FilmmakerRoleBase Base { get; }

    internal FilmmakerRole(FilmmakerRoleBase role) : base(role)
    {
        Base = role;
    }

    /// <summary>
    /// Gets the <see cref="FilmmakerRoleBase"/> custom role name.
    /// </summary>
    public string CustomRoleName => Base.CustomRoleName;

    /// <summary>
    /// Gets the <see cref="FilmmakerRoleBase"/> camera position.
    /// </summary>
    public Vector3 CameraPosition
    {
        get => Base.CameraPosition; 
        set => Base.CameraPosition = value;
    }

    /// <summary>
    /// Gets the <see cref="FilmmakerRoleBase"/> camera rotation.
    /// </summary>
    public Quaternion CameraRotation
    {
        get => Base.CameraRotation;
        set => Base.CameraRotation = value;
    }
}
