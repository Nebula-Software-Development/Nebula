using LightContainmentZoneDecontamination;
using static LightContainmentZoneDecontamination.DecontaminationController;

namespace Nebuli.API.Features;

/// <summary>
/// Allows easy management of the <see cref="DecontaminationController"/>.
/// </summary>
public static class Decontamination
{
    /// <summary>
    /// Gets the <see cref="DecontaminationController"/>.
    /// </summary>
    public static readonly DecontaminationController Controller = Singleton;

    /// <summary>
    /// Gets if <see cref="DecontaminationController"/> is currently decontaminating.
    /// </summary>
    public static bool IsDecontaminating => Controller.IsDecontaminating;

    /// <summary>
    /// Gets the round start time of the <see cref="Controller"/>.
    /// </summary>
    public static double RoundStartTime
    {
        get => Controller.RoundStartTime;
        set => Controller.NetworkRoundStartTime = value;
    }

    /// <summary>
    /// Disables the elevators.
    /// </summary>
    public static void DisableElevators() => Controller.DisableElevators();

    /// <summary>
    /// Finishes the decontamination sequence.
    /// </summary>
    public static void FinishDecontamination() => Controller.FinishDecontamination();

    /// <summary>
    /// Forces Light Containment to be decontaminated.
    /// </summary>
    public static void ForceDecontamination() => Controller.ForceDecontamination();

    /// <summary>
    /// Gets or sets the <see cref="DecontaminationController.DecontaminationStatus"/>.
    /// </summary>
    public static DecontaminationStatus DecontaminationStatus
    {
        get => Controller.DecontaminationOverride;
        set => Controller.NetworkDecontaminationOverride = value;
    }
}