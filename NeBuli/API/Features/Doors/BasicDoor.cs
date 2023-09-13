using BasicDoorBase = Interactables.Interobjects.BasicDoor;

namespace Nebuli.API.Features.Doors;

public class BasicDoor : Door
{
    /// <summary>
    /// Gets the <see cref="BasicDoorBase"/> base.
    /// </summary>
    public new BasicDoorBase Base { get; }

    internal BasicDoor(BasicDoorBase basicDoor) : base(basicDoor)
    {
        Base = basicDoor;
    }

    /// <summary>
    /// Forces the door to play a beep sound.
    /// </summary>
    /// <param name="deniedSound">If the beep sound should be a denied sound.</param>
    public void PlayBeepSound(bool deniedSound) => Base.RpcPlayBeepSound(deniedSound);
}